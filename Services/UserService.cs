using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using blog.Data;
using blog.DTO.User;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace blog.Services
{
    public class UserService(ApplicationDbContext contex, IConfiguration configuration) : IUser
    {
        public async Task<List<User>> GetAll(int pageSize = 20, int pageIndex = 1, string? searchText = null)
        {

            if(pageSize <= 0 || pageIndex <= 0){
                throw new KeyNotFoundException("Page size and pageIndex must be greater than 0");
            }

            var users = contex.User.Where(u => u.IsDelete == false);

            if(!string.IsNullOrEmpty(searchText)) {
                users = users.Where(e => e.FullName.Contains(searchText));
            }

            if (users == null)
            {
                throw new KeyNotFoundException("Don't find user table");
            }

            var result = await users.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return result;
        }

       public async Task<List<User>> GetAllExsitDb(int pageSize, int pageIndex, string searchText, bool isDelete)
        {
            var users = await contex.User.Where(e => e.IsDelete == isDelete).Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).Where(e => string.IsNullOrEmpty(searchText) || e.FullName.Contains(searchText)).ToListAsync();

            if (users == null)
            {
                throw new KeyNotFoundException("Don't find user table");
            }

            return users;
        }

        public async Task<int> Count()
        {
            var count = await contex.User.CountAsync();

            return count;
        }

        public async Task<User> Create(CreateUser _user)
        {
            var user = new User();

            user.FullName = _user.FullName;
            user.Email = _user.Email;
            user.Password = _user.Password;
            user.UserName =  _user.UserName;

            await contex.User.AddAsync(user);
            await contex.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetUserById(Guid _id)
        {
            var user = await contex.User.FirstOrDefaultAsync(e => e.UserId == _id);

            return user;
        }
        public async Task<User?> GetUserByUserName(string userName)
        {
            var user = await contex.User.FirstOrDefaultAsync(e => e.UserName == userName);

            return user;
        }

        public async Task<bool> Delete(Guid _id)
        {
            var user = await GetUserById(_id);

            if (user == null)
            {
                throw new KeyNotFoundException("User don't exsit");
            }

            user.IsDelete = false;
            contex.Update(user);
            await contex.SaveChangesAsync();

            return true;

        }
        public async Task<User?> Edit(User _user)
        {
            var user = await contex.User.FirstOrDefaultAsync(e => e.UserId == _user.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException("User don't exsit");
            }

            user.FullName = _user.FullName;

            contex.User.Update(user);
            await contex.SaveChangesAsync();



            return user;
        }

         public async Task<string> Login(string _userName, string _password){
            var user = await contex.User.FirstOrDefaultAsync(e => e.UserName == _userName);

            if(user == null){
                throw new KeyNotFoundException("Don't find user by UseName");
            }

            if(user.Password == _password){
                var token = GenToken(user);
                
                return token;
            }else{
                throw new KeyNotFoundException("Password don't macth");
            }
         }

         private string GenToken(User user){
             var claim = new List<Claim>{
                new (JwtRegisteredClaimNames.Sub, user.FullName),
                new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new ("Id", user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"] ?? string.Empty));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["Audience"],
                claims: claim,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
         }

          public async Task<bool> isEmailExist(string email){
            return await contex.User.AnyAsync(e => e.Email == email);
          }
        public async Task<bool> isAccountExist(string acount){
            return await contex.User.AnyAsync(e => e.UserName == acount);
        }
    }
}
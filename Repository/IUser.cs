using blog.DTO.User;
using blog.Model;

namespace blog.Repository
{
    public interface IUser
    {
        public Task<List<UserDto>> GetAll(int pageSize = 20, int pageIndex = 1, string? searchText = null);
        public Task<List<User>> GetAllExsitDb(int pageSize, int pageIndex, string searchText, bool isDelete);
        public Task<int> Count();
        public Task<User> Create(CreateUser _user);
        public Task<User?> GetUserById(Guid _id);
        public Task<User?> GetUserByUserName(string userName);
        public Task<bool> Delete(Guid _id);
        public Task<User?> Edit(User _user);
        public Task<string?> Login(User _user, string password);
        public Task<bool> isEmailExist(string email);
        public Task<bool> isAccountExist(string acount);
        public Task<string> VerifyEmail(string email);
        public Task<Guid> ChangePassword(User user, string password);
        public Task<bool> ResetPassword(string email);
        public Task<User> CreateUserByAdmin(CUser user);
    }
}
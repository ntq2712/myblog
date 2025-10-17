using blog.Data;
using blog.DTO.MyProfile;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class GetInTouchService(ApplicationDbContext dbContext) : IGetInTouchRepository
    {
        public async Task<List<GetInTouch>> GetAllInTouches()
        {

            string password = "trongqui2712";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            Console.WriteLine($"hashedPassword: {hashedPassword}");

            return await dbContext.GetInTouch.OrderBy(e => e.CreateAt).ToListAsync();
        }
        public async Task<GetInTouch> GetInTouch(CGetInTouch dto)
        {
            var newItem = new GetInTouch
            {
                Email = dto.Email,
                Message = dto.Message,
                Moblie = dto.Moblie,
                CreateAt = DateTime.UtcNow
            };

            await dbContext.AddAsync(newItem);
            await dbContext.SaveChangesAsync();

            return newItem;
        }
    }
}
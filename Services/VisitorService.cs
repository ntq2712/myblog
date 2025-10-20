
using blog.Data;
using blog.DTO.Visitor;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class VisitorService(ApplicationDbContext dbContext) : IVisitorRepository
    {
        public Task<List<Visitor>> GetVisitors()
        {
            return dbContext.Visitor.ToListAsync();
        }
        public async Task<Visitor> RegiteVisitor(CVisitor visitor)
        {
            var newVisitor = new Visitor
            {
                FullName = visitor.FullName,
                CompanyName = visitor.CompanyName,
                Email = visitor.Email,
                Avatar = visitor.Avatar,
                PositionTitle = visitor.PositionTitle,
                Account = visitor.Account
            };

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(visitor.Password);

            newVisitor.Password = hashedPassword;

            await dbContext.AddAsync(newVisitor);
            await dbContext.SaveChangesAsync();

            return newVisitor;
        }
        public async Task<Visitor?> GetVisitorById(Guid id)
        {
            return await dbContext.Visitor.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<VisitorResponse?> LoginVisitor(string account, string password)
        {
            var visitor = await dbContext.Visitor.FirstOrDefaultAsync(v => v.Account == account);

            if (visitor == null)
            {
                return null;
            }

            if (!BCrypt.Net.BCrypt.Verify(password, visitor.Password))
            {
                return null;
            }

            return new VisitorResponse
            {
                Id = visitor.Id,
                Avatar = visitor.Avatar,
                CompanyName = visitor.CompanyName,
                Email = visitor.Email,
                FullName = visitor.FullName,
                PositionTitle = visitor.PositionTitle
            };
        }

        public async Task<bool> GetVisitorByAccount(string account)
        {
            return await dbContext.Visitor.AnyAsync(v => v.Account == account);
        }
    }
}
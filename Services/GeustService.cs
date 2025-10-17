using blog.Data;
using blog.DTO.Guest;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class GeustService(ApplicationDbContext dbContext) : IGuest
    {
        public async Task<List<Guest>> GetListGuest(int pageSize, int pageIndex, string? searchText, int? status, int? relationship)
        {
            IQueryable<Guest> query = dbContext.Guest.Where(g => !g.IsDelete);

            if (status != null)
            {
                query = query.Where(g => g.Status == status);
            }

            if (relationship != null)
            {
                query = query.Where(g => g.Status == relationship);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
                query = query.Where(g => EF.Functions.Like(EF.Functions.Collate(g.FullName, "SQL_Latin1_General_CP1_CI_AI"), $"%{searchText}%"));

            var listGuest = await query.OrderBy(g => g.CreateAt).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();

            return listGuest;
        }

        public async Task<Guest> CreateGuest(CGuestDto dto, Guid userId)
        {
            var newGuest = new Guest
            {
                FullName = dto.FullName,
                Relationship = dto.Relationship,
                IsRefund = dto.IsRefund,
                Status = dto.Status,
                RefundValue = dto.RefundValue,
                WeddingGiftMoney = dto.WeddingGiftMoney,
                IsDelete = false,
                CreateAt = DateTime.UtcNow,
                CreateBy = userId
            };

            await dbContext.Guest.AddAsync(newGuest);
            await dbContext.SaveChangesAsync();

            return newGuest;
        }

        public async Task<Guest?> UpdateGuest(Guest dto, Guid userId)
        {
            var guest = await dbContext.Guest.FirstOrDefaultAsync(g => g.Id == dto.Id);

            if (guest == null)
            {
                return null;
            }

            guest.FullName = dto.FullName;
            guest.IsRefund = dto.IsRefund;
            guest.RefundValue = dto.RefundValue;
            guest.Relationship = dto.Relationship;
            guest.WeddingGiftMoney = dto.WeddingGiftMoney;
            guest.Status = dto.Status;
            guest.ModifyBy = userId;
            guest.ModifyAt = DateTime.UtcNow;

            dbContext.Guest.Update(guest);
            await dbContext.SaveChangesAsync();

            return guest;
        }

        public async Task<int> CountGuest()
        {
            return await dbContext.Guest.CountAsync();
        }

        public async Task<Guest?> DeleteGuest(Guid id)
        {
            var guest = await dbContext.Guest.FirstOrDefaultAsync(g => g.Id == id);

            if (guest == null)
            {
                return guest;
            }

            dbContext.Guest.Remove(guest);
            await dbContext.SaveChangesAsync();

            return guest;
        }

        public async Task<SummaryGuest> Summary()
        {
            var result = await dbContext.Guest.Where(g => !g.IsDelete).GroupBy(g => 1).Select(g => new SummaryGuest
            {
                TotalAbsent = g.Where(g => g.Status == 0).Count(),
                TotalArrived = g.Where(g => g.Status == 1).Count(),
                TotalGuest = g.Count(),
                TotalRefund = g.Sum(g => g.RefundValue),
                TotalWeddingGiftMoney = g.Sum(g => g.WeddingGiftMoney),
                TotalGuestSend = g.Where(g => g.Status == 0 && g.WeddingGiftMoney > 0).Count()
            }).FirstOrDefaultAsync();

            return result ?? new SummaryGuest
            {
                TotalAbsent = 0,
                TotalArrived = 0,
                TotalGuest = 0,
                TotalRefund = 0,
                TotalWeddingGiftMoney = 0,
                TotalGuestSend = 0
            };
        }
    }
}
using blog.Data;
using blog.DTO.MyProfile;
using blog.Extenstion;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class CaseStudyDetailService(ApplicationDbContext dbContext) : ICaseStudyDetailRepository
    {
        public async Task<List<CaseStudyDetail>> GetAll()
        {
            return await dbContext.CaseStudyDetail.ToListAsync();
        }

        public async Task<List<CaseStudyDetail>> GetAllByCase(Guid id)
        {
            return await dbContext.CaseStudyDetail.Where(c => c.CaseStudyId == id).OrderBy(c => c.Index).ToListAsync();
        }

        public async Task<CaseStudyDetail> Create(CCaseStudyDetail dto)
        {
            var isExits = await dbContext.CaseStudy.AnyAsync(c => c.Id == dto.CaseStudyId);
            if (!isExits)
            {
                throw new HttpStatusCodeException(400, "Case study not exists");
            }

            var newItem = new CaseStudyDetail
            {
                CaseStudyId = dto.CaseStudyId,
                Content = dto.Content,
                Index = dto.Index,
                Thumbnail = dto.Thumbnail,
                Title = dto.Title,
                CreateAt = DateTime.UtcNow
            };

            await dbContext.CaseStudyDetail.AddAsync(newItem);
            await dbContext.SaveChangesAsync();

            return newItem;
        }

        public async Task<CaseStudyDetail> Update(CaseStudyDetail dto, Guid userId)
        {
            var item = await dbContext.CaseStudyDetail.FirstOrDefaultAsync(c => c.Id == dto.Id);
            if (item == null)
            {
                throw new HttpStatusCodeException(400, "Case study not exists");
            }


            item.Content = dto.Content;
            item.Index = dto.Index;
            item.Thumbnail = dto.Thumbnail;
            item.Title = dto.Title;
            item.ModifyAt = DateTime.UtcNow;
            item.ModifyBy = userId;

            dbContext.CaseStudyDetail.Update(item);
            await dbContext.SaveChangesAsync();

            return item;
        }

        public async Task<CaseStudyDetail> DeleteById(Guid id)
        {
            var ctd = await dbContext.CaseStudyDetail.FirstOrDefaultAsync(c => c.Id == id) ?? throw new HttpStatusCodeException(400, "Case study not exist");
            dbContext.CaseStudyDetail.Remove(ctd);
            await dbContext.SaveChangesAsync();

            return ctd;
        }
    }
}
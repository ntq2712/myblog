using blog.Data;
using blog.DTO.MyProfile;
using blog.Extenstion;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class CaseStudyService(ApplicationDbContext dbContext) : ICaseStudyService
    {
        public async Task<List<CaseTudyFullValue>> GetList()
        {
            var list = await dbContext.CaseStudy.Where(c => c.IsDelete == false).ToListAsync();
            var types = await dbContext.CaseStudyType.Where(c => c.IsDelete == false).ToListAsync();

            var result = (
                from t in types
                join c in list on t.Id equals c.Type
                orderby t.UiMetadata.Index
                select new CaseTudyFullValue
                {
                    Id = c.Id,
                    CaseStudyType = t,
                    Title = c.Title,
                    Introduction = c.Introduction,
                    Thumbnail = c.Thumbnail,
                }
            ).ToList();


            return result;
        }

        public async Task<CaseStudy> CreateCaseStudy(CCaseStudy dto)
        {
            var type = await dbContext.CaseStudyType.FirstOrDefaultAsync(t => t.Id == dto.Type);

            if (type == null)
            {
                throw new HttpStatusCodeException(400, "Case study type not exist.");
            }

            var caseTudy = new CaseStudy();
            caseTudy.Title = dto.Title;
            caseTudy.Introduction = dto.Introduction;
            caseTudy.Thumbnail = dto.Thumbnail;
            caseTudy.Type = dto.Type;
            caseTudy.CreateAt = DateTime.Now;

            await dbContext.CaseStudy.AddAsync(caseTudy);
            await dbContext.SaveChangesAsync();

            return caseTudy;
        }

        public async Task<CaseStudy> UpdateCaseStudy(CaseStudy dto, Guid userId)
        {
            var caseTudy = await dbContext.CaseStudy.FirstOrDefaultAsync(t => t.Id == dto.Id);

            if (caseTudy == null)
            {
                throw new HttpStatusCodeException(400, "Case study not exist.");
            }

            caseTudy.Title = dto.Title;
            caseTudy.Introduction = dto.Introduction;
            caseTudy.Thumbnail = dto.Thumbnail;
            caseTudy.ModifyAt = DateTime.Now;
            caseTudy.ModifyBy = userId;

            dbContext.CaseStudy.Update(caseTudy);
            await dbContext.SaveChangesAsync();

            return caseTudy;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var caseTudy = await dbContext.CaseStudy.FirstOrDefaultAsync(c => c.Id == id);

            if (caseTudy == null)
            {
                return false;
            }

            dbContext.CaseStudy.Remove(caseTudy);
            await dbContext.SaveChangesAsync();

            return true;
        }

    }
}
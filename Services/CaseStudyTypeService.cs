using blog.Data;
using blog.DTO.MyProfile;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class CaseStudyTypeService(ApplicationDbContext applicationDb) : ICaseStudyTypeService
    {
        public async Task<List<CaseStudyType>> GetList()
        {
            return await applicationDb.CaseStudyType.OrderBy(t => t.UiMetadata.Index).ToListAsync();
        }
        public async Task<CaseStudyType> CreateCaseStudyType(CCaseStudyType cCaseStudyType)
        {
            var newType = new CaseStudyType
            {
                Name = cCaseStudyType.Name
            };
            newType.UiMetadata.Color = cCaseStudyType.Color;
            newType.UiMetadata.ColorLight = cCaseStudyType.ColorLight;
            newType.UiMetadata.Index = cCaseStudyType.Index;
            newType.UiMetadata.Icon = cCaseStudyType.Icon;
            newType.CreateAt = DateTime.Now;

            await applicationDb.CaseStudyType.AddAsync(newType);
            await applicationDb.SaveChangesAsync();

            return newType;
        }

        public async Task<bool> Delete()
        {
            await applicationDb.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var isExitsCaseStudiesUseType = await applicationDb.CaseStudy.AnyAsync(c => c.Type == id);

            if (isExitsCaseStudiesUseType)
            {
                return false;
            }

            var type = await applicationDb.CaseStudyType.FirstOrDefaultAsync(t => t.Id == id);

            if (type == null)
            {
                return false;
            }

            applicationDb.CaseStudyType.Remove(type);

            await applicationDb.SaveChangesAsync();

            return true;
        }
    }
}
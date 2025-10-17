using blog.Data;
using blog.DTO.MyProfile;
using blog.Extenstion;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class WorkExperience(ApplicationDbContext dbContext) : IWorkExperienceRepository
    {
        public async Task<MyWorkExperience> AddAsync(CWorkExperience workExperience)
        {
            var newItem = new MyWorkExperience
            {
                CompanyName = workExperience.CompanyName,
                Introduction = workExperience.Introduction,
                Thumbnail = workExperience.Thumbnail,
                Title = workExperience.Title,
                StartDate = workExperience.StartDate,
                EndDate = workExperience.EndDate,
            };

            await dbContext.MyWorkExperience.AddAsync(newItem);
            await dbContext.SaveChangesAsync();

            return newItem;
        }

        public async Task<List<MyWorkExperience>> GetAllMyWork()
        {
            return await dbContext.MyWorkExperience.OrderBy(w => w.StartDate).ToListAsync();
        }

        public async Task<MyWorkExperience?> GetById(Guid id)
        {
            return await dbContext.MyWorkExperience.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<MyWorkExperience> Update(MyWorkExperience workExperience)
        {
            var existsItem = await GetById(workExperience.Id);

            if (existsItem == null)
            {
                throw new HttpStatusCodeException(400, "Work experience not exists");
            }

            existsItem.CompanyName = workExperience.CompanyName;
            existsItem.Introduction = workExperience.Introduction;
            existsItem.Title = workExperience.Title;
            existsItem.Thumbnail = workExperience.Thumbnail;
            existsItem.EndDate = workExperience.EndDate;
            existsItem.StartDate = workExperience.StartDate;

            dbContext.MyWorkExperience.Update(existsItem);
            await dbContext.SaveChangesAsync();

            return existsItem;
        }

        public async Task<bool> DeleteById(Guid id)
        {
            var existsItem = await GetById(id);

            if (existsItem == null)
            {
                throw new HttpStatusCodeException(400, "Work experience not exists");
            }

            dbContext.Remove(existsItem);
            await dbContext.SaveChangesAsync();
            return true;

        }
    }
}
using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface IWorkExperienceRepository
    {
        public Task<MyWorkExperience> AddAsync(CWorkExperience workExperience);
        public Task<List<MyWorkExperience>> GetAllMyWork();
        public Task<MyWorkExperience> Update(MyWorkExperience workExperience);
        public Task<bool> DeleteById(Guid id);
    }
}

using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface ICaseStudyTypeService
    {
        public Task<CaseStudyType> CreateCaseStudyType(CCaseStudyType cCaseStudyType);
        public Task<bool> Delete();
        public Task<List<CaseStudyType>> GetList();
        public Task<bool> DeleteById(Guid id);
    }
}
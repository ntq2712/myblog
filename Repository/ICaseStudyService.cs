
using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface ICaseStudyService
    {
        public Task<List<CaseTudyFullValue>> GetList();
        public Task<CaseStudy> CreateCaseStudy(CCaseStudy dto);
        public Task<bool> DeleteById(Guid id);
    }
}
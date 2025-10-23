
using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface ICaseStudyDetailRepository
    {
        public Task<List<CaseStudyDetail>> GetAll();
        public Task<List<CaseStudyDetail>> GetAllByCase(Guid id);
        public Task<CaseStudyDetail> Create(CCaseStudyDetail dto);
        public Task<CaseStudyDetail> Update(CaseStudyDetail dto);
        public Task<CaseStudyDetail> DeleteById(Guid id);
    }
}
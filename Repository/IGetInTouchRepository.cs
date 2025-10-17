
using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface IGetInTouchRepository
    {
        public Task<List<GetInTouch>> GetAllInTouches();
        public Task<GetInTouch> GetInTouch(CGetInTouch dto);
    }
}
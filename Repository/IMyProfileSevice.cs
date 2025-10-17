
using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface IMyProfileSevice
    {
        public Task<MyProfile> CreateProfile(CMyProfile profile);
        public Task<MyProfile> GetProfile();
        public Task<bool> DeleteAll();
        public Task<MyProfile> UpdateProfile(CMyProfile profile);
    }
}
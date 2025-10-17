using AutoMapper;
using blog.Data;
using blog.DTO.MyProfile;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class MyProfileSevice(ApplicationDbContext dbContext, IMapper mapper) : IMyProfileSevice
    {
        public async Task<MyProfile> CreateProfile(CMyProfile profile)
        {
            var newProfile = mapper.Map<MyProfile>(profile);
            newProfile.Id = Guid.Parse("00000000-0000-0000-0000-000000000001");
            newProfile.CreateBy = Guid.Parse("00000000-0000-0000-0000-000000000001");
            newProfile.CreateAt = DateTime.UtcNow;

            await dbContext.MyProfiles.AddAsync(newProfile);
            await dbContext.SaveChangesAsync();

            return newProfile;
        }
        public async Task<MyProfile> UpdateProfile(CMyProfile profile)
        {
            var newProfile = await GetProfile();
            newProfile.Avatar = profile.Avatar;
            newProfile.BirthDay = profile.BirthDay;
            newProfile.FullName = profile.FullName;
            newProfile.MySkills = profile.MySkills;

            dbContext.MyProfiles.Update(newProfile);
            await dbContext.SaveChangesAsync();

            return newProfile;
        }

        public async Task<MyProfile> GetProfile()
        {
            return await dbContext.MyProfiles.FirstOrDefaultAsync(x => x.Id == Guid.Parse("00000000-0000-0000-0000-000000000001")) ?? new MyProfile();
        }

        public async Task<bool> DeleteAll()
        {
            var list = await dbContext.MyProfiles.ToListAsync();

            dbContext.RemoveRange(list);
            await dbContext.SaveChangesAsync();

            return true;
        }
    }
}
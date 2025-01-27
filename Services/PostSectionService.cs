using AutoMapper;
using blog.Data;
using blog.DTO.PostSection;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class PostSectionService(ApplicationDbContext dbContext, IMapper mapper) : IPostSection
    {
        public async Task<List<PostSection>> GetAll(PageResponse filter)
        {
            var postSections = await dbContext.PostSection.Where(e => e.IsDelete == true).Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageIndex * filter.PageSize).Where(s => string.IsNullOrEmpty(filter.SearchText) || s.Heading.Contains(filter.SearchText)).ToListAsync();

            if (postSections == null)
            {
                throw new KeyNotFoundException("Don't find post section table");
            }

            return postSections;
        }

        public async Task<PostSection> Create(CreatePostSection _postSection){
            var postSection = mapper.Map<PostSection>(_postSection);

            if(postSection == null) {
                throw new KeyNotFoundException("Create post section fail: [Map]");
            }

            await dbContext.PostSection.AddAsync(postSection);
            await dbContext.SaveChangesAsync();

            return postSection;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DTO.PostSection;
using blog.Helper;
using blog.Model;

namespace blog.Repository
{
    public interface IPostSection
    {
        public Task<List<PostSection>> GetAll(PageResponse _filter);
        public Task<PostSection> Create(CreatePostSection _postSection);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DTO.Guest;
using blog.Model;

namespace blog.Repository
{
    public interface IGuest
    {
        public Task<List<Guest>> GetListGuest(int pageSize, int pageIndex, string? searchText, int? status, int? relationship);
        public Task<Guest> CreateGuest(CGuestDto dto, Guid userId);
        public Task<Guest?> UpdateGuest(Guest dto, Guid userId);
        public Task<int> CountGuest();
        public Task<Guest?> DeleteGuest(Guid id);
        public Task<SummaryGuest> Summary();
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DTO.Visitor;
using blog.Model;

namespace blog.Repository
{
    public interface IVisitorRepository
    {
        public Task<List<Visitor>> GetVisitors();
        public Task<Visitor> RegiteVisitor(CVisitor visitor);
        public Task<bool> GetVisitorByAccount(string account);
        public Task<Visitor?> GetVisitorById(Guid id);
        public Task<VisitorResponse?> LoginVisitor(string account, string password);
    }
}
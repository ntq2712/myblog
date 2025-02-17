using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Repository
{
    public interface IEmailService
    {
        public Task<bool> SendMail(string to, string subject, string body);
    }
}
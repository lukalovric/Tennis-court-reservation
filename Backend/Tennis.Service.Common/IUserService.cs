using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Model;

namespace Tennis.Service.Common
{
    public interface IUserService
    {
        public Task<User> CreateUserAsync(User request);
        public Task<User> GetUserLoginAsync(User request);
    }
}

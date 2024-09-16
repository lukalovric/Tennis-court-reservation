using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Model;

namespace Tennis.Repository.Common
{
    public interface IUserRepository
    {
        public Task<User> CreateUserAsync(User user);
        public Task<User> GetUserLoginAsync(User user);
    }
}

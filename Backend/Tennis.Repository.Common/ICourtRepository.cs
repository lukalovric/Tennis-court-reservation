using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Model;

namespace Tennis.Repository.Common
{
    public interface ICourtRepository
    {
        Task<IEnumerable<Court>> GetAllCourtsAsync();
        Task AddCourtAsync(Court court);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tennis.Model;

namespace Tennis.Service.Common
{
    public interface ICourtService
    {
        Task<IEnumerable<Court>> GetAllCourtsAsync();
        Task AddCourtAsync(Court court);
    }
}

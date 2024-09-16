using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tennis.Service.Common
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllReservationsAsync();
        Task AddReservationAsync(Reservation reservation);
        Task<bool> DeleteReservationAsync(int id);

    }
}

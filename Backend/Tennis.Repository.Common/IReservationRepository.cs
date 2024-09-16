using System.Collections.Generic;
using System.Threading.Tasks;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task AddReservationAsync(Reservation reservation);
    Task<bool> DeleteReservationAsync(int id);

}

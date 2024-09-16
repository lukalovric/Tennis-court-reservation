using System.Collections.Generic;
using System.Threading.Tasks;
using Tennis.Service.Common;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;

    public ReservationService(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _reservationRepository.GetAllReservationsAsync();
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _reservationRepository.AddReservationAsync(reservation);
    }
    public async Task<bool> DeleteReservationAsync(int id)
    {
        return await _reservationRepository.DeleteReservationAsync(id);
    }
}
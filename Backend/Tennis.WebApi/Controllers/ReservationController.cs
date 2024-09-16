using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tennis.Service.Common;

[Route("api/[controller]")]
[ApiController]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReservations()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();
        return Ok(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(Reservation reservation)
    {
        await _reservationService.AddReservationAsync(reservation);
        return CreatedAtAction(nameof(GetReservations), new { id = reservation.Id }, reservation);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReservation(int id)
    {
        var success = await _reservationService.DeleteReservationAsync(id);
        if (!success)
        {
            return BadRequest("Failed to delete the reservation.");
        }

        return NoContent();
    }
}

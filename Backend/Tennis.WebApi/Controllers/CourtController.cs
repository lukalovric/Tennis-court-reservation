using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tennis.Model;
using Tennis.Service.Common;

[Route("api/[controller]")]
[ApiController]
public class CourtController : ControllerBase
{
    private readonly ICourtService _courtService;

    public CourtController(ICourtService courtService)
    {
        _courtService = courtService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCourts()
    {
        var courts = await _courtService.GetAllCourtsAsync();
        return Ok(courts);
    }

    [HttpPost]
    public async Task<IActionResult> AddCourt(Court court)
    {
        await _courtService.AddCourtAsync(court);
        return CreatedAtAction(nameof(GetCourts), new { id = court.Id }, court);
    }
}
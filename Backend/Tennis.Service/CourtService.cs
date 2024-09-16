using System.Collections.Generic;
using System.Threading.Tasks;
using Tennis.Model;
using Tennis.Repository.Common;
using Tennis.Service.Common;

public class CourtService : ICourtService
{
    private readonly ICourtRepository _courtRepository;

    public CourtService(ICourtRepository courtRepository)
    {
        _courtRepository = courtRepository;
    }

    public async Task<IEnumerable<Court>> GetAllCourtsAsync()
    {
        return await _courtRepository.GetAllCourtsAsync();
    }

    public async Task AddCourtAsync(Court court)
    {
        await _courtRepository.AddCourtAsync(court);
    }
}
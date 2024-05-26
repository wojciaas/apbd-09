using apbd_09_orm.Repositories;

namespace apbd_09_orm.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    
    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public async Task<IEnumerable<object>> GetTripsAsync(int pageNum, int pageSize)
    {
        int tripsCount = await _tripRepository.GetTripsCountAsync();
        return new []
        {
            new
            {
                pageNum,
                pageSize,
                allPages = tripsCount,
                trips = await _tripRepository.GetTripsAsync(pageNum, pageSize)
            }
        };
    }
}
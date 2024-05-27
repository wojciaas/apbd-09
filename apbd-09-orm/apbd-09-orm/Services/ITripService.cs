using apbd_09_orm.RequestModels;

namespace apbd_09_orm.Services;

public interface ITripService
{
    Task<IEnumerable<object>> GetTripsAsync(int pageNum, int pageSize);
    public Task AssignClientToTripAsync(int idTrip, RequestClientTripDTO requestClientTripDto);
}
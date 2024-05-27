using apbd_09_orm.Entities;
using apbd_09_orm.RequestModels;
using apbd_09_orm.ResponseModels;

namespace apbd_09_orm.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<ResponseTripDTO>> GetTripsAsync(int pageNum = 1, int pageSize = 10);
    public Task<int> GetTripsCountAsync();
    public Task AssignClientToTripAsync(Client client, Trip trip, DateTime? paymentDate);
    public Task<Trip?> GetTripAsync(int id);
}
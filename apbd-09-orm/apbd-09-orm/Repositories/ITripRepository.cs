using apbd_09_orm.ResponseModels;

namespace apbd_09_orm.Repositories;

public interface ITripRepository
{
    public Task<IEnumerable<ResponseTripDTO>> GetTripsAsync(int pageNum = 1, int pageSize = 10);
    public Task<int> GetTripsCountAsync();
}
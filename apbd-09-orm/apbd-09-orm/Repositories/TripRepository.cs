using apbd_09_orm.Context;
using apbd_09_orm.Entities;
using apbd_09_orm.ResponseModels;
using Microsoft.EntityFrameworkCore;

namespace apbd_09_orm.Repositories;

public class TripRepository : ITripRepository
{
    private readonly ApbdContext _context;
    
    public TripRepository(ApbdContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ResponseTripDTO>> GetTripsAsync(int pageNum = 1, int pageSize = 10)
    {
        return await _context.Trips
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .OrderByDescending(t => t.DateFrom)
            .Select(t => new ResponseTripDTO(
                t.Name,
                t.Description,
                t.DateFrom,
                t.DateTo,
                t.MaxPeople,
                t.IdCountries.Select(c => new ResponseCountryDTO(c.Name)),
                t.ClientTrips.Select(ct => new ResponseClientDTO(
                    ct.IdClientNavigation.FirstName,
                    ct.IdClientNavigation.LastName
                    ))
                ))
            .ToListAsync();

    }
    
    public async Task<int> GetTripsCountAsync()
    {
        return await _context.Trips.CountAsync();
    }
    
    public async Task<Trip?> GetTripAsync(int id)
    {
        return await _context.Trips
            .FirstOrDefaultAsync(t => t.IdTrip == id);
    }
    
    public async Task AssignClientToTripAsync(Client client, Trip trip, DateTime? paymentDate)
    {
        await _context.ClientTrips.AddAsync(new ClientTrip()
        {
            IdClient = client.IdClient,
            IdTrip = trip.IdTrip,
            PaymentDate = paymentDate,
            RegisteredAt = DateTime.Now
        });
        await _context.SaveChangesAsync();
    }
}
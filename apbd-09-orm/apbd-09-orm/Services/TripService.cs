using apbd_09_orm.Entities;
using apbd_09_orm.Repositories;
using apbd_09_orm.RequestModels;

namespace apbd_09_orm.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IClientService _clientService;
    
    public TripService(ITripRepository tripRepository, IClientService clientService)
    {
        _tripRepository = tripRepository;
        _clientService = clientService;
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

    public async Task AssignClientToTripAsync(int id, RequestClientTripDTO requestClientTripDto)
    {
        (
            string firstName, 
            string lastName, 
            string email, 
            string telephone, 
            string pesel, 
            int idTrip, 
            string tripName, 
            DateTime? paymentDate
        ) = requestClientTripDto;

        Client? client = await _clientService.GetClientByPeselAsync(pesel);
        EnsureClientWithPeselNotExist(client, pesel);
        client = new Client()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Telephone = telephone,
            Pesel = pesel
        };
        Trip? trip = await _tripRepository.GetTripAsync(id);
        EnsureTripExists(trip, id);
        EnsureTripWasNotAlreadyStarted(trip!, id);
        EnsureClientIsNotAssignedToTrip(client, trip!);
        await _tripRepository.AssignClientToTripAsync(client, trip!, paymentDate);
    }
    
    private void EnsureClientWithPeselNotExist(Client? client, string pesel)
    {
        if (client != null)
        {
            throw new Exception($"Client with PESEL={pesel} already exists");
        }
    }
    
    private void EnsureClientIsNotAssignedToTrip(Client client, Trip trip)
    {
        if (client.ClientTrips.Any(ct => ct.IdTrip == trip.IdTrip))
        {
            throw new Exception($"Client with id={client.IdClient} is already assigned to trip with id={trip.IdTrip}");
        }
    }
    
    private void EnsureTripExists(Trip? trip, int id)
    {
        if (trip == null)
        {
            throw new Exception($"Trip with id={id} was not found.");
        }
    }
    
    private void EnsureTripWasNotAlreadyStarted(Trip trip, int id)
    {
        if (trip.DateFrom < DateTime.Now)
        {
               throw new Exception($"Trip with id={id} was already started.");
        }
    }
}
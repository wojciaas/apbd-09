using apbd_09_orm.Entities;
using apbd_09_orm.Exceptions;
using apbd_09_orm.Repositories;

namespace apbd_09_orm.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task RemoveClientAsync(int id)
    {
        Client? client = await _clientRepository.GetClientAsync(id);
        EnsureClientExists(client, id);
        EnsureClientHasNoAssignedTrips(client!);
        await _clientRepository.RemoveClientAsync(client!);
    }
    
    private static void EnsureClientExists(Client? client, int id)
    {
        if (client == null)
        {
            throw new DomainException($"Client with id={id} was not found.");
        }
    }

    private static void EnsureClientHasNoAssignedTrips(Client client)
    {
        if (client.ClientTrips.Count > 0)
        {
            throw new DomainException("Cannot remove client with assigned trips.");
        }
    }

    public async Task<Client?> GetClientByPeselAsync(string pesel)
    {
        return await _clientRepository.GetClientByPeselAsync(pesel);
    }
    
    public async Task AddClientAsync(Client client)
    {
        await _clientRepository.AddClientAsync(client);
    }
}
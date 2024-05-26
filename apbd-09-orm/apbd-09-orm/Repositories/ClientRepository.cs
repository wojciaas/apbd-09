using apbd_09_orm.Context;
using apbd_09_orm.Entities;
using Microsoft.EntityFrameworkCore;

namespace apbd_09_orm.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApbdContext _context;
    
    public ClientRepository(ApbdContext context)
    {
        _context = context;
    }

    public async Task RemoveClientAsync(Client client)
    {
        _context.Clients.Attach(client);
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
    
    public async Task<Client?> GetClientAsync(int id)
    {
        return await _context.Clients.Include(c => c.ClientTrips)
                                        .FirstOrDefaultAsync(c => c.IdClient == id);;
    }
}
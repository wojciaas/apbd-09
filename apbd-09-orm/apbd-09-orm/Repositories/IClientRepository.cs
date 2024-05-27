using apbd_09_orm.Entities;

namespace apbd_09_orm.Repositories;

public interface IClientRepository
{
    Task RemoveClientAsync(Client client);
    Task<Client?> GetClientAsync(int id);
    Task<Client?> GetClientByPeselAsync(string pesel);
    Task AddClientAsync(Client client);
}
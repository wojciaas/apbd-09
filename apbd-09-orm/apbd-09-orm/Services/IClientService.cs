using apbd_09_orm.Entities;

namespace apbd_09_orm.Services;

public interface IClientService
{
    Task RemoveClientAsync(int id);
    Task<Client?> GetClientByPeselAsync(string pesel);
    Task AddClientAsync(Client client);
}
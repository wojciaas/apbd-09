using apbd_09_orm.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_09_orm.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveClientAsync(int id)
    {
        try
        {
            await _clientService.RemoveClientAsync(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok($"Client with id={id} removed successfully.");
    }
}
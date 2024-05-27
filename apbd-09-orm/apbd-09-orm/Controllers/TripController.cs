using apbd_09_orm.RequestModels;
using apbd_09_orm.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_09_orm.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripController : ControllerBase
{
    private readonly ITripService _tripService;
    
    public TripController(ITripService tripService)
    {
        _tripService = tripService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
    {
        return Ok(await _tripService.GetTripsAsync(pageNum, pageSize));
    }
    
    [HttpPost("{idTrip:int}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, RequestClientTripDTO requestClientTripDto)
    {
        try
        {
            await _tripService.AssignClientToTripAsync(idTrip, requestClientTripDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}
using Microsoft.AspNetCore.Mvc;
using reservations_api.DTOs.Requests;
using reservations_api.DTOs.Responses;
using reservations_api.Services;




namespace reservations_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{

  private readonly IReservationService _reservationService;


  public ReservationsController(IReservationService reservationService)
  {
    _reservationService = reservationService;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
  {
    if (!ModelState.IsValid)
    {
      return ValidationProblem(ModelState);
    }

    try
    {
      var createdReservation = await _reservationService.CreateAsync(request);
      return CreatedAtAction(
          nameof(Create),
          createdReservation);
    }
    catch (InvalidOperationException ex)
    {
      if (ex.Message.Contains("StartTime"))
      {
        return BadRequest(new { message = ex.Message });
      }

      if (ex.Message.Contains("Time conflict"))
      {
        return Conflict(new { message = ex.Message });
      }

      throw;
    }
  }

  //eliminar una reservacion
  [HttpDelete]
  [Route("{id}")]
  public async Task<IActionResult> DeleteReservation(Guid id)
  {
    var result = await _reservationService.DeleteReservation(id);

    if (!result)
      return NotFound();

    return NoContent();
  }

  //obtener por filtrado de la fecha de la reservacion
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ReservationResponse>>> GetByDate([FromQuery] DateTime date)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var reservations = await _reservationService.GetByDateReservation(date);
    return Ok(reservations);
  }





}
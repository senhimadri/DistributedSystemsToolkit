using FormulaAirline.API.Models;
using FormulaAirline.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMessageProduser messageProduser;
        private readonly ILogger<BookingController> logger;

        public static readonly List<Booking> _booking = new();
        public BookingController(IMessageProduser _messageProduser, ILogger<BookingController> _logger)
        {
            messageProduser = _messageProduser;
            logger = _logger;
        }

        [HttpPost]
        public IActionResult CreatingBooking(Booking newBooking)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _booking.Add(newBooking);

            messageProduser.SendingMessages<Booking>(newBooking);

            return Ok();
        }
    }
}

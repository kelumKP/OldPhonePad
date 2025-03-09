using Microsoft.AspNetCore.Mvc;
using OldPhonePad.Application.Interfaces;

namespace OldPhonePad.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhonePadController : ControllerBase
    {
        private readonly IPhonePadService _phonePadService;

        public PhonePadController(IPhonePadService phonePadService)
        {
            _phonePadService = phonePadService ?? throw new ArgumentNullException(nameof(phonePadService));
        }

        [HttpGet("decode")]
        public IActionResult Decode([FromQuery] string input)
        {
            if (string.IsNullOrEmpty(input))
                return BadRequest("Input cannot be empty");

            var result = _phonePadService.DecodeInput(input);
            return Ok(result);
        }
    }
}
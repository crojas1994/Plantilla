using Microsoft.AspNetCore.Mvc;
using VentanillaUnica.Tramites.Application.IServices;
using VentanillaUnica.Tramites.Dtos;

namespace VentanillaUnica.Tramites.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        [HttpGet("{identifier}")]
        [Produces(typeof(ParameterDto))]
        public async Task<IActionResult> GetByIdentifierAsync(string identifier,
            [FromServices] IParameterAplicationService parameterAplicationService)
        {
            return Ok(await parameterAplicationService.GetByIdentifierAsync(identifier));
        }
    }
}

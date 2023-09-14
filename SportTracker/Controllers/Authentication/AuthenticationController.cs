using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportTracker.Models;
using SportTracker.Services.Commands;
using SportTracker.Services.Queries;

namespace SportTracker.Controllers.Authentication
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {

                var result = await _mediator.Send(new LoginQuery(model));
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        result = result.ActualValue
                    });
                }

                return BadRequest(result.Error.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Signup([FromBody] SignupModel model)
        {
            try
            {

                var result = await _mediator.Send(new SignupCommand(model));
                if (result.IsSuccess)
                {
                    return Ok(new
                    {
                        result = result.ActualValue
                    });
                }

                return BadRequest(result.Error.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        
    }
}

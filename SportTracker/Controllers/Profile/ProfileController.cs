using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportTracker.Models;
using SportTracker.Services.Commands;
using SportTracker.Services.Queries;

namespace SportTracker.Controllers.Profile
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new GetProfileQuery(userId));
                if (result.IsSuccess) return Ok(result.ActualValue);

                return BadRequest(result.Error.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
         
        }
        
        
        [HttpPost]
        public async Task<IActionResult> EditProfile([FromBody] SignupModel model)
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new EditProfileCommand(model , userId));
                if (result.IsSuccess) return Ok(result.ActualValue);

                return BadRequest(result.Error.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
         
        }


        [HttpPost]
        [Authorize]
        [RequestSizeLimit(2 * 1024 * 1024)]
        public async Task<IActionResult> UploadProfilePicture(  IFormFile file)
        {
            var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var validData = Utilities.Utilities.IsImage(file);
            if (!validData)
            {
                return BadRequest("invalid file format, not an image");
            }
            var result = await _mediator.Send(new UploadProfilePictureCommand(userId, file));

            var url =
                $"{Request.Scheme}://{Request.Host}/profilePicture/{result.ActualValue}";
            
            return Ok(url);
        }
       
    }
}

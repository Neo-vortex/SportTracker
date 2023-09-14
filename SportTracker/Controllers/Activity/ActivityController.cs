using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportTracker.Models.Dto;
using SportTracker.Services.Commands;
using SportTracker.Services.Queries;

namespace SportTracker.Controllers.Activity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ApiVersion("0.1")]
    public class ActivityController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ActivityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddActivity([FromBody] UserActivityDto activityDto)
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new AddActivityCommand(activityDto, userId));
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
        
        
        
        [Authorize]
        [HttpPatch]
        public async Task<IActionResult> SyncActivities([FromBody] List<UserActivityDto> activityDto)
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new SyncActivitiesCommand(activityDto, userId));
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
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetMyActivities()
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new GetMyActivitiesQuery(userId));
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
        
        
                
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetHighestScoreUser([FromQuery] int count)
        {
            try
            {
                var userId = HttpContext.User?.Claims.SingleOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _mediator.Send(new GetHighestScoreUserQuery(count));
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

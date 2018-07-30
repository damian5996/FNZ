using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FNZ.WebApi.Controllers
{
    [Route("Requests")]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPatch("{requestId}/Refuse")]
        public async Task<IActionResult> RefuseRequest(long requestId)
        {
            var result = await _requestService.RefuseRequest(requestId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{requestId}/Accept")]
        public async Task<IActionResult> AcceptRequest(long requestId)
        {
            var result = await _requestService.AcceptRequest(requestId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}

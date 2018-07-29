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

        [HttpPatch("{id}/Refuse")]
        public async Task<IActionResult> RefuseRequest(long id)
        {
            var result = await _requestService.RefuseRequest(id);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


    }
}

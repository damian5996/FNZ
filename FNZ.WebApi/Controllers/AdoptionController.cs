using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using FNZ.Share.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace FNZ.WebApi.Controllers
{
    [Route("Adoption")]
    public class AdoptionController : Controller
    {
        private readonly IAdoptionService _adoptionService;

        public AdoptionController(IAdoptionService adoptionService)
        {
            _adoptionService = adoptionService;
        }
        
        [HttpPost("SendApplication/{animalId}")]
        public async Task<IActionResult> SendApplication([FromBody] ApplicationBindingModel application, long animalId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adoptionService.SendApplication(application, animalId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> MarkAsAdopted([FromForm] long animalId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _adoptionService.MarkAsAdopted(animalId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

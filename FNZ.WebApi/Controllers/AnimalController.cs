using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using FNZ.Share.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace FNZ.WebApi.Controllers
{
    [Route("Animals")]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;

        public AnimalController(IAnimalService animalService)
        {
            _animalService = animalService;
        }

        //[HttpPost("Add")]
        //public async Task<IActionResult> AddAnimal([FromBody] AnimalBindingModel animalBindingModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //   // var result = await _animalService.AddAnimal(animalBindingModel);

        //    //if (result.ErrorOccurred)
        //    //{
        //    //    return BadRequest(result);
        //    //}

        //    //return Ok(result);
        //}
    }
}

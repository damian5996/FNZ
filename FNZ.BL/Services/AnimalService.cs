using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services
{
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        //public async Task<ResponseDto<BaseModelDto>> AddAnimal(AnimalBindingModel animalToAdd)
        //{
        //    var result = new ResponseDto<BaseModelDto>();
        //    var animal = Mapper.Map<Animal>(animalToAdd);
        //    var post = 
        //}
    }
}

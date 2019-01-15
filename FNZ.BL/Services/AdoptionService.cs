using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Consts;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services
{
    public class AdoptionService : IAdoptionService
    {
        private readonly IAdoptionRepository _adoptionRepository;
        private readonly IEmailService _emailService;
        private readonly IAnimalRepository _animalRepository;
        private readonly IPostRepository _postRepository;

        public AdoptionService(IAdoptionRepository adoptionRepository, IEmailService emailService, IAnimalRepository animalRepository, IPostRepository postRepository)
        {
            _adoptionRepository = adoptionRepository;
            _emailService = emailService;
            _animalRepository = animalRepository;
            _postRepository = postRepository;
        }

        public async Task<ResponseDto<BaseModelDto>> SendApplication(ApplicationBindingModel applicationToSend, long animalId)
        {
            var result = new ResponseDto<BaseModelDto>();
            var application = Mapper.Map<Application>(applicationToSend);

            var animal = _animalRepository.Get(x => x.Id == animalId);

            application.Animal = animal;

            var insertApplication = await _adoptionRepository.InsertAsync(application);
            if (!insertApplication)
            {
                result.Errors.Add(ErrorsKeys.post_Adding, ErrorsValues.post_AddingRequest);
                return result;
            }

            var postId = _postRepository.Get(x => x.Animal.Id == animal.Id).Id;
            await _emailService.SendEmail("damianjacyna59@gmail.com", "Fundacja Niechcianych Zwierząt - złożono wniosek o adopcję",
                "Dane składającego:\n" +
                "Imię i nazwisko: " + application.FirstName + " " + application.LastName + "\n" +
                "Email: " + application.Email + "\n" +
                "Numer telefonu: " +application.PhoneNumber + "\n" +
                "Adres zamieszkania: " + application.Address + "\n" +
                "Link do postu ze zwierzęciem: http://localhost:8010/app/#!/news/" + postId);


            return result;
        }

        public async Task<ResponseDto<BaseModelDto>> MarkAsAdopted(long animalId)
        {
            var result = new ResponseDto<BaseModelDto>();
            var animal = _animalRepository.Get(a => a.Id == animalId);
            animal.AdoptionDate = DateTime.Now;
            var updateAnimal = await _animalRepository.SaveAsync();
            var post = _postRepository.Get(p => p.Animal !=null && p.Animal.Id == animalId);
            post.Category = Enums.Category.FullfilledDreams;
            var updatePost = await _postRepository.SaveAsync();
            return result;
        }

    }
}

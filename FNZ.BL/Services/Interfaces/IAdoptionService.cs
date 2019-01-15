using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.BindingModels;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services.Interfaces
{
    public interface IAdoptionService
    {
        Task<ResponseDto<BaseModelDto>> SendApplication(ApplicationBindingModel applciation, long animalId);
        Task<ResponseDto<BaseModelDto>> MarkAsAdopted(long animalId);
    }
}

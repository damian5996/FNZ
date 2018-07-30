using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services.Interfaces
{
    public interface IRequestService
    {
        Task<ResponseDto<BaseModelDto>> RefuseRequest(long requestId);
        Task<ResponseDto<BaseModelDto>> AcceptRequest(long requestId);
    }
}

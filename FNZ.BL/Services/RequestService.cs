using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Consts;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<ResponseDto<BaseModelDto>> RefuseRequest(long id)
        {
            var result = new ResponseDto<BaseModelDto>();
            var request = _requestRepository.Get(r => r.Id == id);
            if (request == null)
            {
                result.Errors.Add(ErrorsKeys.request_Refuse, ErrorsValues.request_NotExist);
                return result;
            }
            request.RefusalDate = DateTime.Now;
            var saveRequest = await _requestRepository.SaveAsync();
            if (!saveRequest)
            {
                result.Errors.Add(ErrorsKeys.request_Refuse, ErrorsValues.request_Refuse);
                return result;
            }

            return result;
        }
    }
}

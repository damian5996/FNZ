using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IPostRepository _postRepository;

        public RequestService(IRequestRepository requestRepository, IPostRepository postRepository)
        {
            _requestRepository = requestRepository;
            _postRepository = postRepository;
        }

        public ResponseDto<RequestsListDto> GetAllRequests(RequestParameterBindingModel parameters)
        {
            var result = new ResponseDto<RequestsListDto>()
            {
                Object = new RequestsListDto()
            };
            List<Request> requests = new List<Request>();
            if (parameters.ShowAccepted && !parameters.ShowRefused)
            {
                requests = _requestRepository.GetAll(r => r.AcceptanceDate != null);
            }
            else if (parameters.ShowRefused && !parameters.ShowAccepted)
            {
                requests = _requestRepository.GetAll(r => r.RefusalDate != null);
            }
            else if (parameters.ShowAccepted && parameters.ShowRefused)
            {
                requests = _requestRepository.GetAll(r => r.AcceptanceDate != null || r.RefusalDate != null);
            }
            else
            {
                requests = _requestRepository.GetAll(r => r.AcceptanceDate == null && r.RefusalDate == null);
            }
            //switch (parameters.RequestStatus)
            //{
            //    case Enums.RequestStatus.InProgress:
            //        requests = _requestRepository.GetAll(r => r.AcceptanceDate == null && r.RefusalDate == null);
            //        break;
            //    case Enums.RequestStatus.Accepted:
            //        requests = _requestRepository.GetAll(r => r.AcceptanceDate != null);
            //        break;
            //    case Enums.RequestStatus.Refused:
            //        requests = _requestRepository.GetAll(r => r.RefusalDate != null);
            //        break;
            //}
            int totalPageCount = (int)Math.Ceiling((decimal)requests.Count() / parameters.Limit);
            var requestsSort = requests.AsQueryable();
            requestsSort = Sort(requestsSort, parameters);
            requests = requestsSort.ToList();
            requests = requests.Skip(parameters.Limit * (parameters.PageNumber - 1)).Take(parameters.Limit).ToList();
            var requestsDto = Mapper.Map<List<RequestDto>>(requests);
            //NIE WIEM CZY POTRZEBNE TO USTAWIANIE NULLI
            //if (parameters.ShowAccepted && !parameters.ShowRefused)
            //{
            //    requestsDto.ForEach(r => r.RefusalDate = null);
            //}
            //else if (parameters.ShowRefused && !parameters.ShowAccepted)
            //{
            //    requestsDto.ForEach(r => r.AcceptanceDate = null);
            //}
            //else if (parameters.ShowAccepted && parameters.ShowRefused)
            //{
            //}
            //else
            //{
            //    requestsDto.ForEach(r =>
            //    {
            //        r.AcceptanceDate = null;
            //        r.RefusalDate = null;
            //    });
            //}

            //switch (parameters.RequestStatus)
            //{
            //    case Enums.RequestStatus.InProgress:
            //        requestsDto.ForEach(r =>
            //        {
            //            r.AcceptanceDate = null;
            //            r.RefusalDate = null;
            //        });
            //        break;
            //    case Enums.RequestStatus.Accepted:
            //        requestsDto.ForEach(r => r.RefusalDate = null);
            //        break;
            //    case Enums.RequestStatus.Refused:
            //        requestsDto.ForEach(r => r.AcceptanceDate = null);
            //        break;
            //}
            requestsDto.ForEach(r => r.PostTitle = _postRepository.Get(p => p.Id == r.Post.Id).Title);
            result.Object = new RequestsListDto()
            {
                Requests = requestsDto,
                CurrentPage = parameters.PageNumber,
                TotalPageCount = totalPageCount
            };

            return result;
        }

        public IQueryable<Request> Sort(IQueryable<Request> requests, RequestParameterBindingModel parameters)
        {
            var property = typeof(Request).GetProperty(parameters.Sort);

            if (property == null)
            {
                RequestParameterBindingModel defaultParameters = new RequestParameterBindingModel();
                property = typeof(Request).GetProperty(defaultParameters.Sort);
            }
            else
            {
                requests = parameters.Ascending ? requests.OrderBy(x => property.GetValue(x)) : requests.OrderByDescending(x => property.GetValue(x));
            }

            return requests;
        }
        public async Task<ResponseDto<BaseModelDto>> RefuseRequest(long requestId)
        {
            var result = new ResponseDto<BaseModelDto>();
            var request = _requestRepository.Get(r => r.Id == requestId);
            if (request == null)
            {
                result.Errors.Add(ErrorsKeys.request_Refuse, ErrorsValues.request_NotExist);
                return result;
            }

            if (request.RefusalDate != null || request.AcceptanceDate != null)
            {
                result.Errors.Add(ErrorsKeys.request_Refuse, ErrorsValues.request_AlreadySolved);
                return result;
            }
            switch (request.Action)
            {
                case Enums.Action.Add:
                    break;
                case Enums.Action.Edit:
                    var editedPost = _postRepository.Get(p => p.Id == request.EditedPost.Id);
                    var deletePost = await _postRepository.Remove(editedPost);
                    if (!deletePost)
                    {
                        result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_Accept);
                        return result;
                    }
                    break;
                case Enums.Action.Delete:
                    break;
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

        public async Task<ResponseDto<BaseModelDto>> AcceptRequest(long requestId)
        {
            var result = new ResponseDto<BaseModelDto>();
            var request = _requestRepository.Get(r => r.Id == requestId);
            if (request == null)
            {
                result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_NotExist);
                return result;
            }
            var post = _postRepository.Get(p => p.Id == request.Post.Id);
            if (post == null)
            {
                result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.post_NotFoundInDb);
                return result;
            }

            if (request.RefusalDate != null || request.AcceptanceDate != null)
            {
                result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_AlreadySolved);
                return result;
            }
            
            request.AcceptanceDate = DateTime.Now;
            var saveRequest = await _requestRepository.SaveAsync();
            switch (request.Action)
            {
                case Enums.Action.Add:
                    post.AddedAt = DateTime.Now;
                    break;
                case Enums.Action.Edit:
                    var editedPost = _postRepository.Get(p => p.Id == request.EditedPost.Id);
                    //post = Mapper.Map<Post,Post>(editedPost);
                    post.Author = editedPost.Author;
                    post.Category = editedPost.Category;
                    post.Content = editedPost.Content;
                    post.Title = editedPost.Title;
                    post.PhotoPath = editedPost.PhotoPath;
                    post.EditedAt = DateTime.Now;
                    var deletePost = await _postRepository.Remove(editedPost);
                    if (!deletePost)
                    {
                        result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_Accept);
                        return result;
                    }
                    break;
                case Enums.Action.Delete:
                    post.IsDeleted = true;
                    break;
            }
            var savePost = await _postRepository.SaveAsync();

            if (!saveRequest || !savePost)
            {
                result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_Accept);
                return result;
            }

            return result;
        }
    }
}

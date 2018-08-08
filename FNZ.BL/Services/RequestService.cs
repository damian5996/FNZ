using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Consts;
using FNZ.Data.Repository.Interfaces;
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

            var editedPost = _postRepository.Get(p => p.Id == request.EditedPost.Id); 
            request.AcceptanceDate = DateTime.Now;
            var saveRequest = await _requestRepository.SaveAsync();
            switch (request.Action)
            {
                case Enums.Action.Add:
                    post.AddedAt = DateTime.Now;
                    break;
                case Enums.Action.Edit:
                    //post = Mapper.Map<Post,Post>(editedPost);
                    post.Author = editedPost.Author;
                    post.Category = editedPost.Category;
                    post.Content = editedPost.Content;
                    post.Title = editedPost.Title;
                    post.PhotoPath = editedPost.PhotoPath;
                    post.EditedAt = DateTime.Now;
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

            var deletePost = await _postRepository.Remove(editedPost);
            if (!deletePost)
            {
                result.Errors.Add(ErrorsKeys.request_Accept, ErrorsValues.request_Accept);
                return result;
            }

            return result;
        }
    }
}

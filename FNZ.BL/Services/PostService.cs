using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Consts;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IRequestRepository _requestRepository;

        public PostService(IPostRepository postRepository, IRequestRepository requestRepository)
        {
            _postRepository = postRepository;
            _requestRepository = requestRepository;
        }

        public async Task<ResponseDto<BaseModelDto>> AddPost(AddPostBindingModel postToAdd /*int moderatorId*/)
        {
            var result = new ResponseDto<BaseModelDto>();
            var post = Mapper.Map<Post>(postToAdd);
            var insertPost = await _postRepository.InsertAsync(post);
            if (!insertPost)
            {
                result.Errors.Add(ErrorsKeys.post_Adding, ErrorsValues.post_AddingRequest);
                return result;
            }
            
            var request = new Request()
            {
                SentAt = DateTime.Now,
                Post = post,
                Action = Enums.Action.Add
            };
            var insertRequest = await _requestRepository.InsertAsync(request);
            if (!insertRequest)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_AddingRequest);
                return result;
            }
            
            return result;
        }

        public async Task<ResponseDto<BaseModelDto>> DeletePost(long postId)
        {
            var result = new ResponseDto<BaseModelDto>();

            var post = _postRepository.Get(p => p.Id == postId);
            if (post.AddedAt == null)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_NotAddedYet);
                return result;
            }

            var request = new Request()
            {
                SentAt = DateTime.Now,
                Post = post,
                Action = Enums.Action.Delete
            };
            var insertRequest = await _requestRepository.InsertAsync(request);
            if (!insertRequest)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_AddingRequest);
                return result;
            }

            return result;
        }
    }
}

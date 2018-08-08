using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
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
        
        public async Task<ResponseDto<BaseModelDto>> AddPost(PostBindingModel postToAdd /*int moderatorId*/)
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

        public async Task<ResponseDto<BaseModelDto>> EditPost(long postId, PostBindingModel editPostBindingModel)
        {
            var result = new ResponseDto<BaseModelDto>();
            
            Post newPost = Mapper.Map<Post>(editPostBindingModel);
            var post = _postRepository.Get(p => p.Id == postId);
            if (post == null)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_NotFound);
                return result;
            }
            if (post.IsDeleted)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_IsDeleted);
                return result;
            }
            var insertNewPost = await _postRepository.InsertAsync(newPost);
            
            if (!insertNewPost)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_EditingRequest);
                return result;
            }
            var request = new Request()
            {
                SentAt = DateTime.Now,
                Post = post,
                Action = Enums.Action.Edit,
                EditedPost = newPost
            };
            var insertRequest = await _requestRepository.InsertAsync(request);
            if (!insertRequest)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_EditingRequest);
                return result;
            }

            return result;
        }

        public async Task<ResponseDto<PostSearchDto>> GetAllPosts(PostSearchParameterBindingModel parameters)
        {
            var result = new ResponseDto<PostSearchDto>();

            var posts = await GetByParameters(parameters);

            result.Object = posts;
            return result;
        }

        public async Task<PostSearchDto> GetByParameters(PostSearchParameterBindingModel parameters)
        {
            IQueryable<Post> posts;
            if (parameters.Query != null)
            {
                posts = await GetPostsByParameters(parameters, true);
            }
            else
            {
                posts = await GetPostsByParameters(parameters, false);
            }
            int totalPageCount = (int)Math.Ceiling((decimal)posts.Count() / parameters.Limit);
            posts = Sort(posts, parameters);
            posts = posts.Skip(parameters.Limit * (parameters.PageNumber - 1)).Take(parameters.Limit);
            return new PostSearchDto()
            {
                Posts = posts.ToList(),
                CurrentPage = parameters.PageNumber,
                TotalPageCount = totalPageCount
            };
        }

        public IQueryable<Post> Sort(IQueryable<Post> posts, PostSearchParameterBindingModel parameters)
        {
            var property = typeof(Post).GetProperty(parameters.Sort);

            if (property == null)
            {
                PostSearchParameterBindingModel defaultParameters = new PostSearchParameterBindingModel();
                property = typeof(Post).GetProperty(defaultParameters.Sort);
            }
            else
            {
                posts = parameters.Ascending ? posts.OrderBy(x => property.GetValue(x)) : posts.OrderByDescending(x => property.GetValue(x));
            }

            return posts;
        }
        public async Task<IQueryable<Post>> GetPostsByParameters(PostSearchParameterBindingModel parameters, bool useFunction)
        {
            IQueryable<Post> posts = _postRepository.GetAll(p => p.AddedAt != null).AsQueryable();
            if (useFunction)
            {
                Expression<Func<Post, bool>> function = x =>
                    x.Title.Contains(parameters.Query) || x.Content.Contains(parameters.Query) ||
                    x.Author.Contains(parameters.Query);

                return posts.Where(function);
            }

            return posts;

        }
    }
}

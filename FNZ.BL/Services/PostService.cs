using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Data.Repository.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Consts;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;
using Microsoft.AspNetCore.Identity;

namespace FNZ.BL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IRequestRepository _requestRepository;
        private readonly UserManager<Moderator> _userManager;
        private readonly IAnimalRepository _animalRepository;
        private const string uploadMainFolder = "wwwroot\\postImages";

        public PostService(IPostRepository postRepository, IRequestRepository requestRepository, UserManager<Moderator> userManager, IAnimalRepository animalRepository)
        {
            _postRepository = postRepository;
            _requestRepository = requestRepository;
            _userManager = userManager;
            _animalRepository = animalRepository;
        }

        public ResponseDto<PostDto> GetPost(long postId)
        {
            var result = new ResponseDto<PostDto>();

            var post = _postRepository.Get(p => p.Id == postId && p.AddedAt != null);
            if (post == null)
            {
                result.Errors.Add(ErrorsKeys.post_GetById, ErrorsValues.post_NotFound);
            }
            var postDto = Mapper.Map<PostDto>(post);
            result.Object = postDto;

            return result;
        }
        
        public async Task<ResponseDto<BaseModelDto>> AddPost(PostBindingModel postToAdd, string moderatorId)
        {
            var result = new ResponseDto<BaseModelDto>();
            var post = Mapper.Map<Post>(postToAdd);
            
            var insertPost = await _postRepository.InsertAsync(post);
            if (!insertPost)
            {
                result.Errors.Add(ErrorsKeys.post_Adding, ErrorsValues.post_AddingRequest);
                return result;
            }
            var fileName = "";
            var uploadFolder = $"{uploadMainFolder}\\{post.Id}";
            if (postToAdd.File != null)
            {
                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                fileName = Guid.NewGuid() + Path.GetExtension(postToAdd.File.FileName);
                var path = Path.Combine(uploadFolder,
                    fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await postToAdd.File.CopyToAsync(stream);
                }
            }

            post.PhotoPath = fileName;
            if (postToAdd.Category == Enums.Category.CatsAdoption || postToAdd.Category == Enums.Category.DogsAdoption)
            {
                var animal = Mapper.Map<Animal>(postToAdd);
                animal.Type = postToAdd.Category == Enums.Category.CatsAdoption ? Enums.Type.Cat : Enums.Type.Dog;
                var insertAnimal = await _animalRepository.InsertAsync(animal);
                if (!insertAnimal)
                {
                    result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_AddingRequest);
                    return result;
                }

                post.Animal = animal;
            }
            var updatePost = await _postRepository.SaveAsync();
            if (!updatePost)
            {
                result.Errors.Add(ErrorsKeys.post_Adding, ErrorsValues.post_AddingRequest);
                return result;
            }

            var moderator = _userManager.FindByIdAsync(moderatorId);
            var request = new Request()
            {
                SentAt = DateTime.Now,
                Post = post,
                Action = Enums.Action.Add,
                Moderator = moderator.Result
            };
            var insertRequest = await _requestRepository.InsertAsync(request);
            if (!insertRequest)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_AddingRequest);
                return result;
            }
            
            return result;
        }

        public async Task<ResponseDto<BaseModelDto>> DeletePost(long postId, string moderatorId)
        {
            var result = new ResponseDto<BaseModelDto>();

            var post = _postRepository.Get(p => p.Id == postId);
            if (post.AddedAt == null)
            {
                result.Errors.Add(ErrorsKeys.post_CreatingRequest, ErrorsValues.post_NotAddedYet);
                return result;
            }
            var moderator = _userManager.FindByIdAsync(moderatorId);
            var request = new Request()
            {
                SentAt = DateTime.Now,
                Post = post,
                Action = Enums.Action.Delete,
                Moderator = moderator.Result
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
            var postsMapped = Mapper.Map<List<PostDto>>(posts.ToList());
            return new PostSearchDto()
            {
                PostsDto = postsMapped,
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
            IQueryable<Post> posts;
            if (parameters.Category == null)
            {
                posts = _postRepository.GetAll(p => p.AddedAt != null && p.IsDeleted == false).AsQueryable();
            }
            else
            {
                posts = _postRepository.GetAll(p => p.AddedAt != null && p.Category == parameters.Category && p.IsDeleted == false).AsQueryable();
            }
            var query = parameters.Query.ToLower();
            if (useFunction)
            {
                Expression<Func<Post, bool>> function = x =>
                    x.Title.ToLower().Contains(query) || x.Content.ToLower().Contains(query) ||
                    x.Author.ToLower().Contains(query);
                
                return posts.Where(function);
            }

            return posts;

        }
    }
}

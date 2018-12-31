using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.BindingModels;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResponseDto<BaseModelDto>> AddPost(PostBindingModel post, string moderatorId);
        Task<ResponseDto<BaseModelDto>> DeletePost(long postId);
        Task<ResponseDto<BaseModelDto>> EditPost(long postId, PostBindingModel editPostBindingModel);
        Task<ResponseDto<PostSearchDto>> GetAllPosts(PostSearchParameterBindingModel parameters);
        ResponseDto<PostDto> GetPost(long postId);
    }
}

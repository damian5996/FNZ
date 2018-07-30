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
        Task<ResponseDto<BaseModelDto>> AddPost(AddPostBindingModel post);
        Task<ResponseDto<BaseModelDto>> DeletePost(long postId);
    }
}

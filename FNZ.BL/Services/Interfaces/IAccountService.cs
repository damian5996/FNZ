using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FNZ.Share.BindingModels;
using FNZ.Share.ModelsDto;

namespace FNZ.BL.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDto<BaseModelDto>> Register(RegisterBindingModel registerBindingModel);
        Task<ResponseDto<LoginDto>> Login(LoginBindingModel loginBindingModel);
        Task<ResponseDto<BaseModelDto>> Logout();
        Task<ResponseDto<BaseModelDto>> EditProfile(string moderatorId, EditProfileBindingModel editProfileBindingModel);
        Task<ResponseDto<BaseModelDto>> ChangePassword(string moderatorId,
            ChangePasswordBindingModel changePasswordBindingModel);
        Task<ResponseDto<BaseModelDto>> AddModerator(string adminId, string emailHashed, string newModeratorEmail);
    }
}

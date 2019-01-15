using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FNZ.BL.Services.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Consts;
using FNZ.Share.Models;
using FNZ.Share.ModelsDto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FNZ.BL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Moderator> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<Moderator> userManager, IHttpContextAccessor httpContextAccessor, IEmailService emailService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
        }

        public async Task<ResponseDto<BaseModelDto>> Register(RegisterBindingModel registerBindingModel)
        {
            var result = new ResponseDto<BaseModelDto>();
            var moderator = await _userManager.FindByNameAsync(registerBindingModel.Username);
            if (moderator != null)
            {
                result.Errors.Add(ErrorsKeys.account_userAlreadyExists, ErrorsValues.account_userAlreadyExists);
            }

            if (moderator == null)
            {
                moderator = new Moderator()
                {
                    Id = registerBindingModel.Username,
                    Email = registerBindingModel.Email,
                    PasswordHash = registerBindingModel.Password,
                    FirstName = registerBindingModel.FirstName,
                    LastName = registerBindingModel.LastName,
                    IsDeleted = false,
                    IsAdmin = false,
                    UserName = registerBindingModel.Username
                };
                var createResult = await _userManager.CreateAsync(moderator, registerBindingModel.Password);
                if (createResult.Errors.Any())
                {
                    foreach (var error in createResult.Errors)
                    {
                        result.Errors.Add(error.Code, error.Description);
                    }
                    return result;
                }
            }

            return result;

        }
        public async Task<ResponseDto<LoginDto>> Login(LoginBindingModel loginBindingModel)
        {
            var result = new ResponseDto<LoginDto>();
            var moderator = await _userManager.FindByIdAsync(loginBindingModel.Username);
            if (moderator == null)
            {
                moderator = await _userManager.FindByEmailAsync(loginBindingModel.Username);
            }

            var correctPassword = await _userManager.CheckPasswordAsync(moderator, loginBindingModel.Password);
            if (moderator == null || !correctPassword)
            {
                result.Errors.Add(ErrorsKeys.account_Login, ErrorsValues.account_wrongCredentials);
                return result;
            }
            var identity = new ClaimsIdentity("Identity.Application");
            identity.AddClaim(new Claim(ClaimTypes.Name, moderator.Id));
            

            await _httpContextAccessor.HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });

            var loginDto = Mapper.Map<LoginDto>(moderator);
            result.Object = loginDto;
            
            return result;
        }

        [Authorize]
        public async Task<ResponseDto<BaseModelDto>> Logout()
        {
            var result = new ResponseDto<BaseModelDto>();
            await _httpContextAccessor.HttpContext.SignOutAsync("Identity.Application");
            return result;
        }

        public async Task<ResponseDto<BaseModelDto>> EditProfile(string moderatorId, EditProfileBindingModel editProfileBindingModel)
        {
            var result = new ResponseDto<BaseModelDto>();
            var moderator = await _userManager.FindByNameAsync(moderatorId);

            if (moderator == null)
            {
                result.Errors.Add(ErrorsValues.account_userAlreadyExists, "Nie znaleziona użytkownika w bazie danych.");
            }//move to consts

            moderator.FirstName = editProfileBindingModel.FirstName;
            moderator.LastName = editProfileBindingModel.LastName;

            var updateResult = await _userManager.UpdateAsync(moderator);

            if (updateResult.Errors.Any())
            {
                foreach (var error in updateResult.Errors)
                {
                    result.Errors.Add(error.Code, error.Description);
                }
                return result;
            }
            return result;
        }

        [Authorize]
        public async Task<ResponseDto<BaseModelDto>> ChangePassword(string moderatorId, ChangePasswordBindingModel changePasswordBindingModel)
        {
            var result = new ResponseDto<BaseModelDto>();
            var moderator = await _userManager.FindByIdAsync(moderatorId);
            
            var changePasswordResult = await _userManager.ChangePasswordAsync(moderator, changePasswordBindingModel.Password, changePasswordBindingModel.NewPassword);
            if (changePasswordResult.Errors.Any())
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    result.Errors.Add(error.Code, error.Description);
                }
                return result;
            }

            return result;
        }

        public async Task<ResponseDto<BaseModelDto>> AddModerator(string adminId, string emailHashed, string newModeratorEmail)
        {
            var result = new ResponseDto<BaseModelDto>();
            var moderator = await _userManager.FindByIdAsync(adminId);
            if (!moderator.IsAdmin)
            {
                result.Errors.Add(ErrorsValues.account_userIsNotAdmin, ErrorsKeys.account_userIsNotAdmin);
            }

            await _emailService.SendEmail(newModeratorEmail, "Fundacja Niechcianych Zwierząt - rejestracja",
                "Witaj! Poniżej znajduje się link przekierowujący na stronę Fundacji Niechcianych Zwierząt. Zostałeś wybrany/a moderatorem strony.\n Zarejestruj się i rzetelnie pełnij swoją funkcję.\n Powodzenia! :)\n http://localhost:8010/app/#!/register/" + emailHashed +"/" + newModeratorEmail);
            return result;
        }

        
    }
}

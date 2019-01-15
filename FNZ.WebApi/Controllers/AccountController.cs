using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using FNZ.Share.BindingModels;
using FNZ.Share.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace FNZ.WebApi.Controllers
{
    [Authorize]
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterBindingModel registerBindingModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.Register(registerBindingModel);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel loginBindingModel)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _accountService.Login(loginBindingModel);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _accountService.Logout();
            return Ok(result);
        }

        [HttpPost("EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] EditProfileBindingModel editProfileBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moderatorId = User.Identity.Name;
            var result = await _accountService.EditProfile(moderatorId, editProfileBindingModel);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordBindingModel changePasswordBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var moderatorId = User.Identity.Name;
            var result = await _accountService.ChangePassword(moderatorId, changePasswordBindingModel);
            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("AddModerator")]
        public async Task<IActionResult> AddModerator([FromForm] string newModeratorEmail, [FromForm] string emailHashed)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var adminId = User.Identity.Name;
            var result = await _accountService.AddModerator(adminId, emailHashed, newModeratorEmail);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FNZ.BL.Services.Interfaces;
using FNZ.Share.BindingModels;
using Microsoft.AspNetCore.Mvc;

namespace FNZ.WebApi.Controllers
{
    [Route("Posts")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddPost([FromBody] AddPostBindingModel postBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _postService.AddPost(postBindingModel);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

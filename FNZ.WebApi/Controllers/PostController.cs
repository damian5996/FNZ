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

        [HttpGet("{postId}")]
        public IActionResult GetPost(long postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _postService.GetPost(postId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> GetAllPosts(PostSearchParameterBindingModel parameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _postService.GetAllPosts(parameters);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddPost([FromBody] PostBindingModel postBindingModel)
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

        [HttpDelete("{postId}/Delete")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _postService.DeletePost(postId);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPatch("{postId}/Edit")]
        public async Task<IActionResult> EditPost(long postId, PostBindingModel editPostBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _postService.EditPost(postId, editPostBindingModel);

            if (result.ErrorOccurred)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}

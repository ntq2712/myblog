
using System.Security.Claims;
using blog.DTO.Tag;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/tag")]
    public class TagController(ITag _tag) : ControllerBase
    {

        [HttpGet("getAll")]
        [Authorize]
        public async Task<IActionResult> GetAllTags()
        {
            try
            {
                var tags = await _tag.GetAll();

                return Ok(new Response<List<Tag>>
                {
                    Data = tags
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTag dto)
        {
            try
            {
                var userId = User.FindFirstValue("Id");
                var guidId = new Guid(userId ?? "");
                var tag = await _tag.Crate(dto.Name, guidId);

                return Ok(new Response<Tag>
                {
                    Data = tag
                });

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
using blog.DTO.Post;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [Route("[controller]")]
    public class PostController(IPost iPost) : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<ResponseBase<Post>>> CreatePost([FromBody]CreatePost _post)
        {
            try
            {
                var userIdClaim = HttpContext.User.Claims.FirstOrDefault(e => e.Type == "Id")?.Value;

                if (Guid.TryParse(userIdClaim, out var userId))
                {
                    var post = await iPost.Create(_post, userId);

                    var repon = new ResponseBase<Post>
                    {
                        Data = post,
                        Success = true,
                        Status = 200,
                        Message = "Success",
                    };

                    return repon;
                }
                else
                {
                    

                    var reponEmpty = new ResponseBase<Post>
                    {
                        Data = null,
                        Success = false,
                        Status = 401,
                        Message = "Don't find current user !",
                    };

                    return reponEmpty;
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
    }
}
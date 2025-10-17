using blog.DTO.MyProfile;
using blog.Filter;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MyProfileController(IMyProfileSevice myProfile) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var data = await myProfile.GetProfile();

                var repon = new Response<MyProfile>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> CreateProfile([FromBody] CMyProfile profile)
        {
            try
            {
                // var currentUserId = User.FindFirst("Id")?.Value;

                // if (currentUserId == null)
                // {
                //     return Ok(new ResponseBase<string>
                //     {
                //         Data = null,
                //         Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                //         Status = 401,
                //         Success = false
                //     });
                // }

                // Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var data = await myProfile.CreateProfile(profile);

                var repon = new Response<MyProfile>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> UpdateProfile([FromBody] CMyProfile profile)
        {
            try
            {
                // var currentUserId = User.FindFirst("Id")?.Value;

                // if (currentUserId == null)
                // {
                //     return Ok(new ResponseBase<string>
                //     {
                //         Data = null,
                //         Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                //         Status = 401,
                //         Success = false
                //     });
                // }

                // Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var data = await myProfile.UpdateProfile(profile);

                var repon = new Response<MyProfile>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                // var currentUserId = User.FindFirst("Id")?.Value;

                // if (currentUserId == null)
                // {
                //     return Ok(new ResponseBase<string>
                //     {
                //         Data = null,
                //         Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                //         Status = 401,
                //         Success = false
                //     });
                // }

                // Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var data = await myProfile.DeleteAll();

                var repon = new Response<bool>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
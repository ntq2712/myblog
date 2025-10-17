using blog.DTO.MyProfile;
using blog.Filter;
using blog.Helper;
using blog.Model;
using blog.Repository;
using blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkExperienceController(IWorkExperienceRepository service) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        // [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> GetAll()
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

                var data = await service.GetAllMyWork();

                var repon = new Response<List<MyWorkExperience>>
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
        public async Task<IActionResult> Create([FromBody] CWorkExperience dto)
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

                var data = await service.AddAsync(dto);

                var repon = new Response<MyWorkExperience>
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
        public async Task<IActionResult> Update([FromBody] MyWorkExperience dto)
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

                // Guid currentUserIdGuid = new Guid(currentUs  erId ?? "");

                var data = await service.Update(dto);

                var repon = new Response<MyWorkExperience>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(
                    new Response<MyWorkExperience>
                    {
                        Data = null,
                        Message = ex.Message,
                        Success = true,
                    }
                );
            }
        }
    }
}
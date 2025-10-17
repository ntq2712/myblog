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
    public class GetInTouchController(IGetInTouchRepository service) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                // var currentUserId = User.FindFirst("Id")?.Value;


                // Console.WriteLine($" user.currentUserId: {currentUserId}");

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

                var data = await service.GetAllInTouches();

                var repon = new Response<List<GetInTouch>>
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
        public async Task<IActionResult> Create([FromBody] CGetInTouch dto)
        {
            try
            {
                var data = await service.GetInTouch(dto);

                var repon = new Response<GetInTouch>
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
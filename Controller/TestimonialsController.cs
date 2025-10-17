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
    public class TestimonialsController(ITestimonialRepository service) : ControllerBase
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

                var data = await service.GetTestimonials();

                var repon = new Response<List<TestimonialResponse>>
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
        public async Task<IActionResult> Create([FromBody] CTestimonial dto)
        {
            try
            {
                var data = await service.Create(dto);

                var repon = new Response<Testimonials>
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
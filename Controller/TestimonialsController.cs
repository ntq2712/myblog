using blog.DTO.MyProfile;
using blog.DTO.Visitor;
using blog.Filter;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestimonialsController(ITestimonialRepository service, IVisitorRepository visitorRepository) : ControllerBase
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

        [HttpGet]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> GetAllByFilter([FromQuery] int status)
        {
            try
            {

                var data = await service.GetTestimonialByFilter(status);

                var repon = new Response<List<TestimonialFilterResponse>>
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
        public async Task<IActionResult> UpdateTestimonialStatus([FromBody] UpdateStatus updateStatus)
        {
            try
            {

                var data = await service.UpdateStatusTestimonial(updateStatus.Id, updateStatus.Status);

                if (data == null)
                {
                    return Ok(new Response<List<Testimonials>>
                    {
                        Data = null,
                        Message = "Fail",
                        Success = false,
                        Status = 400
                    });
                }

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

        [HttpPost]
        [Route("[action]")]
        // [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> Create([FromBody] CTestimonial dto)
        {
            try
            {
                var _visitor = await visitorRepository.GetVisitorById(dto.UserId);
                if (_visitor == null)
                {
                    return Ok(new Response<Testimonials>
                    {
                        Data = null,
                        Message = "Visitor not exist",
                        Success = false,
                        Status = 400
                    });
                }

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
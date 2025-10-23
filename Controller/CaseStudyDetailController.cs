using blog.DTO.MyProfile;
using blog.Extenstion;
using blog.Filter;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseStudyDetailController(ICaseStudyDetailRepository service) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.GetAll();

                var repon = new Response<List<CaseStudyDetail>>
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
        public async Task<IActionResult> GetListByCase(Guid id)
        {
            try
            {
                var data = await service.GetAllByCase(id);

                var repon = new Response<List<CaseStudyDetail>>
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
        public async Task<IActionResult> CreateCaseStudyDetail([FromBody] CCaseStudyDetail dto)
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

                var data = await service.Create(dto);

                var repon = new Response<CaseStudyDetail>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (HttpStatusCodeException ex)
            {
                return Ok(new Response<string>
                {
                    Message = ex.Message,
                    Success = false,
                    Status = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<string>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                });
            }
        }

        [HttpPut]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> UpdateCaseStudyDetail([FromBody] CaseStudyDetail dto)
        {
            try
            {
                var currentUserId = User.FindFirst("Id")?.Value;

                if (currentUserId == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền truy cập tài nguyên này !",
                        Status = 401,
                        Success = false
                    });
                }

                Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var data = await service.Update(dto, currentUserIdGuid);

                var repon = new Response<CaseStudyDetail>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                };

                return Ok(repon);
            }
            catch (HttpStatusCodeException ex)
            {
                return Ok(new Response<string>
                {
                    Message = ex.Message,
                    Success = false,
                    Status = ex.StatusCode
                });
            }
            catch (Exception ex)
            {
                return Ok(new Response<string>
                {
                    Status = 500,
                    Message = ex.Message,
                    Success = false
                });
            }
        }
    }
}
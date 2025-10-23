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
    public class CaseStudyController(ICaseStudyService service) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await service.GetList();

                var repon = new Response<List<CaseTudyFullValue>>
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
        public async Task<IActionResult> GetCaseStudyDetails(Guid id)
        {
            try
            {
                var data = await service.GetCaseStudyDetails(id);

                var repon = new Response<CaseStudyDetailById>
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
        public async Task<IActionResult> CreateCaseStudy([FromBody] CCaseStudy dto)
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

                var data = await service.CreateCaseStudy(dto);

                var repon = new Response<CaseStudy>
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

        [HttpPost]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> UpdateCaseStudy([FromBody] CaseStudy dto)
        {
            try
            {
                var currentUserId = User.FindFirst("Id")?.Value;

                if (currentUserId == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền truy cap !",
                        Status = 401,
                        Success = false
                    });
                }

                Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var data = await service.UpdateCaseStudy(dto, currentUserIdGuid);

                var repon = new Response<CaseStudy>
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

        [HttpPost]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> HideCaseStudy([FromBody] Guid id)
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

                var data = await service.HideCaseStudy(id, currentUserIdGuid);

                var repon = new Response<bool>
                {
                    Data = data,
                    Message = "",
                    Success = data,
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

        [HttpDelete]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        public async Task<IActionResult> DeleteById(Guid id)
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

                var data = await service.DeleteById(id);

                var repon = new Response<bool>
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
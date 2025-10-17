using blog.DTO.Guest;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestController(IGuest guest) : ControllerBase
    {
        [HttpGet]
        [Route("get-list-guest")]
        [Authorize]
        public async Task<ActionResult> GetListGuest([FromQuery] int _pageSize = 20, int _pageIndex = 1, string? _searchText = null,  int? _status = null, int? _relationship = null)
        {
            var listGuest = await guest.GetListGuest(_pageSize, _pageIndex, _searchText, _status, _relationship);
            var count = await guest.CountGuest();

            var reponse = new Response<List<Guest>>
            {
                Data = listGuest,
                Status = 200,
                Message = "Thanh Cong",
                Success = true,
                PageIndex = _pageIndex,
                PageSize = _pageSize,
                SearchText = _searchText,
                TotalRow = count
            };

            return Ok(reponse);
        }

        [HttpGet]
        [Route("get-summary-guest")]
        [Authorize]
        public async Task<ActionResult> Summary()
        {
            var listGuest = await guest.Summary();

            var reponse = new ResponseBase<SummaryGuest>
            {
                Data = listGuest,
                Status = 200,
                Message = "Thanh Cong",
                Success = true,
            };

            return Ok(reponse);
        }

        [HttpPost]
        [Route("create-guest")]
        [Authorize]
        public async Task<ActionResult> CreateGuest(CGuestDto dto)
        {
            try
            {
                var currentUserId = User.FindFirst("Id")?.Value;

                if (currentUserId == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                        Status = 401,
                        Success = false
                    });
                }

                Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var newGuest = await guest.CreateGuest(dto, currentUserIdGuid);

                var reponse = new Response<Guest>
                {
                    Data = newGuest,
                    Status = 200,
                    Message = "Tao Thanh Cong",
                    Success = true,
                    PageIndex = 1,
                    PageSize = 1,
                    SearchText = null,
                    TotalRow = 1
                };

                return Ok(reponse);
            }
            catch
            {
                return Ok(new ResponseBase<string>
                {
                    Data = null,
                    Message = "Da co loi xay ra !",
                    Status = 500,
                    Success = false
                });
            }
        }

        [HttpPut]
        [Route("update-guest")]
        [Authorize]
        public async Task<ActionResult> UpdateGuest(Guest dto)
        {
            try
            {
                var currentUserId = User.FindFirst("Id")?.Value;

                if (currentUserId == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                        Status = 401,
                        Success = false
                    });
                }

                Guid currentUserIdGuid = new Guid(currentUserId ?? "");

                var newGuest = await guest.UpdateGuest(dto, currentUserIdGuid);

                if (newGuest == null)
                {
                    var reponseErr = new ResponseBase<Guest>
                    {
                        Data = null,
                        Status = 400,
                        Message = "Khong tim thay guest",
                        Success = false,
                    };

                    return Ok(reponseErr);
                }

                var reponse = new Response<Guest>
                {
                    Data = newGuest,
                    Status = 200,
                    Message = "Tao Thanh Cong",
                    Success = true,
                    PageIndex = 1,
                    PageSize = 1,
                    SearchText = null,
                    TotalRow = 1
                };

                return Ok(reponse);
            }
            catch
            {
                return Ok(new ResponseBase<string>
                {
                    Data = null,
                    Message = "Da co loi xay ra !",
                    Status = 500,
                    Success = false
                });
            }
        }

        [HttpDelete]
        [Route("delete-guest")]
        [Authorize]
        public async Task<ActionResult> DeleteGuest(Guid id)
        {
            try
            {
                var newGuest = await guest.DeleteGuest(id);

                var reponse = new Response<Guest>
                {
                    Data = newGuest,
                    Status = 200,
                    Message = "Xoa Thanh Cong",
                    Success = true,
                    PageIndex = 1,
                    PageSize = 1,
                    SearchText = null,
                    TotalRow = 1
                };

                return Ok(reponse);
            }
            catch
            {
                return Ok(new ResponseBase<string>
                {
                    Data = null,
                    Message = "Da co loi xay ra !",
                    Status = 500,
                    Success = false
                });
            }
        }
    }
}

// eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0cm9uZ3F1aTI3MTJAZ21haWwuY29tIiwianRpIjoiMzhlYjAyMTEtMDQ4NS00MTJjLThiNWYtZTg4NmFlMWZjYjhjIiwiSWQiOiIxNWZiNGY5Ni1iNGYwLTQwMDgtMzRiYS0wOGRlMDhiYTJjNmYiLCJleHAiOjE3NjA3ODkyOTQsImlzcyI6Im5ndXllbnRyb25ncXVpLmNvbSIsImF1ZCI6Im5ndXllbnRyb25ncXVpMjcxMi5jb20ifQ.crYsolHMNrWHwDVJT2PN6FYIxh220Kq68PcXEg6KfWE
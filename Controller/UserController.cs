using System.Security.Claims;
using blog.DTO.User;
using blog.Filter;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser _iUser, IRSA _rsaService) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        [AuthorizeRole(RoleType.ADMIN)]
        // [Authorize]
        public async Task<ActionResult<Response<List<UserDto>>>> GetAllUser([FromQuery] int _pageSize = 20, int _pageIndex = 1, string? _searchText = null)
        {
            try
            {
                var users = await _iUser.GetAll(_pageSize, _pageIndex, _searchText);

                var count = await _iUser.Count();

                var repon = new Response<List<UserDto>>
                {
                    Data = users,
                    PageSize = _pageSize,
                    PageIndex = _pageIndex,
                    TotalRow = count,
                    SearchText = _searchText,
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
        [Authorize]
        public async Task<ActionResult<ResponseBase<User>>> Register([FromBody] CreateUser _user)
        {
            try
            {
                var permission = User.FindFirstValue("Permission");

                if (permission == null || permission != "Register")
                {
                    return Ok(new Response<string>
                    {
                        Data = "",
                        Success = false,
                        Message = "Bạn không có quyền truy cập tài nguyên này.",
                        Status = 401
                    });
                }

                var repon = new ResponseBase<User>();

                var isEmailExist = await _iUser.isEmailExist(_user.Email);
                if (isEmailExist)
                {
                    repon.Message = "Email exist !";
                    repon.Status = 204;

                    return Ok(repon);
                }

                var user = await _iUser.Create(_user);

                repon.Success = true;
                repon.Status = 200;
                repon.Message = "Success";
                repon.Data = user;

                return Ok(repon);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<ActionResult<ResponseBase<bool>>> DeleteUser(Guid _id)
        {
            try
            {
                var isSuccess = await _iUser.Delete(_id);

                var repon = new ResponseBase<bool>
                {
                    Success = isSuccess,
                    Message = "Success",
                    Data = isSuccess
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
        public async Task<ActionResult<ResponseBase<User>>> UpdateProfile([FromBody] User _user)
        {
            try
            {
                var currentUserId = User.FindFirst("Id")?.Value;
                Guid currentUserIdGuid = new Guid(currentUserId ?? "");
                var isAdmin = await _iUser.GetUserById(currentUserIdGuid);

                if (currentUserIdGuid != _user.UserId || isAdmin?.Role != 0)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                        Status = 401,
                        Success = false
                    });
                }

                var user = await _iUser.Edit(_user);

                var repon = new ResponseBase<User>
                {
                    Success = true,
                    Message = "Success",
                    Data = user
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
        [Authorize]
        public async Task<ActionResult<ResponseBase<User>>> GetUserById([FromQuery] Guid _id)
        {
            try
            {
                var user = await _iUser.GetUserById(_id);

                var repon = new ResponseBase<User>
                {
                    Success = true,
                    Message = "Success",
                    Data = user
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
        [Authorize]
        public async Task<ActionResult<ResponseBase<User>>> GetUserByUserName([FromQuery] string userName)
        {
            try
            {
                var user = await _iUser.GetUserByUserName(userName);

                var repon = new ResponseBase<User>
                {
                    Success = true,
                    Message = "Success",
                    Data = user
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
        public async Task<ActionResult<ResponseBase<string>>> Login([FromBody] LoginDto user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.password) || string.IsNullOrWhiteSpace(user.userName))
                {
                    var repon = new ResponseBase<string>
                    {
                        Data = "",
                        Status = 204,
                        Message = "Login Fail",
                        Success = false
                    };
                    return repon;
                }

                if (!ApiKeyProvider.IsValidBase64(user.password))
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Mật khẩu không hợp lệ.",
                        Status = 400,
                        Success = false
                    });
                }

                var _user = await _iUser.GetUserByUserName(user.userName);

                if (_user == null)
                {
                    var repon = new ResponseBase<string>
                    {
                        Data = "",
                        Status = 404,
                        Message = "Người dùng không tồn tại.",
                        Success = false
                    };
                    return repon;
                }

                string decryptedPasswor = _rsaService.Decrypt(user.password);

                var token = await _iUser.Login(_user, decryptedPasswor);

                if (token == null)
                {

                    var repon = new ResponseBase<string>
                    {
                        Data = "",
                        Status = 204,
                        Message = "Sai mật khẩu.",
                        Success = false
                    };
                    return repon;
                }
                else
                {

                    var repon = new ResponseBase<string>
                    {
                        Data = token,
                        Status = 200,
                        Message = "Thành công.",
                        Success = true
                    };

                    return repon;
                }

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, string apiKey)
        {
            try
            {

                if (!ApiKeyProvider.IsValidBase64(apiKey))
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Bạn không có quyền.",
                        Status = 401,
                        Success = false
                    });
                }
                string decryptedKey = ApiKeyProvider.Decrypt(apiKey);
                string secretKey = ApiKeyProvider.GetSecretKey();

                if (decryptedKey != secretKey)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Bạn không có quyền.",
                        Status = 401,
                        Success = false
                    });
                }

                if (email == null)
                {
                    var res = new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Email không hợp lệ.",
                        Status = 400,
                        Success = false
                    };

                    return Ok(res);
                }

                string token = await _iUser.VerifyEmail(email);

                if (token == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Đã có lỗi xãy ra.",
                        Status = 400,
                        Success = false
                    });
                }

                return Ok(new ResponseBase<string>
                {
                    Data = "",
                    Message = "Vui lòng kiểm tra gmail của bạn.",
                    Status = 204,
                    Success = true
                });
            }
            catch (KeyNotFoundException ex)
            {
                return Ok(new ResponseBase<string>
                {
                    Data = "",
                    Message = ex.Message,
                    Status = 400,
                    Success = false
                });
            }

        }

        [HttpPut("Change-Password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword resquet)
        {
            try
            {
                var user = await _iUser.GetUserById(resquet.userId);

                if (user == null)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Người dùng không tồn tại",
                        Status = 204,
                        Success = false
                    });
                }

                var currentUserId = User.FindFirst("Id")?.Value;
                Guid currentUserIdGuid = new Guid(currentUserId ?? "");
                var isAdmin = await _iUser.GetUserById(currentUserIdGuid);

                if (currentUserIdGuid != resquet.userId || isAdmin?.Role != 0)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = null,
                        Message = "Bạn không có quyền đổi mật khẩu tài khoản này !",
                        Status = 401,
                        Success = false
                    });
                }

                Guid id = await _iUser.ChangePassword(user, resquet.password);

                return Ok(new ResponseBase<Guid>
                {
                    Data = id,
                    Message = "Cập nhật mật khẩu thành công thành công !",
                    Status = 200,
                    Success = false
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("Forgot-Password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email, string apiKey)
        {
            try
            {
                if (!ApiKeyProvider.IsValidBase64(apiKey))
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Bạn không có quyền truy cập tài nguyên này.",
                        Status = 401,
                        Success = false
                    });
                }
                string decryptedKey = ApiKeyProvider.Decrypt(apiKey);
                string secretKey = ApiKeyProvider.GetSecretKey();

                if (decryptedKey != secretKey)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Bạn không có quyền.",
                        Status = 401,
                        Success = false
                    });
                }

                bool isSuccess = await _iUser.ResetPassword(email);

                if (isSuccess)
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Vui lòng kiểm tra gmail.",
                        Status = 204,
                        Success = true
                    });
                }
                else
                {
                    return Ok(new ResponseBase<string>
                    {
                        Data = "",
                        Message = "Tài khoản chưa được đăng ký.",
                        Status = 400,
                        Success = false
                    });

                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("public-key")]
        public ActionResult<string> GetPublicKey()
        {

            return Ok(_rsaService.GetPublicKey());
        }
    }
}
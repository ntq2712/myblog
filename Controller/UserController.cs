using blog.DTO.User;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUser iUser) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<Response<List<User>>>> GetAllUser([FromQuery] int _pageSize = 20, int _pageIndex = 1, string? _searchText = null)
        {
            try
            {
                var users = await iUser.GetAll(_pageSize, _pageIndex, _searchText);

                var count = await iUser.Count();

                var repon = new Response<List<User>>
                {
                    Data = users,
                    PageSize = _pageSize,
                    PageIndex = _pageIndex,
                    TotalRow = count,
                    SearchText = _searchText,
                    Message = "Success",
                    Success = true
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
        public async Task<ActionResult<ResponseBase<User>>> CreateUser([FromBody] CreateUser _user)
        {
            try
            {
                var repon = new ResponseBase<User>();

                var isAccountExist = await iUser.isAccountExist(_user.UserName);
                if (isAccountExist)
                {
                    repon.Status = 204;
                    repon.Message = "Account exist !";

                    return Ok(repon);
                }

                var isEmailExist = await iUser.isEmailExist(_user.Email);
                if (isEmailExist)
                {
                    repon.Message = "Email exist !";
                    repon.Status = 204;

                    return Ok(repon);
                }

                var user = await iUser.Create(_user);

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
                var isSuccess = await iUser.Delete(_id);

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
        public async Task<ActionResult<ResponseBase<User>>> UpdateUser([FromBody] User _user)
        {
            try
            {
                var user = await iUser.Edit(_user);

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
        public async Task<ActionResult<ResponseBase<User>>> GetUserById([FromQuery] Guid _id)
        {
            try
            {
                var user = await iUser.GetUserById(_id);

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
        public async Task<ActionResult<ResponseBase<User>>> GetUserByUserName([FromQuery] string userName)
        {
            try
            {
                var user = await iUser.GetUserByUserName(userName);

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
        public async Task<ActionResult<ResponseBase<string>>> Login([FromBody] string _userName, string _password)
        {
            try
            {
                var token = await iUser.Login(_userName, _password);

                if (token == null)
                {

                    var repon = new ResponseBase<string>
                    {
                        Data = "",
                        Status = 204,
                        Message = "Login Fail",
                        Success = true
                    };
                    return repon;
                }
                else
                {

                    var repon = new ResponseBase<string>
                    {
                        Data = token,
                        Status = 200,
                        Message = "Success",
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
    }
}
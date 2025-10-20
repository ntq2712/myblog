
using blog.DTO.Visitor;
using blog.Helper;
using blog.Model;
using blog.Repository;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitorController(IVisitorRepository repository) : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetVisitors()
        {
            try
            {
                var data = await repository.GetVisitors();

                var repon = new Response<List<Visitor>>
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
        public async Task<IActionResult> RegiteVisitor([FromBody] CVisitor visitor)
        {
            try
            {

                if (await repository.GetVisitorByAccount(visitor.Account))
                {

                    return Ok(new Response<string>
                    {
                        Data = "Account Exist",
                        Message = "Account Exist",
                        Success = false,
                        Status = 400
                    });
                }

                var data = await repository.RegiteVisitor(visitor);

                var repon = new Response<Visitor>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                    Status = 200
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
        public async Task<IActionResult> LoginVisitor([FromBody] LoginVisitor visitor)
        {
            try
            {
                var data = await repository.LoginVisitor(visitor.Account, visitor.Password);

                if (data == null)
                {
                    return Ok(new Response<VisitorResponse>
                    {
                        Data = null,
                        Message = "Account or password incorrect.",
                        Success = false,
                        Status = 400
                    });
                }

                var repon = new Response<VisitorResponse>
                {
                    Data = data,
                    Message = "Success",
                    Success = true,
                    Status = 200
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
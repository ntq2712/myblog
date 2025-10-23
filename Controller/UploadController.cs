
using blog.DTO;
using blog.Helper;
using Microsoft.AspNetCore.Mvc;

namespace blog.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly string _uploadfileFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        private readonly IWebHostEnvironment environment;

        public UploadController(IWebHostEnvironment _environment)
        {
            if (!Directory.Exists(_uploadfileFolder))
            {
                Directory.CreateDirectory(_uploadfileFolder);
            }

            environment = _environment;

        }

        [HttpPost("UploadImage")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "Invali file." });
            }

            if (file.Length > (5 * 1024 * 1024))
            {
                return BadRequest(new { message = "Maximum size can be 5 MB." });
            }

            var allowedExtensions = new[] { ".png", ".jpg" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (Array.IndexOf(allowedExtensions, fileExtension) == -1)
            {
                return BadRequest(new { message = "Just accept image." });
            }

            try
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(_uploadfileFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                string fileUrl = "";

                if (environment.IsDevelopment())
                {
                    fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";
                }
                else
                {
                    fileUrl = $"{Request.Scheme}://quinguyen.click/uploads/{fileName}";
                }

                var response = new ResponseBase<string>
                {
                    Data = fileUrl,
                    Status = 200,
                    Message = "",
                    Success = true,
                };

                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPost("UploadWithName")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadWithName([FromForm] UploadFileSpecifyName request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest(new { message = "Invalid request.File." });

            if (request.File.Length > (5 * 1024 * 1024)) // 5 MB
                return BadRequest(new { message = "Maximum size is 5 MB." });

            var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
            var fileExtension = Path.GetExtension(request.File.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest(new { message = "Only .png or .jpg allowed." });

            try
            {
                // 🧹 Làm sạch tên request.File người dùng nhập
                string safeFileName = string.IsNullOrWhiteSpace(request.FileName)
                    ? Path.GetFileNameWithoutExtension(request.File.FileName)
                    : Path.GetFileNameWithoutExtension(request.FileName);

                // Loại bỏ ký tự đặc biệt để tránh path traversal
                safeFileName = string.Concat(safeFileName.Split(Path.GetInvalidFileNameChars()));

                // Đảm bảo không trùng request.File
                string finalFileName = $"{safeFileName}{fileExtension}";
                string filePath = Path.Combine(_uploadfileFolder, finalFileName);

                if (!Directory.Exists(_uploadfileFolder))
                    Directory.CreateDirectory(_uploadfileFolder);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }

                // Tạo URL trả về
                string fileUrl;
                if (environment.IsDevelopment())
                {
                    fileUrl = $"{Request.Scheme}://{Request.Host}/uploads/{finalFileName}";
                }
                else
                {
                    fileUrl = $"https://quinguyen.click/uploads/{finalFileName}";
                }

                var response = new ResponseBase<string>
                {
                    Data = fileUrl,
                    Status = 200,
                    Message = "Upload successful",
                    Success = true,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Server error: {ex.Message}" });
            }
        }


        [HttpDelete("DelateImage")]
        public IActionResult DeleteIamge(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return BadRequest(new { message = "Invali file name." });
            }

            string filePath = Path.Combine(_uploadfileFolder, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return BadRequest(new { message = "File not exists" });
            }

            try
            {
                System.IO.File.Delete(filePath);

                var response = new ResponseBase<string>
                {
                    Data = fileName,
                    Status = 200,
                    Message = "Delte file success",
                    Success = true,
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi server khi xóa file.", error = ex.Message });
            }

        }
    }
}

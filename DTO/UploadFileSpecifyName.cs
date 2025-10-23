using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace blog.DTO
{
    public class UploadFileSpecifyName
    {


        [FromForm(Name = "file")]
        public IFormFile File { get; set; }

        [FromForm(Name = "fileName")]
        public string? FileName { get; set; }


    }
}
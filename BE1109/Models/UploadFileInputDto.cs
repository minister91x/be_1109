using System.ComponentModel.DataAnnotations;

namespace BE1109.Models
{
    public class UploadFileInputDto
    {
        [Required]
        public IFormFile? File { get; set; }
    }

}

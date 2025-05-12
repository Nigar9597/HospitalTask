using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalTask.Models
{
    public class Doctor3:BaseModel
    {
        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImageUpload { get; set; }
        public string Name { get; set; }
        public int Department3Id { get; set; }
        public Department3? Department3 { get; set; }
    }
}

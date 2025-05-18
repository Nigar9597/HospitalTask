using System.ComponentModel.DataAnnotations;

namespace HospitalTask.ViewModels
{
    public class LoginUserVM
    {
        [Required]
        public string UsernameorEmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public bool IsPersistent { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos {
    public class UserForRegisterDto {
        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength (8, MinimumLength = 4, ErrorMessage = "Password must be b/w 4 to 8")]
        public string Password { get; set; }
    }
}
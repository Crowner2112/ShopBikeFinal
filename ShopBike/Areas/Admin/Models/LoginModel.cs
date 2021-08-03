using System.ComponentModel.DataAnnotations;

namespace ShopPhone.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Hãy nhập đúng email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string Password { get; set; }
    }
}
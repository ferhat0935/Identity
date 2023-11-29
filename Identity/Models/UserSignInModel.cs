using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserSignInModel
    {
        [Required(ErrorMessage ="Kullanıcı adı giriniz")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Şifre giriniz")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberMe { get; set; }
    }
}

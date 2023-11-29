using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adı giriniz")]
        public  string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Lütfen email adresini doğru giriniz")]
        [Required(ErrorMessage ="Email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Şifre giriniz")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Cinsiyeti giriniz")]
        public string Gender { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class UserAdminCreateModel
    {
        [Required(ErrorMessage ="Kullanıcı adı giriniz!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Email giriniz!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Cinsiyet giriniz!")]
        public string Gender { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class RoleCreateModel
    {
        [Required(ErrorMessage ="isim giriniz!")]
        public  string Name { get; set; }
    }
}

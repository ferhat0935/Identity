using Microsoft.AspNetCore.Identity;

namespace Identity.CustomDescriber
{
    public class CustomErrorDescriber:IdentityErrorDescriber
    {
        public override IdentityError PasswordTooShort(int length)
        {
            return new()
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az {length} karakter olmalıdır."
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new()
            {
                Code= "PasswordRequiresNonAlphanumeric",
                Description="Şifre en az bir özel karakter(~! vs.) içermelidir."
            };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new()
            {
                Code = "DuplicateUserName",
                Description = $"{userName} Kullanıcı adı daha önceden kayıtlı"
            };
        }


        
    }
}

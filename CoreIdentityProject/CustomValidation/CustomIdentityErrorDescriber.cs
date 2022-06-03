using Microsoft.AspNetCore.Identity;

namespace CoreIdentityProject.CustomValidation
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError InvalidUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"Bu {userName} geçersizdir."
            };
        }
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError()
            {
                Code = "InvalidUserName",
                Description = $"Bu username({userName}) zaten kullanılmaktadır."
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"Bu {email} kullanılmaktadır."
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError()
            {
                Code = "DuplicateEmail",
                Description = $"Şifre en az {length} karakterli olmalıdır."
            };
        }
    }
}

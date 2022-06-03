using System.Collections.Generic;
using System.Threading.Tasks;
using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentityProject.CustomValidation
{
    public class CustomUserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            string[] digits = new string[] { "0", "1", "2", "3", "4", "5", "6", "4", "8", "9" };

            foreach (string item in digits)
            {
                if(user.UserName[0].ToString() == item)
                {
                    errors.Add(new IdentityError() { Code = "UserNameContainsFirstLetterDigit",Description="Kullanıcı adı sayı ile başlayamaz"});
                }
            }

            if (errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}

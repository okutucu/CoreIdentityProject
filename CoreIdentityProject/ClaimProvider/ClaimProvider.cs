using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentityProject.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {
        public UserManager<AppUser> _userManager { get; set; }
        public ClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

   

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if(principal != null && principal.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                AppUser user = await _userManager.FindByNameAsync(identity.Name);

                if(user != null)
                {
                    if(user.BirthDay!=null)
                    {
                        var toDay = DateTime.Today;
                        var age = toDay.Year - user.BirthDay?.Year;
                        

                        if(age>15)
                        {
                            Claim violanceClaim = new Claim("violance",true.ToString(), ClaimValueTypes.String, "Internal");

                            identity.AddClaim(violanceClaim);
                        }



                    }
                    if(user.City!= null)
                    {
                        if(!principal.HasClaim(c => c.Type == "city"))
                        {
                            Claim cityClaim = new Claim("city",user.City,ClaimValueTypes.String,"Internal");

                            identity.AddClaim(cityClaim);
                        }
                    }
                }

            }

            return principal;
            
        }
    }
}

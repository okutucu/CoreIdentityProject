using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreIdentityProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CoreIdentityProject.CustomTagHelpers
{
    [HtmlTargetElement("td",Attributes ="user-roles")]
    public class UserRolesName : TagHelper
    {
        public UserManager<AppUser> _userManager { get; set; }

        public UserRolesName(UserManager<AppUser> userManager)
        {
            _userManager= userManager;
        }

        [HtmlAttributeName("user-roles")]
        public string UserId { get; set; }


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);

            IList<string> roles =  await _userManager.GetRolesAsync(user);


            string html = string.Empty;

            roles.ToList().ForEach(x =>
            {
                html += $"<span class='badge rounded-pill bg-primary mb-1' style='display:block'> {x}  </span>";
            });

            output.Content.SetHtmlContent(html);
        }


    }
}

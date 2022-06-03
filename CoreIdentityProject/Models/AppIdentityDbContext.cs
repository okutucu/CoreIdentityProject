using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoreIdentityProject.Models
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options):base(options)
        {

        }

    }
}

using Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Context
{
    public class IdentityDbContext: IdentityDbContext<AppUser,AppRole,int> //user ve role tablolarının id lerini int yaptık
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext>options):base(options)
        {
            
        }
    }
}

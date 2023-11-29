using Microsoft.AspNetCore.Identity;
using System;

namespace Identity.Entities
{
    public class AppRole:IdentityRole<int>
    {
        public DateTime CreatedTime { get; set; }
    }
}

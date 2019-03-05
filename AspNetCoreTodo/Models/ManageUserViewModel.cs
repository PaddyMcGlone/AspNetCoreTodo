using System;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Models
{
    public class ManageUserViewModel
    {
        public IdentityUser[] Admins { get; set; }

        public IdentityUser[] Everyone { get; set; }
    }
}
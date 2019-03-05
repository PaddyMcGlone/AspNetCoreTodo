using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Controllers
{    
    [Authorize(Roles="Administrator")]
    public class ManageUsersController : Controller
    {
        #region Fields        

        private readonly UserManager<IdentityUser> _userManager;

        #endregion

        #region Constructor
        
        public ManageUsersController(UserManager<IdentityUser> UserManager)
        {
            _userManager = UserManager;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Administrators")).ToArray();
            var everyone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUserViewModel
            {
                Admins = admins,
                Everyone = everyone
            };

            return View(model);
        }
        
        #endregion

    }
}
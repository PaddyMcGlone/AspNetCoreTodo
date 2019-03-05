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

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        #region Fields
        private readonly ITodoItemService _toDoItemService;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructor
        public TodoController(ITodoItemService toDoItemService, UserManager<IdentityUser> userManager)
        {
            _toDoItemService = toDoItemService;   
            _userManager = userManager;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index()
        {            
            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var viewModel = new TodoViewModel
            {
                items = await _toDoItemService.GetIncompleteItemsAsync(currentUser)
            };            

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem item)
        {
            if(!ModelState.IsValid) return View("Index");

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var successful = await _toDoItemService.AddTodoItemsAsync(item, currentUser);

            if(!successful)
                return BadRequest("Could not return item");

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if(id == Guid.Empty) 
                return BadRequest("Could not update item");

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser == null) return Challenge();

            var successful = await _toDoItemService.MarkTodoItemDoneAsync(id, currentUser);

            return RedirectToAction("Index");
        }
        #endregion

    }
}
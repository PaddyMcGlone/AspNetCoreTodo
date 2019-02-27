using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreTodo.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        #region Fields
        ITodoItemService _toDoItemService;
        #endregion

        #region Constructor
        public TodoController(ITodoItemService toDoItemService)
        {
            _toDoItemService = toDoItemService;   
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index()
        {            
            var viewModel = new TodoViewModel
            {
                items = await _toDoItemService.GetIncompleteItemsAsync()
            };            

            return View(viewModel);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem item)
        {
            if(!ModelState.IsValid) return View("Index");

            var successful = await _toDoItemService.AddTodoItemsAsync(item);

            if(!successful)
                return BadRequest("Could not return item");

            return RedirectToAction("Index");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkDone(Guid id)
        {
            if(id == Guid.Empty) 
                return BadRequest("Could not update item");

            var successful = await _toDoItemService.MarkTodoItemDoneAsync(id);

            return RedirectToAction("Index");
        }
        #endregion

    }
}
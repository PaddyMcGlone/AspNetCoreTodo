using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;

namespace AspNetCoreTodo.Controllers
{
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

            var successful = await _toDoItemService.AddItemAsync(item);

            if(!successful)
                return BadRequest("Could not return item");

            return RedirectToAction("Index");
        }
        #endregion

    }
}
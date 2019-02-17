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
        public IActionResult Index()
        {
            var items  = _toDoItemService.GetIncompleteItemsAsync();
            
            return View();
        }
        #endregion

    }
}
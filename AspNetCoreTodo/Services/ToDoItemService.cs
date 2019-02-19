using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreTodo.Services
{
    public class ToDoItemService : ITodoItemService
    {

        #region Fields
        private readonly ApplicationDbContext _context;
        #endregion

        #region Constructor
        public ToDoItemService(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<TodoItem[]> GetIncompleteItemsAsync()
        {
            return await _context.ToDoItems
                            .Where(i => i.isDone == false)
                            .ToArrayAsync();
        }
        #endregion
    }
}
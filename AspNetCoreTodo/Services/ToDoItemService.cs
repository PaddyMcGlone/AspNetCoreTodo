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
            var items = await _context.ToDoItems
                            .Where(i => i.isDone == false)
                            .ToArrayAsync();

            return items;
        }

        public async Task<bool> AddTodoItemsAsync(TodoItem todoItem)
        {
            todoItem.id     = Guid.NewGuid();
            todoItem.isDone = false;            

            _context.ToDoItems.Add(todoItem);
            var success = await _context.SaveChangesAsync();
            return success == 1;
        }

        public async Task<bool> MarkTodoItemDoneAsync(Guid id)
        {
            if(id == Guid.Empty) return false;

            var item = await _context.ToDoItems
                                     .SingleOrDefaultAsync(i => i.id == id);
            
            item.isDone = true;
            var success = await _context.SaveChangesAsync();
            
            return success == 1;
        }

        #endregion
    }
}
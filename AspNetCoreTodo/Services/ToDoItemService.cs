using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;
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
        public async Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user)
        {
            var items = await _context.ToDoItems
                            .Where(i => i.isDone == false && i.UserId == user.Id)
                            .ToArrayAsync();

            return items;
        }

        public async Task<bool> AddTodoItemsAsync(TodoItem todoItem, IdentityUser user)
        {
            todoItem.id     = Guid.NewGuid();
            todoItem.isDone = false;
            todoItem.UserId = user.Id;            

            _context.ToDoItems.Add(todoItem);
            var success = await _context.SaveChangesAsync();
            return success == 1;
        }

        public async Task<bool> MarkTodoItemDoneAsync(Guid id, IdentityUser user)
        {
            if(id == Guid.Empty) return false;

            var item = await _context.ToDoItems                                     
                                     .SingleOrDefaultAsync(i => i.id == id && i.UserId == user.Id);
            
            if(item == null) return false;

            item.isDone = true;

            var success = await _context.SaveChangesAsync();            
            return success == 1;
        }

        #endregion
    }
}
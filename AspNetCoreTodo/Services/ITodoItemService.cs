using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(ApplicationUser user);

        Task <bool> AddTodoItemsAsync(TodoItem todoItem, ApplicationUser user);       

        Task <bool> MarkTodoItemDoneAsync(Guid id, ApplicationUser user); 
    }     
}
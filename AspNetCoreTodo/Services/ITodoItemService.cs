using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreTodo.Models;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreTodo.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemsAsync(IdentityUser user);

        Task <bool> AddTodoItemsAsync(TodoItem todoItem, IdentityUser user);       

        Task <bool> MarkTodoItemDoneAsync(Guid id, IdentityUser user); 
    }     
}
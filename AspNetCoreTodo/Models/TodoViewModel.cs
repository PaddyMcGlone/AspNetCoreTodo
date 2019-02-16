using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTodo.Models
{
    public class TodoViewModel
    {
        public TodoItem[] items { get; set; }
    }
}
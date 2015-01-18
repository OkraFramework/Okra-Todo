using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetTodoItemByIdAsync(string id);
        Task<IList<TodoItem>> GetTodoItemsAsync();
        Task<TodoItem> AddTodoItemAsync(TodoItem todoItem);
        Task RemoveTodoItemAsync(string itemId);
        Task UpdateTodoItemAsync(TodoItem todoItem);
    }
}

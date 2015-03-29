using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.DataModels
{
    public interface ITodoDataStore
    {
        IList<TodoItemDataModel> GetTodoItems();
        Task<TodoItemDataModel> GetTodoItemByIdAsync(string id);
        Task AddTodoItemAsync(string title);
        Task AddTodoItemAsync(string title, string[] notes);
        Task RemoveTodoItemAsync(TodoItemDataModel item);

        Task AddNoteAsync(TodoItemDataModel item, string note);
    }
}

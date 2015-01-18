using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    public interface ITodoRepository
    {
        TodoItem GetTodoItemById(string id);
        IList<TodoItem> GetTodoItems();
        TodoItem AddTodoItem(TodoItem todoItem);
        void RemoveTodoItem(TodoItem todoItem);
        void UpdateTodoItem(TodoItem todoItem);
    }
}

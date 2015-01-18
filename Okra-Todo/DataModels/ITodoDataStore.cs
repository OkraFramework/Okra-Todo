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
        void AddTodoItem(string title);
        void RemoveTodoItem(TodoItemDataModel item);
    }
}

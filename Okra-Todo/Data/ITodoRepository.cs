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
    }
}

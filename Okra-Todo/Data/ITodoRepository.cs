using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra_Todo.Data
{
    public interface ITodoRepository
    {
        IList<TodoItem> GetTodoItems();
    }
}

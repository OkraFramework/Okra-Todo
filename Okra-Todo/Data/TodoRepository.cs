using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra_Todo.Data
{
    [Export(typeof(ITodoRepository))]
    public class TodoRepository
    {
        public IList<TodoItem> GetTodoItems()
        {
            return new List<TodoItem>
                {
                    new TodoItem() { Title = "First item"},
                    new TodoItem() { Title = "Second item"},
                    new TodoItem() { Title = "Third item"}
                };
        }
    }
}

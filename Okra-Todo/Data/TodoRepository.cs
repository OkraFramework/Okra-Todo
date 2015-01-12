using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    [Export(typeof(ITodoRepository))]
    public class TodoRepository : ITodoRepository
    {
        public TodoItem GetTodoItemById(string id)
        {
            return GetTodoItems().Where(item => item.Id == id).FirstOrDefault();
        }

        public IList<TodoItem> GetTodoItems()
        {
            return new List<TodoItem>
                {
                    new TodoItem() { Id = "1", Title = "First item"},
                    new TodoItem() { Id = "2", Title = "Second item"},
                    new TodoItem() { Id = "3", Title = "Third item"}
                };
        }
    }
}

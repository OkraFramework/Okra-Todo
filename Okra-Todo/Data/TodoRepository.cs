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
        private IList<TodoItem> todoItems;

        public TodoRepository()
        {
            this.todoItems = new List<TodoItem>
                {
                    new TodoItem() { Id = "1", Title = "First item"},
                    new TodoItem() { Id = "2", Title = "Second item"},
                    new TodoItem() { Id = "3", Title = "Third item"}
                };
        }

        public TodoItem GetTodoItemById(string id)
        {
            return todoItems.Where(item => item.Id == id).FirstOrDefault();
        }

        public IList<TodoItem> GetTodoItems()
        {
            return todoItems;
        }

        public void AddTodoItem(TodoItem todoItem)
        {
            this.todoItems.Add(todoItem);
        }

        public void RemoveTodoItem(TodoItem todoItem)
        {
            this.todoItems.Remove(todoItem);
        }
    }
}

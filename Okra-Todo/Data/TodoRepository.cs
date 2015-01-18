using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    [Export(typeof(ITodoRepository))]
    [Shared]
    public class TodoRepository : ITodoRepository
    {
        private IList<TodoItem> todoItems;
        private int nextId = 4;

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
            return todoItems.FirstOrDefault(item => item.Id == id);
        }

        public IList<TodoItem> GetTodoItems()
        {
            return todoItems;
        }

        public TodoItem AddTodoItem(TodoItem todoItem)
        {
            todoItem.Id = (nextId++).ToString();
            this.todoItems.Add(todoItem);
            return todoItem;
        }

        public void RemoveTodoItem(TodoItem todoItem)
        {
            this.todoItems.Remove(todoItem);
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            TodoItem internalItem = todoItems.First(item => item.Id == todoItem.Id);

            internalItem.Title = todoItem.Title;
            internalItem.Completed = todoItem.Completed;
        }
    }
}

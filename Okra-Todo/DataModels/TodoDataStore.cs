using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.DataModels
{
    [Export(typeof(ITodoDataStore))]
    [Shared]
    public class TodoDataStore: ITodoDataStore
    {
        private ITodoRepository todoRepository;

        private IList<TodoItemDataModel> todoItems;

        [ImportingConstructor]
        public TodoDataStore(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public IList<TodoItemDataModel> GetTodoItems()
        {
            if (todoItems == null)
                InitializeTodoList();

            return todoItems;
        }

        public void AddTodoItem(string title)
        {
            TodoItem todoItem = todoRepository.AddTodoItem(new TodoItem { Title = title });
            this.todoItems.Add(new TodoItemDataModel(todoItem));
        }

        public void RemoveTodoItem(TodoItemDataModel item)
        {
            todoRepository.RemoveTodoItem(item.Id);
            this.todoItems.Remove(item);
        }

        private void InitializeTodoList()
        {
            IList<TodoItem> todoItems = todoRepository.GetTodoItems();

            this.todoItems = new ObservableCollection<TodoItemDataModel>();

            foreach (TodoItem todoItem in todoItems)
                this.todoItems.Add(new TodoItemDataModel(todoItem));
        }
    }
}

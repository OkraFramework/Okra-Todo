using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.DataModels
{
    [Export(typeof(ITodoDataStore))]
    [Shared]
    public class TodoDataStore : ITodoDataStore
    {
        private ITodoRepository todoRepository;

        private Task initializationTask;
        private IList<TodoItemDataModel> todoItems;

        [ImportingConstructor]
        public TodoDataStore(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }

        public IList<TodoItemDataModel> GetTodoItems()
        {
            InitializeTodoListAsync();

            return todoItems;
        }

        public async Task<TodoItemDataModel> GetTodoItemByIdAsync(string id)
        {
            await InitializeTodoListAsync();

            return todoItems.First(i => i.Id == id);
        }

        public async Task AddTodoItemAsync(string title)
        {
            TodoItem todoItem = await todoRepository.AddTodoItemAsync(new TodoItem { Title = title });

            TodoItemDataModel todoItemDataModel = new TodoItemDataModel(todoItem);
            todoItemDataModel.PropertyChanged += TodoItemDataModel_PropertyChanged;
            this.todoItems.Add(todoItemDataModel);
        }

        public async Task RemoveTodoItemAsync(TodoItemDataModel item)
        {
            await todoRepository.RemoveTodoItemAsync(item.Id);
            item.PropertyChanged -= TodoItemDataModel_PropertyChanged;
            this.todoItems.Remove(item);
        }

        private Task InitializeTodoListAsync()
        {
            if (initializationTask == null)
            {
                this.todoItems = new ObservableCollection<TodoItemDataModel>();
                initializationTask = InitializeTodoListAsync();
            }

            return initializationTask;
        }

        private async Task InitializeTodoListAsync()
        {
            IList<TodoItem> todoItems = await todoRepository.GetTodoItemsAsync();

            foreach (TodoItem todoItem in todoItems)
            {
                TodoItemDataModel todoItemDataModel = new TodoItemDataModel(todoItem);
                todoItemDataModel.PropertyChanged += TodoItemDataModel_PropertyChanged;
                this.todoItems.Add(todoItemDataModel);
            }
        }

        private async void TodoItemDataModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TodoItemDataModel todoItemDataModel = (TodoItemDataModel)sender;
            TodoItem todoItem = todoItemDataModel.ToTodoItem();
            await todoRepository.UpdateTodoItemAsync(todoItemDataModel.ToTodoItem());
        }
    }
}

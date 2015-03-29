using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

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

        public Task AddTodoItemAsync(string title)
        {
            return AddTodoItemAsync(title, new string[] { });
        }

        public async Task AddTodoItemAsync(string title, string[] notes)
        {
            TodoItem todoItem = await todoRepository.AddTodoItemAsync(new TodoItem { Title = title });

            foreach (string note in notes)
                await todoRepository.AddNoteAsync(todoItem.Id, note);

            TodoItemDataModel todoItemDataModel = new TodoItemDataModel(todoItem);
            todoItemDataModel.PropertyChanged += TodoItemDataModel_PropertyChanged;

            // NB: Since this method may be called from an external "Share" operation, this is a bit
            //     of a hack to transfer control to the main application thread to update the UI

            await System.Threading.Tasks.Task.Run(async () =>
                {
                    await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        this.todoItems.Add(todoItemDataModel);
                    });
                });
        }

        public async Task RemoveTodoItemAsync(TodoItemDataModel item)
        {
            await todoRepository.RemoveTodoItemAsync(item.Id);
            item.PropertyChanged -= TodoItemDataModel_PropertyChanged;
            this.todoItems.Remove(item);
        }

        public async Task AddNoteAsync(TodoItemDataModel item, string note)
        {
            await todoRepository.AddNoteAsync(item.Id, note);
            item.Notes.Add(note);
        }

        private Task InitializeTodoListAsync()
        {
            if (initializationTask == null)
            {
                this.todoItems = new ObservableCollection<TodoItemDataModel>();
                initializationTask = LoadTodoListAsync();
            }

            return initializationTask;
        }

        private async Task LoadTodoListAsync()
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

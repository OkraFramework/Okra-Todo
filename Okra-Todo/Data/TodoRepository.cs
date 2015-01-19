using Okra.Services;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Okra.TodoSample.Data
{
    [Export(typeof(ITodoRepository))]
    [Shared]
    public class TodoRepository : ITodoRepository
    {
        private IStorageManager storageManager;

        private const string STORAGE_FILENAME = "TodoItems.xml";

        private List<TodoItem> todoItems;
        private int nextId;

        [ImportingConstructor]
        public TodoRepository(IStorageManager storageManager)
        {
            this.storageManager = storageManager;
        }

        public async Task<TodoItem> GetTodoItemByIdAsync(string id)
        {
            if (todoItems == null)
                await LoadTodoItemsAsync();

            return todoItems.FirstOrDefault(item => item.Id == id);
        }

        public async Task<IList<TodoItem>> GetTodoItemsAsync()
        {
            if (todoItems == null)
                await LoadTodoItemsAsync();

            return todoItems;
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem todoItem)
        {
            todoItem.Id = (nextId++).ToString();
            this.todoItems.Add(todoItem);
            
            await SaveTodoItemsAsync();

            return todoItem;
        }

        public async Task RemoveTodoItemAsync(string itemId)
        {
            TodoItem todoItem = todoItems.First(i => i.Id == itemId);
            this.todoItems.Remove(todoItem);

            await SaveTodoItemsAsync();
        }

        public async Task UpdateTodoItemAsync(TodoItem todoItem)
        {
            TodoItem internalItem = todoItems.First(item => item.Id == todoItem.Id);

            internalItem.Title = todoItem.Title;
            internalItem.Completed = todoItem.Completed;

            await SaveTodoItemsAsync();
        }

        public async Task AddNoteAsync(string itemId, string note)
        {
            TodoItem internalItem = todoItems.First(item => item.Id == itemId);

            internalItem.Notes.Add(note);

            await SaveTodoItemsAsync();
        }

        private async Task LoadTodoItemsAsync()
        {
            StorageFolder folder = ApplicationData.Current.RoamingFolder;
            StorageFile file = await folder.TryGetItemAsync(STORAGE_FILENAME) as StorageFile;

            if (file != null)
            {
                todoItems = await storageManager.RetrieveAsync<List<TodoItem>>(file);
            }
            else
            {
                this.todoItems = new List<TodoItem>
                {
                    new TodoItem() { Id = "1", Title = "Install Okra Todo", Completed = true},
                    new TodoItem() { Id = "2", Title = "Add new todo items"},
                    new TodoItem() { Id = "3", Title = "Inspect source code"}
                };
            }

            nextId = int.Parse(this.todoItems.Last().Id) + 1;
        }

        private async Task SaveTodoItemsAsync()
        {
            await storageManager.StoreAsync<List<TodoItem>>(ApplicationData.Current.RoamingFolder, STORAGE_FILENAME, todoItems);
        }
    }
}

using Okra.TodoSample.Common;
using Okra.Navigation;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Okra.TodoSample.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Okra.Core;
using Okra.TodoSample.DataModels;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Okra.TodoSample.Pages.Main
{
    /// <summary>
    /// A basic view model that provides characteristics common to most applications.
    /// </summary>
    [ViewModelExport(SpecialPageNames.Home)]
    public class MainViewModel : ViewModelBase
    {
        private ITodoDataStore todoDataStore;

        private DelegateCommand addNewItemCommand;

        private string addNewItemText;
        private IList<TodoItemDataModel> todoItems = new ObservableCollection<TodoItemDataModel>();

        [ImportingConstructor]
        public MainViewModel(INavigationContext navigationContext, ITodoDataStore todoRepository)
            : base(navigationContext)
        {
            this.todoDataStore = todoRepository;

            this.addNewItemCommand = new DelegateCommand(AddNewItem, CanAddNewItem);
            this.RemoveCompletedItemsCommand = new DelegateCommand(RemoveCompletedItems);
            this.ViewItemDetailCommand = new DelegateCommand<TodoItemDataModel>(ViewItemDetail);

            InitializeData();
        }

        protected MainViewModel()
        {
        }

        public ICommand AddNewItemCommand
        {
            get
            {
                return addNewItemCommand;
            }
        }

        public ICommand RemoveCompletedItemsCommand
        {
            get;
            private set;
        }

        public ICommand ViewItemDetailCommand
        {
            get;
            private set;
        }

        public string AddNewItemText
        {
            get
            {
                return addNewItemText;
            }
            set
            {
                SetProperty(ref addNewItemText, value);
                addNewItemCommand.NotifyCanExecuteChanged();
            }
        }

        public IList<TodoItemDataModel> TodoItems
        {
            get
            {
                return todoItems;
            }
        }

        public void AddNewItem()
        {
            todoDataStore.AddTodoItemAsync(this.AddNewItemText);
            this.AddNewItemText = null;
        }

        public bool CanAddNewItem()
        {
            return !string.IsNullOrEmpty(AddNewItemText);
        }

        public async void RemoveCompletedItems()
        {
            IList<TodoItemDataModel> itemsToRemove = TodoItems.Where(i => i.Completed).ToList();

            foreach (TodoItemDataModel item in itemsToRemove)
                await todoDataStore.RemoveTodoItemAsync(item);
        }

        public void ViewItemDetail(TodoItemDataModel todoItemDataModel)
        {
            NavigationManager.NavigateTo("ItemDetail", todoItemDataModel.Id);
        }

        private void InitializeData()
        {
            this.todoItems = todoDataStore.GetTodoItems();
        }
    }
}

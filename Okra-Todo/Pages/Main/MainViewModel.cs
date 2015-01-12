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
        private ITodoRepository todoRepository;

        private IList<TodoItemDataModel> todoItems = new ObservableCollection<TodoItemDataModel>();

        [ImportingConstructor]
        public MainViewModel(INavigationContext navigationContext, ITodoRepository todoRepository)
            : base(navigationContext)
        {
            this.todoRepository = todoRepository;

            this.AddItemCommand = new DelegateCommand(AddItem);

            InitializeData();
        }

        protected MainViewModel()
        {
        }

        public ICommand AddItemCommand
        {
            get;
            private set;
        }

        public IList<TodoItemDataModel> TodoItems
        {
            get
            {
                return todoItems;
            }
        }

        public void AddItem()
        {
        }

        private void InitializeData()
        {
            IList<TodoItem> items = todoRepository.GetTodoItems();

            foreach (TodoItem item in items)
                this.todoItems.Add(new TodoItemDataModel(item));
        }
    }
}

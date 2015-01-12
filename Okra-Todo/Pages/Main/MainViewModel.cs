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

        private IList<TodoItem> todoItems = new ObservableCollection<TodoItem>();

        [ImportingConstructor]
        public MainViewModel(INavigationContext navigationContext, ITodoRepository todoRepository)
            : base(navigationContext)
        {
            this.todoRepository = todoRepository;

            InitializeData();
        }

        protected MainViewModel()
        {
        }

        public IList<TodoItem> TodoItems
        {
            get
            {
                return todoItems;
            }
        }

        private void InitializeData()
        {
            IList<TodoItem> items = todoRepository.GetTodoItems();

            foreach (TodoItem item in items)
                this.todoItems.Add(item);
        }
    }
}

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
using Okra.TodoSample.DataModels;
using Okra.TodoSample.DataModels;
using System.Threading.Tasks;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Okra.TodoSample.Pages.ItemDetail
{
    /// <summary>
    /// A basic view model that provides characteristics common to most applications.
    /// </summary>
    [ViewModelExport("ItemDetail")]
    public class ItemDetailViewModel : ViewModelBase, IActivatable
    {
        private ITodoDataStore todoDataStore;

        private TodoItemDataModel todoItem;

        [ImportingConstructor]
        public ItemDetailViewModel(INavigationContext navigationContext, ITodoDataStore todoDataStore)
            : base(navigationContext)
        {
            this.todoDataStore = todoDataStore;
        }

        protected ItemDetailViewModel()
        {
        }

        public TodoItemDataModel TodoItem
        {
            get
            {
                return todoItem;
            }
            set
            {
                SetProperty(ref todoItem, value);
            }
        }

        public async void Activate(PageInfo pageInfo)
        {
            string todoItemId = pageInfo.GetArguments<string>();
            this.TodoItem = await todoDataStore.GetTodoItemByIdAsync(todoItemId);
        }

        public void SaveState(PageInfo pageInfo)
        {
        }
    }
}

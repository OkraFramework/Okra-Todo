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

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Okra.TodoSample.Pages.ItemDetail
{
    /// <summary>
    /// A basic view model that provides characteristics common to most applications.
    /// </summary>
    [ViewModelExport("ItemDetail")]
    public class ItemDetailViewModel : ViewModelBase
    {
        private TodoItemDataModel todoItem;

        [ImportingConstructor]
        public ItemDetailViewModel(INavigationContext navigationContext)
            : base(navigationContext)
        {
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
    }
}

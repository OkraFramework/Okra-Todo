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
using System.Threading.Tasks;
using Okra.Core;
using Okra.Sharing;
using System.Windows.Input;
using System.Text;
using Windows.ApplicationModel.DataTransfer;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Okra.TodoSample.Pages.ItemDetail
{
    /// <summary>
    /// A basic view model that provides characteristics common to most applications.
    /// </summary>
    [ViewModelExport("ItemDetail")]
    public class ItemDetailViewModel : ViewModelBase, IActivatable, IShareable
    {
        private ITodoDataStore todoDataStore;

        private DelegateCommand addNoteCommand;

        private string addNoteText;
        private TodoItemDataModel todoItem;

        [ImportingConstructor]
        public ItemDetailViewModel(INavigationContext navigationContext, ITodoDataStore todoDataStore)
            : base(navigationContext)
        {
            this.todoDataStore = todoDataStore;

            this.addNoteCommand = new DelegateCommand(AddNote, CanAddNote);
        }

        protected ItemDetailViewModel()
        {
        }

        public ICommand AddNoteCommand
        {
            get
            {
                return addNoteCommand;
            }
        }

        public string AddNoteText
        {
            get
            {
                return addNoteText;
            }
            set
            {
                SetProperty(ref addNoteText, value);
                addNoteCommand.NotifyCanExecuteChanged();
            }
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

        public void AddNote()
        {
            todoDataStore.AddNoteAsync(TodoItem, this.AddNoteText);
            this.AddNoteText = null;
        }

        public bool CanAddNote()
        {
            return !string.IsNullOrEmpty(AddNoteText);
        }

        public Task ShareRequested(IShareRequest shareRequest)
        {
            shareRequest.Data.Properties.Title = string.Format("Todo Item: {0}", TodoItem.Title);

            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.AppendFormat("<h1>{0}</h1>", TodoItem.Title);

            if (TodoItem.Notes.Count > 0)
            {
                htmlBuilder.Append("<h2>Notes</h2><ul>");

                foreach (string note in TodoItem.Notes)
                    htmlBuilder.AppendFormat("<li>{0}</li>", note);

                htmlBuilder.Append("</ul>");
            }

            shareRequest.Data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(htmlBuilder.ToString()));

            return Task.FromResult(false);
        }
    }
}

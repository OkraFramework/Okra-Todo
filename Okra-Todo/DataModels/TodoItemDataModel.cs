using Okra.Core;
using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.DataModels
{
    public class TodoItemDataModel : NotifyPropertyChangedBase
    {
        private string id;
        private string title;
        private bool completed;
        private IList<string> notes;

        public TodoItemDataModel()
        {
            this.notes = new ObservableCollection<string>();
        }

        public TodoItemDataModel(TodoItem todoItem)
        {
            this.id = todoItem.Id;
            this.title = todoItem.Title;
            this.completed = todoItem.Completed;
            this.notes = new ObservableCollection<string>(todoItem.Notes);
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
            }
        }

        public bool Completed
        {
            get
            {
                return completed;
            }
            set
            {
                SetProperty(ref completed, value);
            }
        }

        public IList<string> Notes
        {
            get
            {
                return notes;
            }
        }

        public TodoItem ToTodoItem()
        {
            return new TodoItem
                        {
                            Id = this.Id,
                            Title = this.Title,
                            Completed = this.Completed
                        };
        }
    }
}

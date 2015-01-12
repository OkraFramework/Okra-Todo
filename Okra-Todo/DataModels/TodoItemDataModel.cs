using Okra.Core;
using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
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

        public TodoItemDataModel()
        {
        }

        public TodoItemDataModel(TodoItem todoItem)
        {
            this.id = todoItem.Id;
            this.title = todoItem.Title;
            this.completed = todoItem.Completed;
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
    }
}

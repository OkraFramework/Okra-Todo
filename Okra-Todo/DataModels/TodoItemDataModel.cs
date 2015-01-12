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
        private string title;
        private bool completed;

        public TodoItemDataModel()
        {
        }

        public TodoItemDataModel(TodoItem todoItem)
        {
            this.Title = todoItem.Title;
            this.Completed = todoItem.Completed;
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

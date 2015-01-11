using Okra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    public class TodoItem : NotifyPropertyChangedBase
    {
        private string title;
        private bool completed;

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

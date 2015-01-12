using Okra.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Data
{
    public class TodoItem
    {
        public string Id
        {
            get;
            set;
        }

        public string Title
        {
            get;
            set;
        }

        public bool Completed
        {
            get;
            set;
        }
    }
}

using Okra.TodoSample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample
{
    public class MainDesignData : MainViewModel
    {
        public MainDesignData()
        {
            this.TodoItems.Add(new TodoItem { Title = "First Design-Time Item" });
            this.TodoItems.Add(new TodoItem { Title = "Second Design-Time Item", Completed = true });
            this.TodoItems.Add(new TodoItem { Title = "Third Design-Time Item" });
        }
    }
}

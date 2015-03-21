using Okra.TodoSample.Data;
using Okra.TodoSample.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Pages.Main
{
    public class MainDesignData : MainViewModel
    {
        public MainDesignData()
        {
            this.AppSettings = new AppSettings() { ActiveTaskColor = "Blue", CompletedTaskColor = "Red" };

            this.TodoItems.Add(new TodoItemDataModel { Title = "First Design-Time Item" });
            this.TodoItems.Add(new TodoItemDataModel { Title = "Second Design-Time Item", Completed = true });
            this.TodoItems.Add(new TodoItemDataModel { Title = "Third Design-Time Item" });
        }
    }
}

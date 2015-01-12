using Okra.TodoSample.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Pages.ItemDetail
{
    public class ItemDetailDesignData : ItemDetailViewModel
    {
        public ItemDetailDesignData()
        {
            this.TodoItem = new TodoItemDataModel { Title = "My Todo Item" };
        }
    }
}

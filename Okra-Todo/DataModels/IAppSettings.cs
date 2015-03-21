using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.DataModels
{
    public interface IAppSettings
    {
        string ActiveTaskColor { get; set; }
        string CompletedTaskColor { get; set; }
    }
}

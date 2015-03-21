using Okra.TodoSample.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Okra.TodoSample.Pages.Settings
{
    public class SettingsDesignData : SettingsViewModel
    {
        public SettingsDesignData()
        {
            this.AppSettings = new AppSettings();
        }
    }
}

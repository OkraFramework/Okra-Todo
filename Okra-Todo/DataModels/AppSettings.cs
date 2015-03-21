using Okra.Core;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Okra.TodoSample.DataModels
{
    [Export(typeof(IAppSettings))]
    [Shared]
    public class AppSettings : NotifyPropertyChangedBase, IAppSettings
    {
        private string activeTaskColor = "Black";
        private string completedTaskColor = "Red";

        public AppSettings()
        {
            ApplicationData.Current.DataChanged += ApplicationData_DataChanged;
            UpdateAppSettings();
        }

        public string ActiveTaskColor
        {
            get
            {
                return activeTaskColor;
            }
            set
            {
                SetProperty(ref activeTaskColor, value);
                StoreAppSettings();
            }
        }

        public string CompletedTaskColor
        {
            get
            {
                return completedTaskColor;
            }
            set
            {
                SetProperty(ref completedTaskColor, value);
                StoreAppSettings();
            }
        }

        private void ApplicationData_DataChanged(ApplicationData sender, object args)
        {
            // If roaming serttings are changed on another device, update on this device also

            UpdateAppSettings();
        }

        private void StoreAppSettings()
        {
            ApplicationDataCompositeValue taskColorSettings = new ApplicationDataCompositeValue();

            taskColorSettings["ActiveTaskColor"] = ActiveTaskColor;
            taskColorSettings["CompletedTaskColor"] = CompletedTaskColor;

            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            roamingSettings.Values["TaskColor"] = taskColorSettings;
        }

        private void UpdateAppSettings()
        {
            ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;
            ApplicationDataCompositeValue taskColorSettings = (ApplicationDataCompositeValue)roamingSettings.Values["TaskColor"];

            if (taskColorSettings != null)
            {
                ActiveTaskColor = (string)taskColorSettings["ActiveTaskColor"];
                CompletedTaskColor = (string)taskColorSettings["CompletedTaskColor"];
            }
        }
    }
}

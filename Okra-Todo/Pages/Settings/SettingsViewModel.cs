using Okra.Navigation;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Okra.TodoSample.Common;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Okra.TodoSample.DataModels;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace Okra.TodoSample.Pages.Settings
{
    /// <summary>
    /// A view model for displaying an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    [ViewModelExport("Settings")]
    public class SettingsViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public SettingsViewModel(INavigationContext navigationContext, IAppSettings appSettings)
            : base(navigationContext)
        {
            this.AppSettings = appSettings;
            this.AvailableColors = new string[] { "Black", "Blue", "Red", "Green" };
        }

        protected SettingsViewModel()
        {
        }

        public IList<string> AvailableColors
        {
            get;
            private set;
        }

        public IAppSettings AppSettings
        {
            get;
            protected set;
        }
    }
}

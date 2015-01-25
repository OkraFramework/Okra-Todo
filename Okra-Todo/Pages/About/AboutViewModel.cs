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
using Windows.ApplicationModel;

// The Settings Flyout item template is documented at http://go.microsoft.com/fwlink/?LinkId=273769

namespace Okra.TodoSample.Pages.About
{
    /// <summary>
    /// A view model for displaying an overview of a single group, including a preview of the items
    /// within the group.
    /// </summary>
    [ViewModelExport("AboutPage")]
    public class AboutViewModel : ViewModelBase
    {
        [ImportingConstructor]
        public AboutViewModel(INavigationContext navigationContext)
            : base(navigationContext)
        {
            PackageVersion version = Package.Current.Id.Version;
            this.ApplicationVersion = string.Format("{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
        }

        public string ApplicationVersion
        {
            get;
            private set;
        }
    }
}

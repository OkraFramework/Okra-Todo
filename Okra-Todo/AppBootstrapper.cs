using Okra;
using Okra.Navigation;
using Okra.Sharing;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.ApplicationSettings;

namespace Okra.TodoSample
{
    public class AppBootstrapper : OkraBootstrapper
    {
        [Import]
        public ISettingsPaneManager SettingsPaneManager { get; set; }

        [Import]
        public IShareSourceManager ShareSourceManager { get; set; }

        [Import]
        public IShareTargetManager ShareTargetManager { get; set; }

        /// <summary>
        /// Perform general initialization of application services.
        /// </summary>
        protected override void SetupServices()
        {
            // Configure the navigation manager to preseve application state in local storage
            NavigationManager.NavigationStorageType = NavigationStorageType.Local;

            // Register with Windows to display items in the settings pane
            SettingsPane.GetForCurrentView().CommandsRequested += SettingsPane_CommandsRequested;

            // Set the page name to act as a share target
            ShareTargetManager.ShareTargetPageName = "ShareTarget";
        }

        void SettingsPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            // The settings names should be localised so create a ResourceLoader to gather these from

            ResourceLoader resourceLoader = new ResourceLoader();

            // Add each settings item in the order you wish them to appear
            // NB: Since we want the SettingsPaneManager to navigate when the item is selected we use the GetNavigateToSettingsCommand helper

            args.Request.ApplicationCommands.Add(SettingsPaneManager.GetNavigateToSettingsCommand(resourceLoader.GetString("AboutCommandLabel"), "AboutPage"));
            args.Request.ApplicationCommands.Add(SettingsPaneManager.GetNavigateToSettingsCommand(resourceLoader.GetString("SettingsCommandLabel"), "Settings"));
        }
    }
}

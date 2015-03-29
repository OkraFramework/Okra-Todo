using Okra.TodoSample.Common;
using Okra.Core;
using Okra.Sharing;
using Okra.Navigation;
using System;
using System.Collections.Generic;
using System.Composition;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Okra.TodoSample.DataModels;

namespace Okra.TodoSample.Pages.ShareTarget
{
    /// <summary>
    /// This view model allows other applications to share content through this application.
    /// </summary>
    [ViewModelExport("ShareTarget")]
    public class ShareTargetViewModel : NotifyPropertyChangedBase, IShareTarget
    {
        private ITodoDataStore todoDataStore;

        private IShareOperation _shareOperation;
        private DelegateCommand _shareCommand;

        private string title;
        private string description;
        private ImageSource image;
        private bool sharing;
        private bool showImage;
        private string comment;
        private string placeholder;
        private bool supportsComment;

        [ImportingConstructor]
        public ShareTargetViewModel(ITodoDataStore todoDataStore)
        {
            this.todoDataStore = todoDataStore;

            this._shareCommand = new DelegateCommand(Share, CanShare);
        }

        public string Title
        {
            get
            {
                return title;
            }
            protected set
            {
                SetProperty(ref title, value);
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            protected set
            {
                SetProperty(ref description, value);
            }
        }

        public ImageSource Image
        {
            get
            {
                return image;
            }
            protected set
            {
                SetProperty(ref image, value);
            }
        }

        public bool Sharing
        {
            get
            {
                return sharing;
            }
            protected set
            {
                if (SetProperty(ref sharing, value))
                    _shareCommand.NotifyCanExecuteChanged();
            }
        }

        public bool ShowImage
        {
            get
            {
                return showImage;
            }
            protected set
            {
                SetProperty(ref showImage, value);
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                SetProperty(ref comment, value);
            }
        }

        public string Placeholder
        {
            get
            {
                return placeholder;
            }
            protected set
            {
                SetProperty(ref placeholder, value);
            }
        }

        public bool SupportsComment
        {
            get
            {
                return supportsComment;
            }
            protected set
            {
                SetProperty(ref supportsComment, value);
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return _shareCommand;
            }
        }

        /// <summary>
        /// Invoked when another application wants to share content through this application.
        /// </summary>
        /// <param name="pageInfo">Activation data used to coordinate the process with Windows.</param>
        public async void Activate(IShareOperation shareOperation)
        {
            this._shareOperation = shareOperation;

            // Communicate metadata about the shared content through the view model
            var shareProperties = this._shareOperation.Data.Properties;
            var thumbnailImage = new BitmapImage();
            this.Title = shareProperties.Title;
            this.Description = shareProperties.Description;
            this.Image = thumbnailImage;
            this.Sharing = false;
            this.ShowImage = false;
            this.Comment = String.Empty;
            this.Placeholder = "Add a note";
            this.SupportsComment = true;
        }

        /// <summary>
        /// Invoked when the user clicks the Share button.
        /// </summary>
        public async void Share()
        {
            this.Sharing = true;
            this._shareOperation.ReportStarted();

            string text = await _shareOperation.Data.GetTextAsync();
            string[] comments = string.IsNullOrEmpty(Comment) ? new string[] { } : new string[] { Comment };

            await todoDataStore.AddTodoItemAsync(text, comments);

            this._shareOperation.ReportCompleted();
        }

        public bool CanShare()
        {
            return !this.Sharing;
        }
    }
}

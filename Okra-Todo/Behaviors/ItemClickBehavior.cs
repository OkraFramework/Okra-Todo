using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Okra.TodoSample.Behaviors
{
    public class ItemClickBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(ItemClickBehavior), new PropertyMetadata(null));

        private DependencyObject associatedObject;   

        public DependencyObject AssociatedObject
        {
            get
            {
                return associatedObject;
            }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public void Attach(DependencyObject associatedObject)
        {
            if (associatedObject == this.associatedObject || DesignMode.DesignModeEnabled)
            {
                return;
            }

            if (this.associatedObject != null)
            {
                throw new InvalidOperationException("Cannot attach behavior multiple times.");
            }

            this.associatedObject = associatedObject;
            
            if (associatedObject is ListViewBase)
            {
                ((ListViewBase)associatedObject).ItemClick += AssociatedObject_ItemClick;
            }
        }

        public void Detach()
        {
            if (associatedObject is ListViewBase)
            {
                ((ListViewBase)associatedObject).ItemClick -= AssociatedObject_ItemClick;
            }

            this.associatedObject = null;
        }

        private void AssociatedObject_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (Command != null && Command.CanExecute(e.ClickedItem))
            {
                Command.Execute(e.ClickedItem);
            }
        }
    }
}

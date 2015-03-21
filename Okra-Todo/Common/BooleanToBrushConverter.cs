using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Okra.TodoSample.Common
{
    /// <summary>
    /// Value converter that translates true to <see cref="Visibility.Visible"/> and false to
    /// <see cref="Visibility.Collapsed"/>.
    /// </summary>
    public sealed class BooleanToBrushConverter : DependencyObject, IValueConverter
    {
        public static readonly DependencyProperty TrueValueProperty = DependencyProperty.Register("TrueValue", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(null));
        public static readonly DependencyProperty FalseValueProperty = DependencyProperty.Register("FalseValue", typeof(Brush), typeof(BooleanToBrushConverter), new PropertyMetadata(null));

        public Brush TrueValue
        {
            get { return (Brush)GetValue(TrueValueProperty); }
            set { SetValue(TrueValueProperty, value); }
        }

        public Brush FalseValue
        {
            get { return (Brush)GetValue(FalseValueProperty); }
            set { SetValue(FalseValueProperty, value); }
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value is bool && (bool)value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}

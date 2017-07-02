using System;
using System.Windows;
using MVVMBase.Factory.Catalog;

namespace MVVMBase.Components
{
    public class ViewModelLocator : DependencyObject
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached(
            "ViewModel", 
            typeof (Type), 
            typeof (ViewModelLocator),
            new PropertyMetadata(default(Type), OnViewModelChanged));
        
        public static void SetViewModel(DependencyObject element, Type value)
        {
            element.SetValue(ViewModelProperty, value);
        }

        public static Type GetViewModel(DependencyObject element)
        {
            return (Type) element.GetValue(ViewModelProperty);
        }

        private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == null) return;

            if (d == null) return;
            
            FrameworkElement control = d as FrameworkElement;
            if (control == null) return;

            CatalogEntry entry = ViewModelCatalog.Default.GetEntry(e.NewValue as Type);
            if (entry == null) return;

            control.DataContext = entry.ViewModelInstance;

            if (entry.View != null)
            {
                Frame frame = d as Frame;
                if (frame != null)
                    frame.Content = entry.View;
            }
        }
    }
}

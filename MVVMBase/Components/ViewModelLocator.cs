using System;
using System.Windows;
using System.Windows.Controls;
using MVVMBase.Binding;
using MVVMBase.Factory;

namespace MVVMBase.Components
{
    public class ViewModelLocator : BindableBase
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

            Control control = d as Control;
            if (control == null) return;

            ViewModel viewModel = ViewModelCatalog.Default.GetViewModel(e.NewValue as Type);
            if (viewModel == null) return;

            control.DataContext = viewModel;
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Navigation;
using MVVMBase.Binding;

namespace MVVMBase.Components
{
    public class ViewModelLocator : BindableBase
    {
        internal static ObservableCollection<ViewModelView> ViewModels { get; private set; } = new ObservableCollection<ViewModelView>();
    }

    internal class ViewModelView
    {
        public ViewModel ViewModel { get; private set; }
        public Type ViewType { get; private set; }

        public ViewModelView(ViewModel viewModel, Type viewType)
        {
            ViewModel = viewModel;
            ViewType = viewType;
        }
    }
}

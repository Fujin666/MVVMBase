using System.Collections.ObjectModel;
using MVVMBase.Binding;

namespace MVVMBase.Components
{
    public class ViewModelLocator : BindableBase
    {
        protected ViewModelLocator()
        {
            ViewModels = new ObservableCollection<ViewModel>();
        }

        internal static ObservableCollection<ViewModel> ViewModels { get; private set; }
    }
}

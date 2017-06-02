using System.ComponentModel;
using System.Windows;
using MVVMBase.Binding;

namespace MVVMBase.Components
{
    public class ViewModel : BindableBase
    {
        protected ViewModel()
        {
        }

        public bool IsDesignInstance
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new DependencyObject());
            }
        }
    }
}

using System.ComponentModel;
using System.Windows;
using MVVMBase.Binding;

namespace MVVMBase.Components
{
    public abstract class ViewModel : BindableBase
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

        public virtual void ViewModelCtor()
        {
            
        }

        public virtual void ViewModelDesignModeCtor()
        {
            
        }
    }
}

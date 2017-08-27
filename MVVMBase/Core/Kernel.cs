using System.Data;
using System.Linq;
using MVVMBase.Attributes;
using MVVMBase.Components;
using MVVMBase.Factory.Catalog;
using MVVMBase.Factory.Modules;
using MVVMBase.Helpers;

namespace MVVMBase.Core
{
    internal class Kernel : IKernel
    {
        private readonly BindingCatalog _bindingCatalog;
        private readonly ViewModelCatalog _viewModelCatalog;

        public Kernel()
        {
            _bindingCatalog = new BindingCatalog();
            _viewModelCatalog = new ViewModelCatalog();
        }

        public void Initialize()
        {
            LoadModules();
            CreateViewModelCatalog();
        }

        public void Bind<TInterface, TClass>(BindingType bindingType = BindingType.Default) where TInterface : class where TClass : class, TInterface
        {
            _bindingCatalog.CreateBinding<TInterface, TClass>(bindingType);
        }

        public T Get<T>() where T : class
        {
            return TypeResolver.GetInstanceOf<T>();
        }

        private void LoadModules()
        {
            foreach (IModule module in TypeResolver.GetInstancesOfBase<IModule>())
            {
                module.Load(this);
            }
        }

        private void CreateViewModelCatalog()
        {
            foreach (ViewModel viewmodel in TypeResolver.GetInstancesOfBase<ViewModel>())
            {
                View view = null;
                var attributes = viewmodel.GetType().GetCustomAttributes(typeof(ViewModelAttribute), false).Cast<ViewModelAttribute>().ToList();
                if (attributes.Count == 1)
                {
                    var attribute = attributes.FirstOrDefault();
                    if (attribute != null)
                    {
                        var viewInstance = TypeResolver.GetInstanceOf(attribute.ViewType);
                        if (viewInstance.GetType().BaseType == typeof(View))
                            view = (View)viewInstance;
                    }
                }

                _viewModelCatalog.AddViewModel(viewmodel, view);
            }
        }
    }
}

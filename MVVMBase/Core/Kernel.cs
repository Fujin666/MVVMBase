using System.Linq;
using MVVMBase.Attributes;
using MVVMBase.Components;
using MVVMBase.Factory.Catalog;
using MVVMBase.Factory.Modules;

namespace MVVMBase.Core
{
    internal class Kernel : IKernel
    {
        private ITypeResolver _typeResolver;

        public void Initialize(ITypeResolver typeResolver)
        {
            _typeResolver = typeResolver;
            LoadModules();
            CreateViewModelCatalog();
        }

        public void Bind<TInterface, TClass>() where TInterface : class where TClass : class
        {
            ViewModelCatalog.Default.CreateBinding<TInterface, TClass>();
        }

        private void LoadModules()
        {
            foreach (IModule module in _typeResolver.GetInstancesOfBase<IModule>())
            {
                module.Load(this);
            }
        }

        private void CreateViewModelCatalog()
        {
            foreach (ViewModel viewmodel in _typeResolver.GetInstancesOfBase<ViewModel>())
            {
                View view = null;
                var attributes = viewmodel.GetType().GetCustomAttributes(typeof(ViewModelAttribute), false).Cast<ViewModelAttribute>().ToList();
                if (attributes.Count == 1)
                {
                    var attribute = attributes.FirstOrDefault();
                    if (attribute != null)
                    {
                        var viewInstance = _typeResolver.GetInstanceOf(attribute.ViewType);
                        if (viewInstance.GetType().BaseType == typeof(View))
                            view = (View)viewInstance;
                    }
                }

                ViewModelCatalog.Default.AddViewModel(viewmodel, view);
            }
        }
    }
}

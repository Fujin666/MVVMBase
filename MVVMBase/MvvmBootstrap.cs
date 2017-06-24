using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MVVMBase.Attributes;
using MVVMBase.Components;
using MVVMBase.Factory.Catalog;
using MVVMBase.Factory.Instances;

namespace MVVMBase
{
    public abstract class MvvmBootstrap
    {
        public Window MainWindow { get; private set; }

        public void Start()
        {
            CreateViewModelCatalog();

            MainWindow = (Window) CreateMainWindow();
            Application.Current.MainWindow = MainWindow;
            MainWindow.Show();
        }

        protected virtual DependencyObject CreateMainWindow()
        {
            return InstanceLocator.Default.GetInstance<Window>();
        }

        protected void CreateViewModelCatalog()
        {
            List<ViewModel> viewmodels = InstanceLocator.Default.GetInstancesOfBase<ViewModel>();
            foreach (ViewModel viewmodel in viewmodels)
            {
                View view = null;
                var attributes = viewmodel.GetType().GetCustomAttributes(typeof (ViewModelAttribute), false).Cast<ViewModelAttribute>().ToList();
                if (attributes.Count == 1)
                {
                    var attribute = attributes.FirstOrDefault();
                    if (attribute != null)
                    {
                        var viewInstance = InstanceLocator.Default.GetInstance(attribute.ViewType);
                        if (viewInstance.GetType().BaseType == typeof(View))
                            view = (View)viewInstance;
                    }
                }
                
                ViewModelCatalog.Default.AddViewModel(viewmodel, view);
            }
        }
    }
}

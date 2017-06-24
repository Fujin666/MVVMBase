using System.Collections.Generic;
using System.Windows;
using MVVMBase.Components;
using MVVMBase.Factory;
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
                ViewModelCatalog.Default.AddViewModel(viewmodel);
            }
        }
    }
}

using System.Collections.Generic;
using System.Windows;
using MVVMBase.Components;
using MVVMBase.Factory;

namespace MVVMBase
{
    public abstract class MvvmBootstrap
    {
        private readonly ViewModelCatalog _viewModelCatalog = new ViewModelCatalog();

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
                
            }
        }
    }
}

using System.Windows;
using MVVMBase.Components;
using MVVMBase.Core;

namespace MVVMBase
{
    public abstract class MvvmBootstrap
    {
        public IKernel Kernel { get; private set; }

        public Window MainWindow { get; private set; }

        protected MvvmBootstrap()
        {
            Kernel = new Kernel();
        }
        
        public void Start()
        {
            Kernel.Initialize();

            MainWindow = (Shell) CreateMainWindow();
            Application.Current.MainWindow = MainWindow;
            MainWindow.Show();
        }

        protected virtual DependencyObject CreateMainWindow()
        {
            return Kernel.Get<Shell>();
        }
    }
}

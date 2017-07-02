using System.Windows;
using MVVMBase.Core;

namespace MVVMBase
{
    public abstract class MvvmBootstrap
    {
        public IKernel Kernel { get; private set; }

        private readonly ITypeResolver _typeResolver;

        public Window MainWindow { get; private set; }

        protected MvvmBootstrap() : this(new TypeResolver())
        {
        }

        protected MvvmBootstrap(ITypeResolver typeResolver)
        {
            Kernel = new Kernel();
            _typeResolver = typeResolver;
        }
        
        public void Start()
        {
            Kernel.Initialize(_typeResolver);

            MainWindow = (Window) CreateMainWindow();
            Application.Current.MainWindow = MainWindow;
            MainWindow.Show();
        }

        protected virtual DependencyObject CreateMainWindow()
        {
            return _typeResolver.GetInstanceOf<Window>();
        }
    }
}

using System.Windows;

namespace MVVMBase.Core
{
    public interface IKernel
    {
        void Initialize();

        void Bind<TInterface, TClass>(BindingType bindingType = BindingType.Default)
            where TInterface : class
            where TClass : class, TInterface;

        T Get<T>()
            where T : class;
    }
}

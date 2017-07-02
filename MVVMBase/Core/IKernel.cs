namespace MVVMBase.Core
{
    public interface IKernel
    {
        void Initialize(ITypeResolver typeResolver);

        void Bind<TInterface, TClass>()
            where TInterface : class
            where TClass : class;
    }
}

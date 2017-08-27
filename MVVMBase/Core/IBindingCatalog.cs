using MVVMBase.Factory.Catalog;

namespace MVVMBase.Core
{
    internal interface IBindingCatalog
    {
        void CreateBinding<TInterface, TClass>(BindingType bindingType) 
            where TInterface : class
            where TClass : class, TInterface;
        BindingEntry<T> GetBindingEntry<T>();
    }
}

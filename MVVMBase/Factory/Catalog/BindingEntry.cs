using System;

namespace MVVMBase.Factory.Catalog
{
    internal class BindingEntry
    {
        public Type InterfaceType { get; private set; }
        public Type ClassType { get; private set; }

        public BindingEntry(Type interfaceType, Type classType)
        {
            InterfaceType = interfaceType;
            ClassType = classType;
        }
    }
}

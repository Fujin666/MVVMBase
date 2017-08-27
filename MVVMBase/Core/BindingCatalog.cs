using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using MVVMBase.Factory.Catalog;
using MVVMBase.Helpers;

namespace MVVMBase.Core
{
    internal class BindingCatalog : IBindingCatalog
    {
        private readonly ObservableCollection<BindingEntry> _bindingEntries;

        public BindingCatalog()
        {
            _bindingEntries = new ObservableCollection<BindingEntry>();
        }

        public void CreateBinding<TInterface, TClass>(BindingType bindingType) where TInterface : class where TClass : class, TInterface
        {
            BindingEntry bindingEntry = null;

            IEnumerable<object> constructorArguments = GetListOfArguments<TClass>();
            
            switch (bindingType)
            {
                case BindingType.Default:
                    bindingEntry = CreateDefaultBinding<TInterface, TClass>(constructorArguments);
                    break;
                case BindingType.Singleton:
                    bindingEntry = CreateSingletonBinding<TInterface, TClass>(constructorArguments);
                    break;
            }

            bindingEntry.IfNotNull(entry => _bindingEntries.Add(entry));
        }

        private IEnumerable<object> GetListOfArguments<TClass>() where TClass : class
        {
            List<object> argsList = new List<object>();

            IEnumerable<ParameterInfo> arguments = TypeResolver.GetConstructorArguments<TClass>().ToList();

            foreach (ParameterInfo parameterInfo in arguments)
            {
                BindingEntry bindingEntry = _bindingEntries.FirstOrDefault(x => x.TypeOfInterface == parameterInfo.ParameterType);
                if (bindingEntry != null)
                {
                    argsList.Add(bindingEntry.Class);
                }
            }

            return argsList;
        }

        public BindingEntry CreateDefaultBinding<TInterface, TClass>(IEnumerable<object> constructorArguments) where TInterface : class where TClass : class, TInterface
        {
            return new DefaultEntry<TInterface, TClass>(constructorArguments);
        }

        public BindingEntry CreateSingletonBinding<TInterface, TClass>(IEnumerable<object> constructorArguments) where TInterface : class where TClass : class, TInterface
        {
            return new SingletonEntry<TInterface, TClass>(constructorArguments);
        }

        public BindingEntry<TInterface> GetBindingEntry<TInterface>()
        {
            return _bindingEntries.FirstOrDefault(entry => entry.TypeOfInterface == typeof (TInterface)) as BindingEntry<TInterface>;
        }
    }
}

using System;
using System.Collections.ObjectModel;
using System.Reflection;
using MVVMBase.Components;

namespace MVVMBase
{
    public class Bootstrap
    {
        private static Bootstrap _default;
        private static volatile object _lock = new object();

        public static Bootstrap Default
        {
            get
            {
                if (_default == null)
                {
                    lock (_lock)
                    {
                        if (_default == null)
                        {
                            _default = new Bootstrap();
                        }
                    }
                }
                return _default;
            }
        }

        public void Initialize()
        {
            Initialize(Assembly.GetEntryAssembly());
        }

        public void Initialize(Assembly entryAssembly)
        {
            var types = entryAssembly.GetExportedTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == typeof (ViewModel))
                {
                    ViewModel newViewModel = (ViewModel)Activator.CreateInstance(type);
                    ViewModelLocator.ViewModels.Add(newViewModel);
                }
            }
        }
    }
}

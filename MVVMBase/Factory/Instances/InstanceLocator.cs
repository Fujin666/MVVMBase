using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace MVVMBase.Factory.Instances
{
    public class InstanceLocator
    {
        private static InstanceLocator _instanceLocator;
        private static readonly object Lock = new object();

        public static InstanceLocator Default
        {
            get
            {
                if (_instanceLocator == null)
                {
                    lock (Lock)
                    {
                        if (_instanceLocator == null)
                        {
                            _instanceLocator = new InstanceLocator();
                        }
                    }
                }
                return _instanceLocator;
            }
        }

        private readonly ObservableCollection<AssemblyEntry> _assemblies = new ObservableCollection<AssemblyEntry>();

        public InstanceLocator()
        {
            var entry = Assembly.GetEntryAssembly();

            _assemblies.Add(new AssemblyEntry(entry.GetName(), entry));

            AssemblyName[] assemblyNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    if (assembly != null)
                    {
                        _assemblies.Add(new AssemblyEntry(assemblyName, assembly));
                    }
                }
            }
        }

        private Assembly GetAssembly(AssemblyName assemblyName)
        {

            AssemblyEntry entry = _assemblies.FirstOrDefault(x => x.AssemblyName == assemblyName);
            if (entry == null)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    _assemblies.Add(new AssemblyEntry(assemblyName, assembly));
                    return GetAssembly(assemblyName);
                }

                return null;
            }
            else
            {
                return entry.Assembly;
            }
        }

        public T GetInstance<T>()
        {
            foreach (var assembly in _assemblies)
            {
                T instance = GetInstance<T>(assembly.Assembly);
                if (instance != null) return instance;
            }

            return default(T);
        }

        private T GetInstance<T>(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type == typeof(T))
                {
                    return (T)Activator.CreateInstance(type);
                }
            }
            return default(T);
        }

        public List<T> GetInstances<T>()
        {
            List<T> list = new List<T>();

            foreach (var assembly in _assemblies)
            {
                T instance = GetInstance<T>(assembly.Assembly);
                if (instance != null) list.Add(instance);
            }

            return list;
        }

        public List<T> GetInstancesOfBase<T>()
        {
            List<T> list = new List<T>();

            foreach (var assembly in _assemblies)
            {
                T instance = GetInstanceOfBase<T>(assembly.Assembly);
                if (instance != null) list.Add(instance);
            }

            return list;
        }

        private T GetInstanceOfBase<T>(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                if (type.BaseType == typeof(T))
                {
                    return (T)Activator.CreateInstance(type);
                }
            }
            return default(T);
        }
    }
}

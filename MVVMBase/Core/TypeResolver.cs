using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace MVVMBase.Core
{
    public class TypeResolver : ITypeResolver
    {
        private readonly ObservableCollection<AssemblyEntry> _assemblies;

        public TypeResolver()
        {
            _assemblies = new ObservableCollection<AssemblyEntry>();

            LoadReferences(Assembly.GetEntryAssembly());
        }

        private void LoadReferences(Assembly rootAssembly)
        {
            var name = rootAssembly.GetName().Name;
            if (_assemblies.FirstOrDefault(x => x.AssemblyName.Name == name) == null)
            {
                _assemblies.Add(new AssemblyEntry(rootAssembly.GetName(), rootAssembly));
            }
            else
            {
                return;
            }
            
            AssemblyName[] assemblyNames = rootAssembly.GetReferencedAssemblies();
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                {
                    Assembly assembly = Assembly.Load(assemblyName);
                    if (assembly != null)
                    {
                        LoadReferences(assembly);
                    }
                }
            }
        }
        
        public T GetInstanceOf<T>()
        {
            foreach (var assembly in _assemblies)
            {
                T instance = (T)GetInstanceOf(assembly.Assembly, typeof(T));
                if (instance != null) return instance;
            }

            return default(T);
        }

        public object GetInstanceOf(Type type)
        {
            foreach (var assembly in _assemblies)
            {
                object instance = GetInstanceOf(assembly.Assembly, type);
                if (instance != null) return instance;
            }

            return null;
        }

        public T GetInstanceOfBase<T>()
        {
            foreach (var assembly in _assemblies)
            {
                T instance = (T)GetInstanceOfBase(assembly.Assembly, typeof(T));
                if (instance != null) return instance;
            }

            return default(T);
        }

        public object GetInstanceOfBase(Type type)
        {
            foreach (var assembly in _assemblies)
            {
                object instance = GetInstanceOfBase(assembly.Assembly, type);
                if (instance != null) return instance;
            }

            return null;
        }

        public IEnumerable<T> GetInstancesOf<T>()
        {
            foreach (var assembly in _assemblies)
            {
                foreach (T instance in GetInstancesOf(assembly.Assembly, typeof(T)))
                {
                    yield return instance;
                }
            }
        }

        public IEnumerable<object> GetInstancesOf(Type type)
        {
            foreach (var assembly in _assemblies)
            {
                foreach (object instance in GetInstancesOf(assembly.Assembly, type))
                {
                    yield return instance;
                }
            }
        }

        public IEnumerable<T> GetInstancesOfBase<T>()
        {
            foreach (var assembly in _assemblies)
            {
                foreach (T instance in GetInstancesOfBase(assembly.Assembly, typeof(T)))
                {
                    yield return instance;
                }
            }
        }

        public IEnumerable<object> GetInstancesOfBase(Type type)
        {
            foreach (var assembly in _assemblies)
            {
                foreach (object instance in GetInstancesOfBase(assembly.Assembly, type))
                {
                    yield return instance;
                }
            }
        }

        private object GetInstanceOf(Assembly assembly, Type type)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typeInTypes in types)
            {
                if (typeInTypes == type)
                {
                    return Activator.CreateInstance(typeInTypes);
                }
            }
            return null;
        }

        private IEnumerable<object> GetInstancesOf(Assembly assembly, Type type)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typeInTypes in types)
            {
                if (typeInTypes == type)
                {
                    yield return Activator.CreateInstance(typeInTypes);
                }
            }
        }

        private object GetInstanceOfBase(Assembly assembly, Type type)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typeInTypes in types)
            {
                if (type.IsClass)
                {
                    if (typeInTypes.BaseType == type)
                    {
                        return Activator.CreateInstance(typeInTypes);
                    }
                }
                else if (type.IsInterface)
                {
                    foreach (Type @interface in typeInTypes.GetInterfaces())
                    {
                        if (@interface == type)
                        {
                            return Activator.CreateInstance(typeInTypes);
                        }
                    }
                }
                
            }
            return null;
        }

        private IEnumerable<object> GetInstancesOfBase(Assembly assembly, Type type)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typeInTypes in types)
            {
                if(type.IsClass)
                {
                    if (typeInTypes.BaseType == type)
                    {
                        yield return Activator.CreateInstance(typeInTypes);
                    }
                }
                else if (type.IsInterface)
                {
                    foreach (Type @interface in typeInTypes.GetInterfaces())
                    {
                        if (@interface == type)
                        {
                           yield return Activator.CreateInstance(typeInTypes);
                        }
                    }
                }
            }
        }
    }
}

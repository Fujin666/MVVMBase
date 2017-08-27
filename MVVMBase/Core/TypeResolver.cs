using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MVVMBase.Core
{
    internal class TypeResolver
    {
        private static IEnumerable<Assembly> GetAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }

        public static T GetInstanceOf<T>() where T : class
        {
            return GetInstanceOf<T>(new object[0]);
        }

        public static object GetInstanceOf(Type type)
        {
            return GetInstanceOf(type, new object[0]);
        }

        public static T GetInstanceOf<T>(object[] constructorArguments)
        {
            var type = typeof(T);
            var loadedAssembly = GetAssemblies().FirstOrDefault(x => x == type.Assembly);

            return (T)GetInstanceOf(loadedAssembly, typeof(T), constructorArguments);
        }

        public static object GetInstanceOf(Type type, params object[] constructorArguments)
        {
            var loadedAssembly = GetAssemblies().FirstOrDefault(x => x == type.Assembly);

            return GetInstanceOf(loadedAssembly, type, constructorArguments);
        }

        public static T GetInstanceOfBase<T>() where T : class
        {
            var type = typeof(T);
            var loadedAssembly = GetAssemblies().FirstOrDefault(x => x == type.Assembly);

            return (T)GetInstanceOfBase(loadedAssembly, typeof(T));
        }

        public static object GetInstanceOfBase(Type type)
        {
            var loadedAssembly = GetAssemblies().FirstOrDefault(x => x == type.Assembly);

            return GetInstanceOfBase(loadedAssembly, type);
        }

        public static IEnumerable<T> GetInstancesOf<T>() where T : class
        {
            foreach (var assembly in GetAssemblies())
            {
                foreach (T instance in GetInstancesOf(assembly, typeof(T)))
                {
                    yield return instance;
                }
            }
        }

        public static IEnumerable<object> GetInstancesOf(Type type)
        {
            foreach (var assembly in GetAssemblies())
            {
                foreach (object instance in GetInstancesOf(assembly, type))
                {
                    yield return instance;
                }
            }
        }

        public static IEnumerable<T> GetInstancesOfBase<T>() where T : class
        {
            foreach (var assembly in GetAssemblies())
            {
                foreach (T instance in GetInstancesOfBase(assembly, typeof(T)))
                {
                    yield return instance;
                }
            }
        }

        public static IEnumerable<object> GetInstancesOfBase(Type type)
        {
            foreach (var assembly in GetAssemblies())
            {
                foreach (object instance in GetInstancesOfBase(assembly, type))
                {
                    yield return instance;
                }
            }
        }

        private static object GetInstanceOf(Assembly assembly, Type type, object[] constructorArguments)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type typeInTypes in types)
            {
                if (typeInTypes == type)
                {
                    return Activator.CreateInstance(typeInTypes, constructorArguments);
                }
            }
            return null;
        }

        private static IEnumerable<object> GetInstancesOf(Assembly assembly, Type type)
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

        private static object GetInstanceOfBase(Assembly assembly, Type type)
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

        private static IEnumerable<object> GetInstancesOfBase(Assembly assembly, Type type)
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

        public static IEnumerable<ParameterInfo> GetConstructorArguments<TClass>() where TClass : class 
        {
            var ctors = typeof(TClass).GetConstructors();
            if (ctors.Length == 0)
            {
                return null;
            }

            return ctors.First().GetParameters();
        }
    }
}

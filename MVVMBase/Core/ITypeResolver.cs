using System;
using System.Collections.Generic;

namespace MVVMBase.Core
{
    public interface ITypeResolver
    {
        T GetInstanceOf<T>();
        object GetInstanceOf(Type type);
        T GetInstanceOfBase<T>();
        object GetInstanceOfBase(Type type);
        IEnumerable<T> GetInstancesOf<T>();
        IEnumerable<object> GetInstancesOf(Type type);
        IEnumerable<T> GetInstancesOfBase<T>();
        IEnumerable<object> GetInstancesOfBase(Type type);
    }
}

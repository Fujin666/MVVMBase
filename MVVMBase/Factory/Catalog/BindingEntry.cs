using System;
using System.Collections.Generic;
using System.Linq;
using MVVMBase.Core;

namespace MVVMBase.Factory.Catalog
{

    internal abstract class BindingEntry
    {
        public abstract Type TypeOfInterface { get; }
        public abstract Type TypeOfClass { get; }
        public abstract object Class { get; }
    }

    internal abstract class BindingEntry<TInterface> : BindingEntry
    {
        public override Type TypeOfInterface { get { return typeof (TInterface); } }
    }

    internal abstract class BindingEntry<TInterface, TClass> : BindingEntry<TInterface>
    {
        public override Type TypeOfClass { get { return typeof(TClass); } }
        public abstract IEnumerable<object> ConstructorArguments { get; }
        
    }

    internal class DefaultEntry<TInterface, TClass> : BindingEntry<TInterface, TClass> where TClass : class
    {
        private readonly IEnumerable<object> _constructorArguments;
        
        public DefaultEntry(IEnumerable<object> constructorArguments)
        {
            _constructorArguments = constructorArguments;
        }

        public override IEnumerable<object> ConstructorArguments { get {return _constructorArguments; } }
        public override object Class { get {return TypeResolver.GetInstanceOf<TClass>(_constructorArguments.ToArray()); } }
    }

    internal class SingletonEntry<TInterface, TClass> : BindingEntry<TInterface, TClass> where TClass : class
    {
        private readonly IEnumerable<object> _constructorArguments;
        private TClass _class;

        public SingletonEntry(IEnumerable<object> constructorArguments)
        {
            _constructorArguments = constructorArguments;
        }

        public override IEnumerable<object> ConstructorArguments { get { return _constructorArguments; } }
        public override object Class
        {
            get
            {
                if (_class == null) _class = TypeResolver.GetInstanceOf<TClass>(_constructorArguments.ToArray()); ;
                return _class;
            }
        }
    }
}

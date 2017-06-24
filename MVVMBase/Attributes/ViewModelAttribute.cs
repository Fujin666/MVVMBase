using System;

namespace MVVMBase.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ViewModelAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public ViewModelAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}

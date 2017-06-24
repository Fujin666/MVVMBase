using System;

namespace MVVMBase.Attributes
{
    public class ViewModelAttribute : Attribute
    {
        public Type ViewType { get; private set; }

        public ViewModelAttribute(Type viewType)
        {
            ViewType = viewType;
        }
    }
}

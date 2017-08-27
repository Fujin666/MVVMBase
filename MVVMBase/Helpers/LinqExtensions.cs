using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMBase.Helpers
{
    public static class LinqExtensions
    {
        public static T IfNotNull<T>(this T item, Action<T> action) 
        {
            if (item == null)
            {
                return default(T);
            }

            action(item);

            return item;
        }

        public static TR IfNotNull<T, TR>(this T item, Func<T, TR> function) 
        {
            if (item == null)
            {
                return default(TR);
            }
            
            return function(item);
        }
    }
}

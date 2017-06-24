using System.Reflection;

namespace MVVMBase.Factory.Instances
{
    internal class AssemblyEntry
    {
        public AssemblyEntry(AssemblyName assemblyName, Assembly assembly)
        {
            AssemblyName = assemblyName;
            Assembly = assembly;
        }

        public AssemblyName AssemblyName { get; private set; }
        public Assembly Assembly { get; private set; }
    }
}

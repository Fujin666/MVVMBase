using MVVMBase.Core;

namespace MVVMBase.Factory.Modules
{
    public interface IModule
    {
        void Load(IKernel kernel);
    }
}

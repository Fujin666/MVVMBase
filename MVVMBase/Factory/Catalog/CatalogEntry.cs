using System;
using MVVMBase.Components;

namespace MVVMBase.Factory.Catalog
{
    internal class CatalogEntry
    {
        public CatalogEntry(ViewModel viewModel)
        {
            EntryType = viewModel.GetType();
            ViewModelInstance = viewModel;
        }

        public Type EntryType { get; private set; }
        public ViewModel ViewModelInstance { get; private set; }
    }
}
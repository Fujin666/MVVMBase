using System;
using MVVMBase.Components;

namespace MVVMBase.Factory.Catalog
{
    internal class CatalogEntry
    {
        public CatalogEntry(ViewModel viewModel, View view)
        {
            EntryType = viewModel.GetType();
            ViewModelInstance = viewModel;
            View = view;
        }

        public Type EntryType { get; private set; }
        public ViewModel ViewModelInstance { get; private set; }
        public View View { get; set; }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using MVVMBase.Components;

namespace MVVMBase.Factory.Catalog
{
    internal class ViewModelCatalog
    {
        private static readonly ObservableCollection<CatalogEntry> _catalogEntries = new ObservableCollection<CatalogEntry>();
        
        public void AddViewModel(ViewModel viewmodel, View view)
        {
            _catalogEntries.Add(new CatalogEntry(viewmodel, view));
        }

        public static CatalogEntry GetEntry(Type type)
        {
            return _catalogEntries.FirstOrDefault(x => x.EntryType == type);
        }
    }   
}

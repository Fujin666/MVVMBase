using System;
using System.Collections.ObjectModel;
using System.Linq;
using MVVMBase.Components;

namespace MVVMBase.Factory.Catalog
{
    internal class ViewModelCatalog
    {
        private static ViewModelCatalog _viewModelCatalog;
        private static readonly object Lock = new object();

        public static ViewModelCatalog Default
        {
            get
            {
                if (_viewModelCatalog == null)
                {
                    lock (Lock)
                    {
                        if (_viewModelCatalog == null)
                        {
                            _viewModelCatalog = new ViewModelCatalog();
                        }
                    }
                }
                return _viewModelCatalog;
            }
        }

        private readonly ObservableCollection<CatalogEntry> _catalogEntries;

        public ViewModelCatalog()
        {
            _catalogEntries = new ObservableCollection<CatalogEntry>();
        }

        public void AddViewModel(ViewModel viewmodel, View view)
        {
            _catalogEntries.Add(new CatalogEntry(viewmodel, view));
        }

        public ViewModel GetViewModel(Type viewModelType)
        {
            CatalogEntry catalogEntry = _catalogEntries.FirstOrDefault(x => x.EntryType == viewModelType);
            if (catalogEntry != null)
            {
                return catalogEntry.ViewModelInstance;
            }

            return null;
        }

        public CatalogEntry GetEntry(Type type)
        {
            return _catalogEntries.FirstOrDefault(x => x.EntryType == type);
        }
    }

    
}

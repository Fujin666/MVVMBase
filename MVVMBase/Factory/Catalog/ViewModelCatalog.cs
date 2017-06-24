using System;
using System.Collections.ObjectModel;
using System.Linq;
using MVVMBase.Components;
using MVVMBase.Factory.Catalog;

namespace MVVMBase.Factory
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

        public void AddViewModel(ViewModel viewmodel)
        {
            _catalogEntries.Add(new CatalogEntry(viewmodel));
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
    }

    
}

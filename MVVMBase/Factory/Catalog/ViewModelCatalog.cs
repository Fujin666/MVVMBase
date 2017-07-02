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
        private readonly ObservableCollection<BindingEntry> _bindingEntries;

        public ViewModelCatalog()
        {
            _catalogEntries = new ObservableCollection<CatalogEntry>();
            _bindingEntries = new ObservableCollection<BindingEntry>();
        }

        public void AddViewModel(ViewModel viewmodel, View view)
        {
            _catalogEntries.Add(new CatalogEntry(viewmodel, view));
        }

        public CatalogEntry GetEntry(Type type)
        {
            return _catalogEntries.FirstOrDefault(x => x.EntryType == type);
        }

        public void CreateBinding<TInterface, TClass>() where TInterface : class where TClass : class
        {
            BindingEntry bindingEntry = new BindingEntry(typeof(TInterface), typeof(TClass));
            _bindingEntries.Add(bindingEntry);
        }
    }

    
}

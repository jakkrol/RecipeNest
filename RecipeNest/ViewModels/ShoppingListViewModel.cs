using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RecipeNest.ViewModels
{
    public class ShoppingListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ShoppingList> ShoppingLists;
        private ObservableCollection<Models.ShoppingList> filteredShoppingLists;
        public ObservableCollection<Models.ShoppingList> FilteredShoppingLists
        {
            get => filteredShoppingLists;
            set
            {
                if (filteredShoppingLists != value)
                {
                    filteredShoppingLists = value;
                    OnPropertyChanged(nameof(FilteredShoppingLists));
                }
            }
        }

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public ICommand PerformSearchCommand { get; }

        public ShoppingListViewModel()
        {
            ShoppingLists = Services.ShoppingListService.Instance.ShoppingLists;
            FilteredShoppingLists = new ObservableCollection<Models.ShoppingList>(ShoppingLists);
            ShoppingLists.CollectionChanged += ShoppingLists_CollectionChanged;
            PerformSearchCommand = new Command(PerformSearch);
        }
        private void ShoppingLists_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PerformSearch();
        }
        private void PerformSearch()
        {
            Debug.WriteLine($"Searching for: {SearchText}");
            if (SearchText == null || SearchText == "")
            {
                FilteredShoppingLists = ShoppingLists;
            }
            else
            {
                FilteredShoppingLists = new ObservableCollection<Models.ShoppingList>(ShoppingLists.Where(r => r.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

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

        private double progress;
        public double Progress
        {
            get => progress;
            set
            {
                if (progress != value)
                {
                    progress = value;
                    OnPropertyChanged(nameof(Progress));
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
        public ICommand DeleteListCommand { get; }

        public ShoppingListViewModel()
        {
            ShoppingLists = Services.ShoppingListService.Instance.ShoppingLists;
            FilteredShoppingLists = new ObservableCollection<Models.ShoppingList>(ShoppingLists);
            ShoppingLists.CollectionChanged += ShoppingLists_CollectionChanged;
            PerformSearchCommand = new Command(PerformSearch);
            DeleteListCommand = new Command(DeleteList);
        }
        private void ShoppingLists_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PerformSearch();
        }
        private async void DeleteList(object list)
        {
            if (list is Models.ShoppingList shoppingList)
            {
                ShoppingLists.Remove(shoppingList);
                await Services.ShoppingListService.Instance.DeleteList(shoppingList);
            }
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

        public void UpdateProgress()
        {
            Debug.WriteLine("UpdateProgress called.");

            foreach (var list in ShoppingLists)
            {
                if (list.Items != null)
                {
                    int total = list.Items.Count;
                    int checkedCount = list.Items.Count(i => i.IsChecked);
                    list.ListProgress = total == 0 ? 0 : (double)checkedCount / total;
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

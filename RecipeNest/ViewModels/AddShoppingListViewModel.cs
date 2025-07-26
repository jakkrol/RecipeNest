using Microsoft.Maui.Controls;
using RecipeNest.Models;
using RecipeNest.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Xml.Linq;

namespace RecipeNest.ViewModels
{
    [QueryProperty(nameof(ListId), "listId")]
    public class AddShoppingListViewModel : INotifyPropertyChanged
    {
        ShoppingList? shoppinglist = new ShoppingList();
        private int? listId;
        public string? ListId
        {
            get => listId.ToString();
            set
            {
                listId = Convert.ToInt32(value);
                LoadListDetails();
            }
        }

        private string _listName;
        public string ListName
        {
            get => _listName;
            set
            {
                if (_listName != value)
                {
                    _listName = value;
                    OnPropertyChanged(nameof(ListName));
                }
            }
        }

        private string _newItemName;
        public string NewItemName
        {
            get => _newItemName;
            set
            {
                if (_newItemName != value)
                {
                    _newItemName = value;
                    OnPropertyChanged(nameof(NewItemName));
                }
            }
        }

        public ObservableCollection<ShoppingItem> ShoppingItems { get; set; } = new();

        public ICommand AddItemCommand { get; }
        public ICommand DeleteItemCommand { get; }
        public ICommand SaveShoppingListCommand { get; }

        public AddShoppingListViewModel()
        {
            AddItemCommand = new Command(AddItem);
            DeleteItemCommand = new Command(DeleteItem);
            SaveShoppingListCommand = new Command(async () => await SaveShoppingList());
        }

        private async void LoadListDetails()
        {
            shoppinglist = ShoppingListService.Instance.ShoppingLists.FirstOrDefault(r => r.Id == listId);
            if (shoppinglist != null)
            {
                ListName = shoppinglist.Name;
                ShoppingItems = new ObservableCollection<ShoppingItem>(shoppinglist.Items);
            }
            OnPropertyChanged(nameof(ListName));
            OnPropertyChanged(nameof(ShoppingItems));

        }
        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(NewItemName))
            {
                ShoppingItems.Add(new ShoppingItem{Name = NewItemName.Trim(), IsChecked = false});
                NewItemName = string.Empty;
            }
        }

        private void DeleteItem(object item)
        {
            if (item is ShoppingItem shoppingItem && ShoppingItems.Contains(shoppingItem))
            {
                ShoppingItems.Remove(shoppingItem);
            }
        }
        private async Task SaveShoppingList()
        {
            if (string.IsNullOrWhiteSpace(ListName))
            {
                await Shell.Current.DisplayAlert("Error", "List name is required.", "OK");
                return;
            }

            var list = new ShoppingList
            {
                Id = listId ?? 0,
                Name = ListName
            };

            await ShoppingListService.Instance.AddNewList(list);

            var dbItems = shoppinglist.Items ?? new List<ShoppingItem>();

            var removedItems = dbItems.Where(dbItem => !ShoppingItems.Any(uiItem => uiItem.Id == dbItem.Id)).ToList();

            foreach (var removedItem in removedItems)
            {
                await ShoppingListService.Instance.DeleteItem(removedItem);
            }

            foreach (var item in ShoppingItems)
            {
                item.ShoppingListId = list.Id;
                await ShoppingListService.Instance.AddNewItem(item); // wywołuje SaveItemAsync / insert/update
            }

            await Shell.Current.DisplayAlert("Success", "Shopping list saved!", "OK");
            await Shell.Current.GoToAsync("//ShoppingListsPage");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}

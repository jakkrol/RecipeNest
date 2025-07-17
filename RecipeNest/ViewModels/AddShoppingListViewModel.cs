using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using RecipeNest.Models;
using RecipeNest.Services;
using Microsoft.Maui.Controls;

namespace RecipeNest.ViewModels
{
    public class AddShoppingListViewModel : INotifyPropertyChanged
    {
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
        public ICommand SaveShoppingListCommand { get; }

        public AddShoppingListViewModel()
        {
            AddItemCommand = new Command(AddItem);
            SaveShoppingListCommand = new Command(async () => await SaveShoppingList());
        }

        private void AddItem()
        {
            if (!string.IsNullOrWhiteSpace(NewItemName))
            {
                ShoppingItems.Add(new ShoppingItem{Name = NewItemName.Trim(), IsChecked = false});
                NewItemName = string.Empty;
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
                Name = ListName
                //CreatedAt = DateTime.Now
            };

            await ShoppingListService.Instance.AddNewList(list);

            foreach (var item in ShoppingItems)
            {
                item.ShoppingListId = list.Id;
                await ShoppingListService.Instance.AddNewItem(item);
            }

            await Shell.Current.DisplayAlert("Success", "Shopping list saved!", "OK");

            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

    }
}

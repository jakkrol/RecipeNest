using RecipeNest.DbConfig;
using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.Services
{
    public class ShoppingListService
    {
        private static ShoppingListService _instance;
        public static ShoppingListService Instance
        {
            get { 
                if( _instance == null)
                {
                    _instance = new ShoppingListService();
                }
                return _instance;
            }
        }

        public ObservableCollection<ShoppingList> ShoppingLists { get; }
        private readonly RecipeNestDb _db;
        public ShoppingListService()
        {
            ShoppingLists = new ObservableCollection<ShoppingList>();
            _db = new RecipeNestDb();
        }

        public async Task LoadShoppingListsFromDb()
        {
            var listsFromDb = await _db.GetItemsAsync<ShoppingList>();
            ShoppingLists.Clear();
            foreach (var item in listsFromDb)
            {
                ShoppingLists.Add(item);
            }
        }

        public async Task AddNewList(ShoppingList shoppingList)
        {
            await _db.SaveItemAsync<ShoppingList>(shoppingList);
            await LoadShoppingListsFromDb();
        }

        public async Task AddNewItem(ShoppingItem shoppingItem)
        {
            await _db.SaveItemAsync<ShoppingItem>(shoppingItem);
            await LoadShoppingListsFromDb();
        }

        public async Task DeleteItem(ShoppingItem shoppingItem)
        {
            Debug.WriteLine("DELETING ITEM: " + shoppingItem.Name);
            await _db.DeleteItemAsync<ShoppingItem>(shoppingItem);
            await LoadShoppingListsFromDb();
        }
        public async Task UpdateList(ShoppingList shoppingList)
        {

        }
        public async Task CheckItem(ShoppingItem shoppingItem)
        {
            Debug.WriteLine("ITEMEK: " + shoppingItem.Name);
            //shoppingItem.IsChecked = !shoppingItem.IsChecked;

            await _db.checkItemInList(shoppingItem);
        }
    }
}

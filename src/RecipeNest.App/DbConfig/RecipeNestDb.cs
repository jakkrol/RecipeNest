using RecipeNest.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.DbConfig
{
    public class RecipeNestDb
    {
        SQLiteAsyncConnection database;

        private async Task Init()
        {
            if (database is not null)
            {
                return;
            }

            //if(File.Exists(Constants.DatabasePath)) { File.Delete(Constants.DatabasePath); }

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await database.CreateTableAsync<Recipe>();
            await database.CreateTableAsync<ShoppingList>();
            await database.CreateTableAsync<ShoppingItem>();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : class, IEntity, new()
        {
            await Init();
            //return await database.Table<T>().ToListAsync();
            return await database.GetAllWithChildrenAsync<T>(recursive: true);
        }
        public async Task<int> SaveItemAsync<T>(T item) where T : IEntity
        {
            await Init();
            int result;

            if (item.Id != Guid.Empty)
            {
                Debug.WriteLine($"Próba UPDATE dla Id: {item.Id}");
                result = await database.UpdateAsync(item);
            }
            else
            {
                item.Id = Guid.NewGuid();
                Debug.WriteLine($"Próba INSERT z nowym Id: {item.Id}");
                result = await database.InsertAsync(item);
            }

            Debug.WriteLine($"WYNIK OPERACJI: {result}"); // Powinno być 1
            return result;
        }
        public async Task<int> DeleteItemAsync<T>(T item)
        {
            Debug.WriteLine("DELETING ITEM FROM DB");
            await Init();
            return await database.DeleteAsync(item);
        }

        public async Task checkItemInList<T>(T item)
        {
            //shoppingItem.IsChecked = !shoppingItem.IsChecked;

            // Save change to DB (SQLite example)
            await database.UpdateAsync(item);
        }
    }
}

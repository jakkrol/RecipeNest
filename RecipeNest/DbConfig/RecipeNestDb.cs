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

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await database.CreateTableAsync<Recipe>();
            await database.CreateTableAsync<ShoppingList>();
            await database.CreateTableAsync<ShoppingItem>();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : new()
        {
            await Init();
            //return await database.Table<T>().ToListAsync();
            return await database.GetAllWithChildrenAsync<T>(recursive: true);
        }
        public async Task<int> SaveItemAsync<T>(T item) where T : IEntity
        {
            await Init();
            if (item.Id != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }
        public async Task<int> DeleteItemAsync<T>(T item)
        {
            Debug.WriteLine("DELETING ITEM FROM DB");
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}

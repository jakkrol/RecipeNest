using RecipeNest.Models;
using SQLite;
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
            var result = await database.CreateTableAsync<Recipe>();
        }

        public async Task<List<Recipe>> GetItemsAsync()
        {
            await Init();
            return await database.Table<Recipe>().ToListAsync();
        }
        public async Task<int> SaveItemAsync(Recipe item)
        {
            await Init();
            if (item.Id != 0)
                return await database.UpdateAsync(item);
            else
                return await database.InsertAsync(item);
        }
        public async Task<int> DeleteItemAsync(Recipe item)
        {
            Debug.WriteLine("DELETING ITEM FROM DB");
            await Init();
            return await database.DeleteAsync(item);
        }
    }
}

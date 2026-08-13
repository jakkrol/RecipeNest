using RecipeNest.DbConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.BackendServices
{
    public class SyncDbData : BaseApiService
    {
        private RecipeNestDb _localDb;
        public SyncDbData(HttpClient httpClient, RecipeNestDb localDb) : base(httpClient)
        {
            _localDb = localDb;
        }
        public void OverrideLocalDbData()
        {
            Console.WriteLine("Nadpisuje lokalną bazę");
        }
        public void OverrideOnlineDbData() 
        {
            Console.WriteLine("Nadpisuje bazę w chmurze");
        }

    }
}

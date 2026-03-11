using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Shared.DTO;

namespace RecipeNest.BackendServices
{
    public class AuthService : BaseApiService
    {
        public AuthService(HttpClient httpClient) : base(httpClient) { }



    }
}

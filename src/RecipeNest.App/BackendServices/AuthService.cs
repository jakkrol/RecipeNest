using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using RecipeNest.Shared.DTO;
using RecipeNest.Services;

namespace RecipeNest.BackendServices
{
    public class AuthService : BaseApiService
    {
        private readonly UserSession _userSession;
        public AuthService(HttpClient httpClient, UserSession userSession) : base(httpClient) 
        {
            _userSession = userSession;
        }

        public async Task<bool> Login(LoginDTO loginuUser)
        {

            var response = await _httpClient.PostAsJsonAsync("api/User/login", loginuUser);
            Debug.WriteLine("THIS IS RESPONSE:   " + response);
            return true;
        }
    }
}

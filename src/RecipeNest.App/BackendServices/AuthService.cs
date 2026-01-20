using RecipeNest.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.BackendServices
{
    public class AuthService
    {
        HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<User>> getUsers()
        {
            List<User> users = await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5264/api/User");
            Debug.WriteLine(users[0].Name);
            return users ?? new List<User>();
        }

        public async Task addUser()
        {
            User user = new User {
                Name = "test",
                Login = "test2",
                Password = "test3",
            };
            await _httpClient.PostAsJsonAsync<User>("http://localhost:5264/api/User", user);
        }
    }
}

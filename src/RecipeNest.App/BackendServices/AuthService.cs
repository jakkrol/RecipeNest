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
    public class AuthService : BaseApiService
    {
        public AuthService(HttpClient httpClient) : base(httpClient) { }


        public async Task<List<User>> getUsers()
        {
            List<User> users = await _httpClient.GetFromJsonAsync<List<User>>("http://localhost:5264/api/User") ?? new List<User>();
            //Debug.WriteLine(users[0].Name);
            return users;
        }

        public async Task<User> getUser(int id)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"http://localhost:5264/api/User/{id}");
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
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

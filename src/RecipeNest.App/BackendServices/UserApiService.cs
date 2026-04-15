using RecipeNest.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace RecipeNest.BackendServices
{
    public class UserApiService : BaseApiService
    {
        public UserApiService(HttpClient httpClient) : base(httpClient) { }


        public async Task<UserDTO> getUser(Guid id)
        {
            var user = await _httpClient.GetFromJsonAsync<UserDTO>($"api/User/{id}");
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User not found");
            }
        }
        public async Task<List<UserDTO>> getUsers()
        {
            List<UserDTO> users = await _httpClient.GetFromJsonAsync<List<UserDTO>>("api/User") ?? new List<UserDTO>();
            //Debug.WriteLine(users[0].Name);

            return users;
        }
        public async Task addUser()
        {
            RegisterDTO user = new RegisterDTO
            {
                Name = "test",
                Login = "test2",
                Password = "test3",
            };
            await _httpClient.PostAsJsonAsync<RegisterDTO>("api/User", user);
        }

    }
}

using FCG.Notification.Application.Dto.Dto.User;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace FCG.Notification.Application.UseCases.Services
{
    public class UserApiService
    {
        private readonly HttpClient _httpClient;

        public UserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto> GetUserAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"api/user/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}

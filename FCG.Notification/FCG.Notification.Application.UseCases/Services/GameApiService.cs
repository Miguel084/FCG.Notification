using FCG.Notification.Application.Dto.Dto.Game;
using FCG.Notification.Application.Dto.Dto.User;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace FCG.Notification.Application.UseCases.Services
{
    public class GameApiService
    {
        private readonly HttpClient _httpClient;

        public GameApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GameDto> GetGameAsync(int gameId)
        {
            var response = await _httpClient.GetAsync($"api/game/{gameId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<GameDto>();
        }
    }
}

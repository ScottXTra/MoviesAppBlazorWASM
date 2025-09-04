using System.Net.Http.Json;
using MoviesApp.Shared.DTOs;

namespace MoviesApp.Client.Services
{
    public class MissionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "api/missions";

        public MissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MissionDto>> GetMissionsAsync()
        {
            try
            {
                var missions = await _httpClient.GetFromJsonAsync<List<MissionDto>>(ApiEndpoint);
                return missions ?? new List<MissionDto>();
            }
            catch (Exception)
            {
                return new List<MissionDto>();
            }
        }

        public async Task<MissionDto?> GetMissionAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MissionDto>($"{ApiEndpoint}/{id}");
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> CreateMissionAsync(CreateMissionDto mission)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(ApiEndpoint, mission);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMissionAsync(int id, UpdateMissionDto mission)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{ApiEndpoint}/{id}", mission);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteMissionAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{ApiEndpoint}/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

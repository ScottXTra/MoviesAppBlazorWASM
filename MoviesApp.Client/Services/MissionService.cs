using System.Net.Http.Json;
using MoviesApp.Shared.DTOs;
using System.Linq;

namespace MoviesApp.Client.Services
{
    public class MissionService
    {
        private readonly HttpClient _httpClient;
        private const string ApiEndpoint = "api/missions";

        // Mock mission data for debugging the missions screen
        private static readonly List<MissionDto> _mockMissions = new()
        {
            new MissionDto
            {
                Id = 1,
                Name = "FenderLH",
                Destination = "Left Hand Fender",
                LaunchDate = DateTime.Now.AddDays(-2),
                Status = "Pending",
                AssignedRobot = "Robot_1",
                MissionType = "Delivery"
            },
            new MissionDto
            {
                Id = 2,
                Name = "FenderRH",
                Destination = "Right Hand Fender",
                LaunchDate = DateTime.Now.AddDays(-1),
                Status = "Pending",
                AssignedRobot = "Robot_2",
                MissionType = "Delivery"
            },
            new MissionDto
            {
                Id = 3,
                Name = "Hood",
                Destination = "Hood",
                LaunchDate = DateTime.Now,
                Status = "Pending",
                AssignedRobot = "Robot_3",
                MissionType = "Delivery"
            }
        };

        public MissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MissionDto>> GetMissionsAsync()
        {
            // Return mock missions instead of calling the API
            await Task.Delay(200); // Simulate network latency
            return _mockMissions;
        }

        public async Task<MissionDto?> GetMissionAsync(int id)
        {
            // Retrieve a single mission from the mock data set
            await Task.Delay(100); // Simulate network latency
            return _mockMissions.FirstOrDefault(m => m.Id == id);
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

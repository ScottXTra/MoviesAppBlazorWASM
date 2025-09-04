using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Server.Data;
using MoviesApp.Server.Hubs;
using MoviesApp.Shared.DTOs;
using MoviesApp.Shared.Models;

namespace MoviesApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MissionsController : ControllerBase
    {
        private readonly MoviesDbContext _context;
        private readonly IHubContext<MissionHub> _hub;

        public MissionsController(MoviesDbContext context, IHubContext<MissionHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDto>>> GetMissions()
        {
            var missions = await _context.Missions
                .Select(m => new MissionDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    Destination = m.Destination,
                    LaunchDate = m.LaunchDate,
                    Status = m.Status
                })
                .ToListAsync();

            return Ok(missions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MissionDto>> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            var missionDto = new MissionDto
            {
                Id = mission.Id,
                Name = mission.Name,
                Destination = mission.Destination,
                LaunchDate = mission.LaunchDate,
                Status = mission.Status
            };

            return Ok(missionDto);
        }

        [HttpPost]
        public async Task<ActionResult<MissionDto>> CreateMission(CreateMissionDto createDto)
        {
            var mission = new Mission
            {
                Name = createDto.Name,
                Destination = createDto.Destination,
                LaunchDate = createDto.LaunchDate,
                Status = createDto.Status
            };

            _context.Missions.Add(mission);
            await _context.SaveChangesAsync();

            var missionDto = new MissionDto
            {
                Id = mission.Id,
                Name = mission.Name,
                Destination = mission.Destination,
                LaunchDate = mission.LaunchDate,
                Status = mission.Status
            };

            await _hub.Clients.All.SendAsync("MissionCreated", missionDto);

            return CreatedAtAction(nameof(GetMission), new { id = mission.Id }, missionDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMission(int id, UpdateMissionDto updateDto)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            mission.Name = updateDto.Name;
            mission.Destination = updateDto.Destination;
            mission.LaunchDate = updateDto.LaunchDate;
            mission.Status = updateDto.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MissionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            var missionDto = new MissionDto
            {
                Id = mission.Id,
                Name = mission.Name,
                Destination = mission.Destination,
                LaunchDate = mission.LaunchDate,
                Status = mission.Status
            };

            await _hub.Clients.All.SendAsync("MissionUpdated", missionDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);

            if (mission == null)
            {
                return NotFound();
            }

            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("MissionDeleted", id);

            return NoContent();
        }

        private bool MissionExists(int id)
        {
            return _context.Missions.Any(e => e.Id == id);
        }
    }
}

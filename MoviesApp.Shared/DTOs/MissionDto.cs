namespace MoviesApp.Shared.DTOs
{
    public class MissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public string? Status { get; set; }
    }

    public class CreateMissionDto
    {
        public string Name { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public string? Status { get; set; }
    }

    public class UpdateMissionDto
    {
        public string Name { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime LaunchDate { get; set; }
        public string? Status { get; set; }
    }
}

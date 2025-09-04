using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Shared.Models
{
    public class Mission
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Destination is required")]
        [StringLength(100, ErrorMessage = "Destination cannot exceed 100 characters")]
        public string Destination { get; set; } = string.Empty;

        [Required(ErrorMessage = "Launch date is required")]
        public DateTime LaunchDate { get; set; } = DateTime.Today;

        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string? Status { get; set; }
    }
}

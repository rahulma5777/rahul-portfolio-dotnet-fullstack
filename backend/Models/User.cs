using System.ComponentModel.DataAnnotations;

namespace NetFullStack.API.Models
{
    /// <summary>
    /// Represents a person who can own multiple task items.  This model
    /// illustrates a one‑to‑many relationship between users and tasks.
    /// </summary>
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        // Navigation property to the related tasks
        public ICollection<TaskItem> TaskItems { get; set; } = new List<TaskItem>();
    }
}
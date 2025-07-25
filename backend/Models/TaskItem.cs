using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetFullStack.API.Models
{
    /// <summary>
    /// Represents a simple task or todo item belonging to a user.
    /// </summary>
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        // Foreign key to the owning user
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model
{
    public class Course
    {
        [Key]
        public int LessonId { get; set; }

        [Required]
        [StringLength(100)] // Adjust the maximum length as needed
        public string LessonTitle { get; set; }

        [Required]
        public string LessonContent { get; set; }
        public int UserId { get; set; } // Foreign key
        [ForeignKey("UserId")]
        public User User { get; set; } // Navigation propert
    }
}

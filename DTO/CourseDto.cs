using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTO
{
    public class CourseDto
    {
        public int LessonId { get; set; }

    
        public string LessonTitle { get; set; }
 
        public string LessonContent { get; set; }
    }
}

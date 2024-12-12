using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Dal.Entities
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Color { get; set; } = "#ffffff";
        public Guid UserId { get; set; }
        public bool IsPublic { get; set; } = false; // Flag for public access

        // Навигационные свойства
        public ICollection<NoteShare>? Shares { get; set; }
    }
}

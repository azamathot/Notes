using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notes.Dal.Entities
{
    public class NoteShare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Автоинкремент
        public int Id { get; set; }
        public int NoteId { get; set; }
        public Guid UserId { get; set; }
        public bool CanEdit { get; set; }
    }
}

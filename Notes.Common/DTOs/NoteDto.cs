namespace Notes.Common.DTOs
{
    public class NoteDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Color { get; set; } = "#ffffff";
        public bool IsPublic { get; set; } = false; // Flag for public access
        public Guid UserId { get; set; }
        public ICollection<NoteShareDto>? Shares { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; }

        // Дополнительные свойства, если нужно
    }
}

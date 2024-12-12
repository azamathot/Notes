namespace Notes.Common.DTOs
{
    public class NoteDto2
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public string Color { get; set; } = "#ffffff";
        public bool IsPublic { get; set; } = false; // Flag for public access
        public Guid UserId { get; set; }
    }
}

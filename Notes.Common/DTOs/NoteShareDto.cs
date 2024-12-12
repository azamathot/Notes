namespace Notes.Common.DTOs
{
    public class NoteShareDto
    {
        public int NoteId { get; set; }
        public Guid UserId { get; set; }
        public bool CanEdit { get; set; }
    }
}

namespace Notes.Common.DTOs
{
    public class ShareItemDto
    {
        public Guid UserId { get; set; }  // Keycloak User ID
        public bool CanEdit { get; set; }
    }
}

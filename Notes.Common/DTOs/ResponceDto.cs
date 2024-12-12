namespace Notes.Common.DTOs
{
    public class ResponceDto<T>
    {
        public bool IsSuccess => Status == StatusCode.Success;
        public StatusCode Status { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }

    public enum StatusCode { Success, NotFound, Forbid, Unauthorize }
}

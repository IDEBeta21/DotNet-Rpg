namespace DotNetRPG.API.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }

        //To tell the front end if the response went right
        public bool Success { get; set; } = true;
        //To send explanation or message (i.e. in case of an error)
        public string Message { get; set; } = string.Empty;
    }
}
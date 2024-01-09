namespace ParamTech.Core.Persistence.Utilities.Response;
public class Result : IResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Json { get; set; }
    public Result()
    {

    }

    public Result(bool success)
    {
        Success = success;
    }

    public Result(string message)
    {
        Message = message;
    }

    public Result(bool success, string message) : this(success)
    {
        Message = message;
    }

    public Result(bool success, string message, string json) : this(success, message)
    {
        Json = json;
    }

   
}
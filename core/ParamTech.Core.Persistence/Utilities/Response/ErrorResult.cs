namespace ParamTech.Core.Persistence.Utilities.Response;
public class ErrorResult : Result
{
    public ErrorResult() : base(false)
    {
    }

    public ErrorResult(bool success) : base(false)
    {
    }

    public ErrorResult(string message) : base(false, message)
    {
    }

    public ErrorResult(bool success, string message) : base(false, message)
    {
    }

    public ErrorResult(bool success, string message, string json) : base(false, message, json)
    {
    }
}
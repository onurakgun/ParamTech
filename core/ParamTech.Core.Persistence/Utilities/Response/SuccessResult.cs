namespace ParamTech.Core.Persistence.Utilities.Response;
public class SuccessResult : Result
{
    public SuccessResult() : base(true)
    {
    }

    public SuccessResult(bool success) : base(true)
    {
    }

    public SuccessResult(string message) : base(true, message)
    {
    }

    public SuccessResult(string message, string json) : base(true, message, json)
    {
    }
}
namespace ParamTech.Core.Persistence.Utilities.Response;
public interface IResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public string Json { get; set; }
}
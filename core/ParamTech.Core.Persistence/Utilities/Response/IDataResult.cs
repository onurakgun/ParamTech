namespace ParamTech.Core.Persistence.Utilities.Response;
public interface IDataResult<T> : IResult
{
    T Data { get; set; }
}
namespace ParamTech.Core.Persistence.Utilities.Response;
public class DataResult<T> : Result, IDataResult<T>
{
    public T Data { get ; set ; }

    public DataResult(bool success, T data) : base(success)
    {
        Data = data;
    }
    public DataResult( bool success, T data, string message) : base(success, message)
    {
        Data = data;
    }
}
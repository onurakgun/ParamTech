using ArxOne.MrAdvice.Advice;
using MethodBoundaryAspect.Fody.Attributes;
using Microsoft.Extensions.DependencyInjection;
using ParamTech.Core.Persistence.CrosCuttingConcerns.Caching;
using ParamTech.Core.Persistence.Utilities.Ioc;
using ParamTech.Core.Persistence.Utilities.Response;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
namespace ParamTech.Core.Persistence.Aspect;
public class RedisCacheAspect<T> : Attribute, IMethodAdvice
{
    private readonly ICacheManager _cacheManager;
    private readonly int Sure;
    private readonly bool Async;
    public RedisCacheAspect(bool async = true, int sure = 365)
    {
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>()!;
        Async = async;
        Sure = sure;
    }
    public void Advise(MethodAdviceContext args)
    {
        string metotAdi = string.Format("{0}.{1}.{2}", args.TargetMethod.ReflectedType!.Namespace, args.TargetMethod.ReflectedType.Name, args.TargetMethod.Name);
        List<object> arguments = args.Arguments.ToList();
        string key = $"{metotAdi}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
        Type dataType = typeof(T);
        object instance;
        Type[] interfaces = dataType.GetInterfaces();
        //if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(IDataResult<>))
        if (dataType.IsGenericType && dataType.GetGenericTypeDefinition() == typeof(IDataResult<>))
        {
            Type[] genericArguments = dataType.GetGenericArguments();
            Type concreteType = typeof(SuccessDataResult<>).MakeGenericType(genericArguments[0]);
            instance = Activator.CreateInstance(concreteType);
        }
        else if (dataType.GetGenericTypeDefinition() == typeof(IResult))
        {

        }
        else
        {
            throw new InvalidOperationException($"Type {dataType}  IDataResult<T>.");
        }

        //RedisValue cacheControl = (RedisValue)_cacheManager.Get(key);
        //if (!cacheControl.IsNullOrEmpty)
        //{
        //    var model = JsonConvert.DeserializeObject<SuccessDataResult<T>>(cacheControl.ToString());
        //    if (Async)
        //    {
        //        args.ReturnValue = Task.FromResult(model);
        //        return;
        //    }
        //    args.ReturnValue = model;
        //    return;
        //}
        //else
        //{
        //    args.Proceed();
        //    //CACHE YOKSA
        //    var ReturnValue = args.ReturnValue;
        //    IDataResult<T> result;
        //    object data;
        //    if (Async)
        //    {
        //        object response = ReturnValue.GetType().GetProperty("Result").GetValue(ReturnValue);
        //        var datası = response.GetType().GetProperties()[0].GetValue(response);
        //        result = IDataResultAsync(response, datası);
        //    }
        //    else
        //    {
        //        var responseData = ReturnValue.GetType().GetProperties()[0].GetValue(ReturnValue);
        //        result = IDataResultNoAsync(ReturnValue, ReturnValue);

        //    }
        //    _cacheManager.Add(key, result, Sure);
        //}
    }

    public SuccessDataResult<T> IDataResultAsync(object response, object datası)
    {
        var Success = response.GetType().GetProperty("Success").GetValue(response);
        var Message = response.GetType().GetProperty("Message").GetValue(response);
        var result = new SuccessDataResult<T>
        {
            Message = Message == null ? null : Message.ToString(),
            Success = (bool)Success,
        };
        return result;
    }

    public SuccessDataResult<T> IDataResultNoAsync(object property, object responseData)
    {
        var json = property.GetType().GetProperty("Json").GetValue(property);
        var Success = property.GetType().GetProperty("Success").GetValue(property);
        var Message = property.GetType().GetProperty("Message").GetValue(property);
        var Data = property.GetType().GetProperty("Data").GetValue(property);
        var result = new SuccessDataResult<T>
        {
            Json = json.ToString(),
            Data = Data == null ? default(T) : (T)Data,
            Message = Message == null ? null : Message.ToString(),
            Success = (bool)Success,
        };
        return result;
    }
}
using ParamTech.Core.Persistence.CrosCuttingConcerns.Loging;
using ParamTech.Core.Persistence.Utilities.Response;
using MethodBoundaryAspect.Fody.Attributes;
using System.Transactions;
namespace ParamTech.Core.Persistence.Aspect;
public class TransactionAspect: OnMethodBoundaryAspect
{
    #region CONSTRACTOR
    private readonly Type _type;
    private readonly bool _async;
    public TransactionAspect(Type type, bool async = true)
    {
        _type = type;
        _async = async;
    }
    public TransactionAspect( bool async = true)
    {
        _async = async;
    }
    #endregion

    public override void OnEntry(MethodExecutionArgs arg)
    {
        TransactionScope transactionScope = new(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
        arg.MethodExecutionTag = transactionScope;
    }

    public override void OnExit(MethodExecutionArgs arg)
    {
        TransactionScope transactionScope = (TransactionScope)arg.MethodExecutionTag;
        if (_async)
        {
            var result = arg.ReturnValue.GetType().GetProperty("Result").GetValue(arg.ReturnValue);
            var Excaption = arg.ReturnValue.GetType().GetProperty("Exception").GetValue(arg.ReturnValue);
            var Success = result.GetType().GetProperty("Success").GetValue(result);
            if (Excaption == null)
            {
                if ((bool)Success == true)
                {
                    transactionScope.Complete();
                    transactionScope.Dispose();
                }
                else
                {
                    transactionScope.Dispose();
                }
            }
            else
            {
                transactionScope.Dispose();
            }
        }
        else
        {
            Exception Excaptions = arg.Exception;
            if (Excaptions != null)
            {
                transactionScope.Dispose();
            }
            else
            {
                var Success = arg.ReturnValue.GetType().GetProperty("Success").GetValue(arg.ReturnValue);
                if ((bool)Success == true)
                {
                    transactionScope.Complete();
                    transactionScope.Dispose();
                }
                else
                {
                    transactionScope.Dispose();
                }
            }
        }
    }

    public override void OnException(MethodExecutionArgs arg)
    {
        TransactionScope transactionScope = (TransactionScope)arg.MethodExecutionTag;
        Exception excaptions = arg.Exception;
        if (_type != null)
        {
            if (excaptions != null)
            {
                ITransactionLogService logService = (ITransactionLogService)Activator.CreateInstance(_type);
                logService.LogInsert(excaptions);
                transactionScope.Dispose();
            }
            else
            {
                if (_async)
                {
                    Exception response = (Exception)arg.ReturnValue.GetType().GetProperty("Exception").GetValue(arg.ReturnValue);
                    ITransactionLogService logService = (ITransactionLogService)Activator.CreateInstance(_type);
                    logService.LogInsert(response);
                    transactionScope.Dispose();
                }
                else
                {
                    Exception Excaptions = arg.Exception;
                    ITransactionLogService logService = (ITransactionLogService)Activator.CreateInstance(_type);
                    logService.LogInsert(Excaptions);
                    transactionScope.Dispose();
                }
            }
            arg.ReturnValue = new ErrorResult(false, message: excaptions.Message);
        }
        arg.ReturnValue = new ErrorResult(false, message: excaptions.Message);
    }
}
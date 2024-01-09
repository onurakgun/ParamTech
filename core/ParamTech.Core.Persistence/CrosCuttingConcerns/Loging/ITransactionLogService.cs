namespace ParamTech.Core.Persistence.CrosCuttingConcerns.Loging;
public interface ITransactionLogService
{
    void LogInsert(Exception exception);
}

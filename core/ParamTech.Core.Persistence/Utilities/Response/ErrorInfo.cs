using System;

namespace ParamTech.Core.Persistence.Utilities.Response;
public class ErrorInfo
{
    public Guid? ErrorLogId { get; set; }
    public string Message { get; set; }
    public string InnerException { get; set; }
    public string StackTrace { get; set; }
    public string Source { get; set; }
    public string Data { get; set; }
    public string TargetSite { get; set; }
    public DateTime? InsertDate { get; set; }
}
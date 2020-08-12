using System;

namespace AbpTemplate.Response
{
    public class ServiceResult
    {
        public ServiceResultCode Code { get; set; }

        public string Message { get; set; }

        public bool Success => Code == ServiceResultCode.Succeed;

        public long Timestamp { get; } = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public void IsSuccess(string message = "")
        {
            Message = message;
            Code = ServiceResultCode.Succeed;
        }

        public void IsFailed(string message = "")
        {
            Message = message;
            Code = ServiceResultCode.Failed;
        }

        public void IsFailed(Exception exception)
        {
            Message = exception.InnerException?.StackTrace;
            Code = ServiceResultCode.Failed;
        }
    }
}
namespace AbpTemplate.Response
{
    public class ServiceResult<TResult> : ServiceResult where TResult : class
    {
        public TResult Result { get; set; }

        public void IsSuccess(TResult result = null, string message = "")
        {
            Message = message;
            Code = ServiceResultCode.Succeed;
            Result = result;
        }
    }
}
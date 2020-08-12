using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.DependencyInjection;

namespace AbpTemplate
{
    public class ApplicationCachingServiceBase : ITransientDependency
    {
        public IDistributedCache Cache { get; set; }
    }
}
using AbpTemplate.Configuration;
using Volo.Abp.Data;

namespace AbpTemplate.Attribute
{
    public class ConnectionStringAttribute : ConnectionStringNameAttribute
    {
        private static readonly string storage = AppSettings.EnabledStorage;

        public ConnectionStringAttribute(string name = "") : base(name)
        {
            Name = string.IsNullOrEmpty(name) ? storage : name;
        }

        public new string Name { get; }
    }
}
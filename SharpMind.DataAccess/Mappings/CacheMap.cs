using SharpMind.DataAccess.Infrastructure;

namespace SharpMind.DataAccess.Mappings
{
    [Document(Name = "cache")]
    internal class CacheMap : MapBase
    {
        public string Key { get; set; }

        public object Value { get; set; }
    }
}

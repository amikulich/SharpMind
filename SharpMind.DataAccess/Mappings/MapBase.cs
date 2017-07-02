using MongoDB.Bson;

namespace SharpMind.DataAccess.Mappings
{
    internal abstract class MapBase
    {
        protected MapBase()
        {
        }

        public ObjectId Id { get; set; }
    }
}

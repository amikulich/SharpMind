using System.Collections.Generic;
using SharpMind.DataAccess.Infrastructure;

namespace SharpMind.DataAccess.Mappings
{
    [Document(Name = "predefined_response")]
    internal class PredefinedResponseMap : MapBase
    {
        public PredefinedResponseMap()
        {
            Keys = new List<string>();
            AlternativeResponses = new List<string>();
        }

        public string Response { get; set; }

        public IEnumerable<string> Keys { get; set; }

        public IEnumerable<string> AlternativeResponses { get; set; }
    }
}

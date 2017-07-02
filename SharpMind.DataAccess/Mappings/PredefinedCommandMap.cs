using System.Collections.Generic;
using SharpMind.DataAccess.Infrastructure;

namespace SharpMind.DataAccess.Mappings
{
    [Document(Name = "predefined_command")]
    internal class PredefinedCommandMap : MapBase
    {
        public PredefinedCommandMap()
        {
            Keys = new List<string>();
            AlternativeResponses = new List<string>();
        }

        public string Response { get; set; }

        public IEnumerable<string> Keys { get; set; }

        public IEnumerable<string> AlternativeResponses { get; set; }
    }
}

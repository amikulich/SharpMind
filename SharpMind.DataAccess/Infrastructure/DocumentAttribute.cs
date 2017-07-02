using System;

namespace SharpMind.DataAccess.Infrastructure
{
    public class DocumentAttribute : Attribute
    {
        public string Name { get; set; }
    }
}

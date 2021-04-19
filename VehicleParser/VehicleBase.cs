using System;
using System.Collections.Generic;

namespace VehicleParser
{
    public class VehicleBase
    {
        public string id { get; set; }
        public string name { get; set; }
        public string group { get; set; }
        public Dictionary<string, string[]> list { get; set; }
    }

    public class JsonPropertyAttribute : Attribute
    {
        public JsonPropertyAttribute(string id)
        {
            throw new NotImplementedException();
        }
    }
}

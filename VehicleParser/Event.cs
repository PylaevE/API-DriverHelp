using System;
using System.Collections.Generic;

#nullable disable

namespace VehicleParser
{
    public partial class Event
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int EventType { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public string Need { get; set; }
        public string CreateDate { get; set; }

        public virtual User Author { get; set; }
    }
}

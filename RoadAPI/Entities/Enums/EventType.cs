using System;

namespace RoadAPI.Entities.Enums
{
    [Flags]
    public enum EventType
    {
        Police = 1 << 0,
        Help = 1 << 1,
        Meeting = 1 << 2
    }
}
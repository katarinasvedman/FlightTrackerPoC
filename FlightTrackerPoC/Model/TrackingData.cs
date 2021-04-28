using System;
using System.Collections.Generic;
using System.Text;

namespace FlightTrackerPoC.Model
{
    public class TrackingData
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Data { get; set; }
    }
}

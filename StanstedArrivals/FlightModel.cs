using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class FlightModel
    {
        //"FlightNumber": "FR3132",
        //"AorD": 0,
        //"Destination": "Paphos",
        //"Terminal": "T1",
        //"ScheduledDateTime": "2016-06-17T16:35:00",
        //"Status": "Estimated 04:25",
        //"CanFollow": true

        public string FlightNumber { get; set; }
        public int AorD { get; set; }
        public string Destination { get; set; }
        public string Terminal { get; set; }
        public DateTime ScheduledDateTime { get; set; }
        public string Status { get; set; }
        public bool CanFollow { get; set; }
    }
}

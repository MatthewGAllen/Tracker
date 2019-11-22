using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Data
{
    public class WorkDetailsDto
    {
        public WorkItem Works { get; set; }

        public List<ModLog> ModLogs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracker.Models
{
    public class WorkItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }

        public WorkItem()
        {
            this.CreatedAt = DateTime.Now;
        }
    }
}

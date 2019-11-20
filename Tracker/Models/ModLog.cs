using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Tracker.Models
{
    public class ModLog
    {
        public int Id { get; set; }
        public DateTime ModifiedAt { get; set; }

        [ForeignKey("WorkItem")]
        public int ItemId { get; set; }

        public WorkItem WorkItem { get; set; }

        public ModLog()
        {
            this.ModifiedAt = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PB.Core.DE
{
    public class FeedModel
    {
        public Guid Id { get; set; }
        public Guid VersionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public string Author { get; set; }
        public List<string> Editors { get; set; }
        public List<string> Viewers { get; set; }
    }
}

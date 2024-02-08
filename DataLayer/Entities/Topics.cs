using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Topics
    {
        [Key]
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public int TopicPoints { get; set; }
        public string TopicDescription { get; set; }
    }
}

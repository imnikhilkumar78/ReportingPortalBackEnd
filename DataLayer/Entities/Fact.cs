﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Fact
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Topicid { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }

        public ICollection<Employee>? Users { get; set; }

        public ICollection<Topics>? Topics { get; set; }
    }
}

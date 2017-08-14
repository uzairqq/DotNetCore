﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class TripViewModel
    {
        [Required]
        [StringLength(100,MinimumLength = 5)]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }=DateTime.UtcNow;
    }
}

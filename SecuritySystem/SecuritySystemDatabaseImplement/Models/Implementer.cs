﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecuritySystemDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }
        [Required]
        public string ImplementerFLM { get; set; }
        [Required]
        public int WorkingTime { get; set; }
        [Required]
        public int PauseTime { get; set; }
        [ForeignKey("ImplementerId")]
        public List<Order> Orders { get; set; }
    }
}
﻿
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebAPIExample.Models
{
    public class TodoItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Key { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime TargetDate { get; set; }
    }
}
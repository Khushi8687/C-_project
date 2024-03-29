﻿using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace C__project.Models
{
    public class Shift
    {
        [Key]
        public int ShiftID { get; set; }
        public string Day {  get; set; }


        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

       
        // Foreign key to Staff
        [ForeignKey("Staffs")]
        public int StaffID { get; set; }

      
        public virtual Staff Staffs { get; set; }
    }

    public class ShiftDto
    {
        public int ShiftID { get; set; }
        public string Day { get; set; }

        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


    }
}


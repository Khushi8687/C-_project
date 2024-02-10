using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace C__project.Models
{
    public class Positions
    {
        [Key]
        public int PositionID { get; set; }

        public string PositionName { get; set; }

        
    }
    public class PositionDto
    {
        public int PositionID { get; set; }

        public string PositionName { get; set; }

    }
}
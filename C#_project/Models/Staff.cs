using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C__project.Models
{
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }

        public string StaffName { get; set; }

        public string Email { get; set; }

        public int Contact { get; set; }

        private string position;

       

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public string Address { get; set; }

      

        [ForeignKey("Position")]
        public int PositionID { get; set; }

        public virtual Positions Position { get; set; }

        public ICollection<Shift> Shifts { get; set; }  

    }


    public class StaffDto
    {
        public int StaffID { get; set; }

        public string StaffName { get; set; }

        public string Email { get; set; }

        public int Contact { get; set; }

        public string PositionName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime HireDate { get; set; }

        public string Address { get; set; }

    }
}
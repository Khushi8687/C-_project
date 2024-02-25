using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C__project.Models.viewmodel
{
    public class DetailsStaff
    {

        public StaffDto SelectedStaff { get; set; }
        public IEnumerable<ShiftDto> ResponsibleShift { get; set; }

      
    }
}
using C__project.Models;
using System.Collections.Generic;

namespace C__project.Controllers
{
    internal class DetailsStaff
    {
        public DetailsStaff()
        {
        }

        public StaffDto selectedstaff { get; internal set; }
        public IEnumerable<ShiftDto> ResponsibleShift { get; internal set; }
    }
}
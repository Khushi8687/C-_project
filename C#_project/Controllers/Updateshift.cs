using C__project.Models;
using System.Collections.Generic;

namespace C__project.Controllers
{
    internal class Updateshift
    {
        public ShiftDto SelectedShift { get; set; }
        public IEnumerable<StaffDto> StaffList { get; set; }
    }
}
using C__project.Models;
using System.Collections.Generic;

namespace C__project.Controllers
{
    public class Updatestaff
    {
        IEnumerable<PositionDto> PositionList { get; set; }
        StaffDto SelectedStaff { get; set; }
    }
}
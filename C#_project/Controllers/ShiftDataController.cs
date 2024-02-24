using C__project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace C__project.Controllers
{
    public class ShiftDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //list shift
        [HttpGet]
        [Route("api/ShiftData/ListShift")]

        public List<ShiftDto> ListShift()
        {
            //query to db
            List<Shift> Shifts = db.Shifts.ToList();

            List<ShiftDto> ShiftDtos = new List<ShiftDto>();

            Shifts.ForEach(c => ShiftDtos.Add(new ShiftDto()
            {
                ShiftID = c.ShiftID,
                Day = c.Day,
                Date = c.Date,
                StartTime = c.StartTime,
                EndTime = c.EndTime
               

            }
                ));
            return ShiftDtos;
        }

        //find staff
        // GET: api/StaffData/FindStaff/2
        [ResponseType(typeof(Shift))]
        [HttpGet]
        [Route("api/ShiftData/findshift/{id}")]
        public IHttpActionResult FindShift(int id)
        {
            Shift Shift = db.Shifts.Find(id);

            if (Shift == null)
            {
                return NotFound();
            }

            ShiftDto ShiftDto = new ShiftDto()
            {
                ShiftID = Shift.ShiftID,
                Day = Shift.Day,
                Date = Shift.Date,
                StartTime = Shift.StartTime,
                EndTime = Shift.EndTime
                
            };

            return Ok(ShiftDto);
        }

        //add staff

        [ResponseType(typeof(Shift))]
        [HttpPost]

        public IHttpActionResult AddShift(Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Shifts.Add(shift);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = shift.ShiftID }, shift);
        }


        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/StaffData/updateshift/{id}")]
        public IHttpActionResult UpdateShift(int id, Shift shift)
        {
            Debug.WriteLine("I have reached the update shift method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != shift.ShiftID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + shift.ShiftID);
                Debug.WriteLine("POST parameter" + shift.Day);
                Debug.WriteLine("POST parameter" + shift.Date);
                Debug.WriteLine("POST parameter" + shift.StartTime);
                Debug.WriteLine("POST parameter" + shift.EndTime);
               
               
                return BadRequest();
            }

            db.Entry(shift).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!shiftExists(id))
                {
                    Debug.WriteLine("Shift not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        private bool shiftExists(int id)
        {
            throw new NotImplementedException();
        }

       // public listshiftforstaff


    }
}

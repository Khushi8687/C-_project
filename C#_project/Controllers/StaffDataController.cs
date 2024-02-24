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
    public class StaffDataController : ApiController
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        //list staff
        [HttpGet]
        [Route("api/StaffData/ListStaffs")]

        public List<StaffDto> ListStaffs()
        {
            //query to db
            List<Staff> Staffs = db.Staffs.ToList();

            List<StaffDto> StaffDtos = new List<StaffDto>();

            Staffs.ForEach(b => StaffDtos.Add(new StaffDto()
            {
                StaffID = b.StaffID,
                StaffName = b.StaffName,
                Email = b.Email,
                Contact = b.Contact,
                BirthDate = b.BirthDate,
                HireDate = b.HireDate,
                Address = b.Address,
                PositionName = b.Position.PositionName

            }
                ));
                return StaffDtos;
        }

        //find staff
        // GET: api/StaffData/FindStaff/2
        [ResponseType(typeof(Staff))]
        [HttpGet]
        [Route("api/StaffData/findstaff/{id}")]
        public IHttpActionResult FindStaff(int id)
        {
            Staff Staff = db.Staffs.Find(id);

            if (Staff == null)
            {
                return NotFound();
            }

            StaffDto StaffDto = new StaffDto()
            {
                StaffID = Staff.StaffID,
                StaffName = Staff.StaffName,
                Email = Staff.Email,
                Contact = Staff.Contact,
                BirthDate = Staff.BirthDate,
                HireDate = Staff.HireDate,
                Address = Staff.Address,
                PositionName= Staff.Position.PositionName
            };

            return Ok(StaffDto);
        }




        //add staff

        [ResponseType(typeof(Staff))]
        [HttpPost]
      
        public IHttpActionResult AddStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(staff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = staff.StaffID }, staff);
        }


        //update staff
        // POST: api/StaffData/UpdateAnimal/5
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/StaffData/updatestaff/{id}")]
        public IHttpActionResult UpdateStaff(int id, Staff staff)
        {
            Debug.WriteLine("I have reached the update staff method!");
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != staff.StaffID)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + staff.StaffID);
                Debug.WriteLine("POST parameter" + staff.StaffName);
                Debug.WriteLine("POST parameter" + staff.Email);
                Debug.WriteLine("POST parameter" + staff.Contact);
                Debug.WriteLine("POST parameter" + staff.BirthDate);
                Debug.WriteLine("POST parameter" + staff.HireDate);
                Debug.WriteLine("POST parameter" + staff.Address);
                return BadRequest();
            }

            db.Entry(staff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!staffExists(id))
                {
                    Debug.WriteLine("Staff not found");
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

        private bool staffExists(int id)
        {
            throw new NotImplementedException();
        }


        //delete staff

        [ResponseType(typeof(Staff))]
        [HttpPost]
        [Route("api/StaffData/deletestaff/{id}")]
        public IHttpActionResult DeleteStaff(int id)
        {
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(staff);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.StaffID == id) > 0;
        }

        
    }
}

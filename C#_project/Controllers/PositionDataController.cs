using C__project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace C__project.Controllers
{
   // this contains positionid and position name which staff table take information on staff position 
    public class PositionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //list staff
        [HttpGet]
        [Route("api/PositionData/ListPosition")]

        public List<PositionDto> ListPositions()
        {
            //query to db
            List<Positions> Positions = db.Positions.ToList();

            List<PositionDto> PostionDtos = new List<PositionDto>();

            Positions.ForEach(s => PostionDtos.Add(new PositionDto()
            {
                PositionID = s.PositionID,
                PositionName = s.PositionName

            }
                ));
            return PostionDtos;
        }

        [ResponseType(typeof(Positions))]
        [HttpGet]
        [Route("api/PositionData/findPosition/{id}")]
        public IHttpActionResult FindPosition(int id)
        {
            Positions positions = db.Positions.Find(id);

            if (positions == null)
            {
                return NotFound();
            }

            PositionDto PositionDto = new PositionDto()
            {
                PositionID = positions.PositionID,
                PositionName = positions.PositionName

            };

            return Ok(PositionDto);
        }
    }
}

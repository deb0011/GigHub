using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.DTO;

namespace GigHub.Controllers
{
    

    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Attend(AttendanceDto att)
        {
            int gigId = att.GigId;
            var userId = User.Identity.GetUserId();

            if(_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == gigId))
            {
                return BadRequest("Attendance already exists.");
            }

            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}

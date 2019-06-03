using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using GigHub.DTO;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();
        }

        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            //Check if following already exists
            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId))
                return BadRequest("You are already following this artist.");

            _context.Followings.Add(new Following {
                FolloweeId = dto.FolloweeId,
                FollowerId = userId
            });

            _context.SaveChanges();

            return Ok();
        }
    }
}

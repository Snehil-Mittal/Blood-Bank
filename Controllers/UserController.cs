using AutoMapper;
using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Dtos;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBankManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        IUserDetailsRepository repo;
        IMapper mapper;
        public UserController(IUserDetailsRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }


        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadUserDto>>> Get()
        {
            var userList = await repo.GetUsers();
            if (userList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<IEnumerable<ReadUserDto>>(userList));
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<ReadUserDto>> GetUser(int id)
        {
            var user = await repo.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ReadUserDto>(user));
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<bool>> Post([FromBody] CreateUserDto u)
        {
            var result = await GetUser(u.UserId);
            if(result == null)
            {
                UserDetails user = mapper.Map<UserDetails>(u);
                bool result1 = await repo.InsertUser(user);

                if (!result1)
                {
                    return BadRequest("The object not created");
                }

                var prod = mapper.Map<ReadUserDto>(user);
                return CreatedAtRoute(nameof(GetUser), new { id = user.UserId }, prod);
            }
            else
            {
                return BadRequest("The object already exists");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateUser(int id, [FromBody] UpdateUserDto u)
        {
            var user = await repo.GetUserById(id);
            if (user != null)
            {
                mapper.Map(u, user);
                var result1 = await repo.UpdateUserDetails(user);
                var result2 = await repo.UpdateUserProfile(user.UserId,u.Role,u.Availability);
                if (result1 && result2)
                {
                    return Ok("Update Successful");
                }
                else
                {
                    return BadRequest("Error in Updation");
                }

            }
            else
            {
                return NotFound("User not found");
            }
        }

        [HttpPut("Approval/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(int id)
        {
            var user = await repo.GetUserById(id);
            if (user != null)
            {
                var result1 = await repo.UpdateApproval(user);
                if (result1)
                {
                    return Ok("Update Successful");
                }
                else
                {
                    return BadRequest("Error in Updation");
                }

            }
            else
            {
                return NotFound("User not found");
            }
        }

        [HttpPut("donation/{id}")]
        public async Task<ActionResult<bool>> UpdateDonation(int id, [FromBody] UpdateUserDto ob)
        {
            var user = await repo.GetUserById(id);
            if (user != null)
            {
                var result1 = await repo.UpdateDonation(user,ob.LastDonated);
                if (result1)
                {
                    return Ok("Update Successful");
                }
                else
                {
                    return BadRequest("Error in Updation");
                }

            }
            else
            {
                return NotFound("User not found");
            }
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var result = await repo.DeleteUser(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

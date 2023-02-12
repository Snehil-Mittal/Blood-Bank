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
            UserDetails user = mapper.Map<UserDetails>(u);
            bool result = await repo.InsertUser(user);

            if (!result)
            {
                return BadRequest("The object not created");
            }

            var prod = mapper.Map<ReadUserDto>(user);
            return CreatedAtRoute(nameof(GetUser), new { id = user.UserId }, prod);
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
                var result2 = await repo.UpdateUserProfile(user.UserId, u.Role,u.Availability);
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

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoadAPI.Entities.User;
using RoadAPI.Helpers;
using RoadAPI.Models.Users;
using RoadAPI.Services.Interfaces;

namespace RoadAPI.Controllers
{
    [Authorize(AuthenticationSchemes="Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            
            return Ok(new
            {
                user.Id,
                user.MobileNumber,
                user.Username,
                user.FirstName,
                user.LastName,
                Token = tokenString
            });
        }
        
        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register([FromBody]RegisterModel model)
        {
            var user = _mapper.Map<User>(model);

            try
            {
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet(Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            var model = _mapper.Map<IList<UserModel>>(users);
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public IActionResult Get(int id)
        {
            if (id == 0)
            {
                var senderId = int.Parse(HttpContext.User.Claims
                    .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                    ?.Value!);
                var selfUser = _userService.GetById(senderId);
                var selfModel = _mapper.Map<DetailUserModel>(selfUser);
                return new ObjectResult(selfModel);
            }
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<UserModel>(user);
            return new ObjectResult(model);
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateModel model)
        {
            var callingUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value;
            if (callingUserId != id.ToString())
            {
                return BadRequest(new { message = "Do not have permission" });
            }
            var user = _mapper.Map<User>(model);
            user.Id = id;
            
            try
            {
                // update user 
                _userService.Update(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedUser = _userService.Delete(id);

            if (deletedUser == null)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
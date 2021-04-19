using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoadAPI.Entities;
using RoadAPI.Entities.Enums;
using RoadAPI.Helpers;
using RoadAPI.Models.Events;
using RoadAPI.Services.Interfaces;

namespace RoadAPI.Controllers
{  
    [Authorize(AuthenticationSchemes="Bearer")]
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventsController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllEvents")]
        public IActionResult GetAll(int limit = 100)
        {
            var events = _eventService.GetAll(limit);
            var model = _mapper.Map<IList<EventModel>>(events);
            return Ok(model);
        }

        [HttpGet("{id}", Name = "GetEventById")]
        public IActionResult GetEventById(int id)
        {
            var @event = _eventService.GetEventById(id);
            if (@event == null)
            {
                return NotFound();
            }
            var model = _mapper.Map<EventModel>(@event);
            return new ObjectResult(model);
        }
        
        [AllowAnonymous]
        [HttpGet("{latitude}_{longitude}/{radius}", Name = "GetInRadius")]
        public IActionResult GetInRadius(double latitude, double longitude, double radius, [FromQuery] int eventTypes = 1023)
        {
            var events = _eventService.GetInRadius(latitude, longitude, radius, (EventType)eventTypes);
            var model = _mapper.Map<IList<EventModel>>(events);
            return Ok(model);
        }

        [HttpPost("Add")]
        public IActionResult CreateEvent([FromBody] CreateModel model)
        {
            if (!Enum.IsDefined(typeof(EventType), model.EventType))
            {
                return BadRequest(new { message = "Unknown event type" });
            }
            var authorId = int.Parse(HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value!);
            var @event = _mapper.Map<Event>(model);
            try
            {
                _eventService.Create(@event, authorId);
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
            var deletedEvent = _eventService.Delete(id);

            if (deletedEvent == null)
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EventUpdateModel model)
        {
            var callingUserId = HttpContext.User.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Name)
                ?.Value;
            var @event = _mapper.Map<Event>(model);
            @event.Id = id;

            try
            {
                // update user 
                _eventService.Update(@event);
                return Ok();
            }
            catch (AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
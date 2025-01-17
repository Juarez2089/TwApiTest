using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Twilio.TwiML.Messaging;
using TwilioApi.DTO;
using TwilioApi.Model;
using TwilioApi.Repository.Interfaces;

namespace TwilioApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SsmTwilioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ITwilioRepository _trepository;
        public SsmTwilioController(ITwilioRepository repository, IMapper mapper)
        {
            _trepository= repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messageDTO = new List<TwlMessageDTO>();

            try
            {
                var messages = await _trepository.GetMessagesBD();
                //returning a DTO to avoid some fields that are not necesary in the front end.
                messageDTO = _mapper.Map<List<TwlMessageDTO>>(messages);

                return Ok(messageDTO);
            }
            catch (Exception )
            {
                throw;
            }
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(TwlMessageDTO msg)
        {
            if (msg == null)
            {
                return BadRequest();
            }
            try
            {
                //Process mms, saving header and detail in db.
                var twlMessage = _mapper.Map<TwlMessage>(msg);
                var Twilo = await _trepository.ProcessSmm(twlMessage);
                return Ok(Twilo);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

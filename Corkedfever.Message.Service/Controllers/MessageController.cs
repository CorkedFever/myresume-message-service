using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Corkedfever.Message.Business;
using Corkedfever.Message.Data.Models;

namespace Corkedfever.Message.Service.Controllers
{
    [ApiController]
    [Route("messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(List<MessageModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_messageService.GetMessages());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("getAllMessagesByEmailAddress/{emailAddress}")]
        [ProducesResponseType(typeof(List<MessageModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAllMessagesByEmailAddress(string emailAddress)
        {
            try
            {
                _messageService.GetAllMessagesByEmailAddress(emailAddress);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [Route("submit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SubmitMessage([FromBody]MessageModel message)
        {
            try
            {
                _messageService.AddMessage(message);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

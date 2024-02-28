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
    public class MessageController
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet]
        [Route("getAll")]
        public IEnumerable<MessageModel> GetAll()
        {
            return _messageService.GetMessages();
        }
        [HttpGet]
        [Route("getAllMessagesByEmailAddress/{emailAddress}")]
        public IEnumerable<MessageModel> GetAllMessagesByEmailAddress(string emailAddress)
        {
            return _messageService.GetAllMessagesByEmailAddress(emailAddress); 
        }
        [HttpPost]
        [Route("submit")]
        public void SubmitMessage([FromBody]MessageModel message)
        {
            _messageService.AddMessage(message);
        }

    }
}

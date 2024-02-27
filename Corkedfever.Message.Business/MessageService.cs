using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Corkedfever.Message.Data;
using Corkedfever.Message.Data.Models;

namespace Corkedfever.Message.Business
{
    public interface IMessageService
    {
        void AddMessage(MessageModel message);
        void DeleteMessage(int id);
        void UpdateMessage(MessageModel message, int id);
        MessageModel GetMessage(int id);
        List<MessageModel> GetMessages();
        List<MessageModel> GetAllMessagesByEmailAddress(string emailAddress);
    }
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public void AddMessage(MessageModel message)
        {
            _messageRepository.AddMessage(message);
        }
        public void DeleteMessage(int id)
        {
            _messageRepository.DeleteMessage(id);
        }
        public void UpdateMessage(MessageModel message, int id)
        {
            _messageRepository.UpdateMessage(message, id);
        }
        public MessageModel GetMessage(int id)
        {
            return _messageRepository.GetMessage(id);
        }
        public List<MessageModel> GetMessages()
        {
            return _messageRepository.GetMessages();
        }

        public List<MessageModel> GetAllMessagesByEmailAddress(string emailAddress)
        {
            return _messageRepository.GetAllMessagesByEmail(emailAddress);
        }
    }
}

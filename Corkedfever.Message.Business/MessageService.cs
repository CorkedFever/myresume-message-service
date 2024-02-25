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
        IEnumerable<MessageModel> GetMessages();
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
        public IEnumerable<MessageModel> GetMessages()
        {
            return _messageRepository.GetMessages();
        }
    }
}

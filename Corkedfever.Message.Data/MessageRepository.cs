using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Corkedfever.Message.Data.Models;
using Corkedfever.Message.Data.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace Corkedfever.Message.Data
{
    public interface IMessageRepository
    {
        void AddMessage(MessageModel message);
        void DeleteMessage(int id);
        void UpdateMessage(MessageModel message, int id);
        MessageModel GetMessage(int id);
        IEnumerable<MessageModel> GetMessages();
    }
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory<CorkedFeverDataContext> _context;
        public MessageRepository(IDbContextFactory<CorkedFeverDataContext> context)
        {
            _context = context;
        }
        public void AddMessage(MessageModel message)
        {
            using (var context = _context.CreateDbContext())
            {
                var dbEmail = new Emails
                {
                    EmailAddress = message.EmailAddress,
                    FirstName = message.FirstName,
                    LastName = message.LastName,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                var dbmessage = new Messages
                {
                    Email = dbEmail,
                    Title = message.Title,
                    Message = message.Message,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                context.Emails.Add(dbEmail); ;
                context.Messages.Add(dbmessage);
                context.SaveChanges();
            }

        }
        public void DeleteMessage(int id)
        {
            using (var context = _context.CreateDbContext())
            {
                 var message = context.Messages.Find(id);
                context.Messages.Remove(message);
                context.SaveChanges();
            }

        }
        public void UpdateMessage(MessageModel message, int id)
        {
            using (var context = _context.CreateDbContext())
            {
                var dbmessage = context.Messages.Where(m => m.Id == id).FirstOrDefault();
                dbmessage.Email.EmailAddress = message.EmailAddress;
                dbmessage.Title = message.Title;
                dbmessage.Email.FirstName = message.FirstName;
                dbmessage.Email.LastName = message.LastName;
                dbmessage.Email.UpdatedDate = DateTime.Now;
                context.Messages.Update(dbmessage);
                context.SaveChanges();

            }
        }
        public MessageModel GetMessage(int id)
        {
            using (var context = _context.CreateDbContext())
            {
                var message = context.Messages.Where(m => m.Id == id).FirstOrDefault();
                return new MessageModel
                {
                    EmailAddress = message.Email.EmailAddress,
                    FirstName = message.Email.FirstName,
                    LastName = message.Email.LastName,
                    Title = message.Title,
                    Message = message.Message
                };
            }
        }
        public IEnumerable<MessageModel> GetMessages()
        {
            using (var context = _context.CreateDbContext())
            {
                var messages = context.Messages.ToList();
                var messageModels = new List<MessageModel>();
                foreach (var message in messages)
                {
                    messageModels.Add(new MessageModel
                    {
                        EmailAddress = message.Email.EmailAddress,
                        FirstName = message.Email.FirstName,
                        LastName = message.Email.LastName,
                        Title = message.Title,
                        Message = message.Message
                    });
                }
                return messageModels;
            }
        }
    }
}

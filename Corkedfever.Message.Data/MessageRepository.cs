using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Corkedfever.Message.Data.Models;
using Microsoft.EntityFrameworkCore;
using Corkedfever.Common.Data;
using Corkedfever.Common.Data.DBModels;

namespace Corkedfever.Message.Data
{
    public interface IMessageRepository
    {
        void AddMessage(MessageModel message);
        List<MessageModel> GetMessages();
        List<MessageModel> GetAllMessagesByEmail(string emailAddress);
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
                var tempEmail = context.Email.Where(e=>e.EmailAddress == message.EmailAddress).FirstOrDefault();

                if (tempEmail != null)
                {
                       tempEmail.UpdatedDate = DateTime.Now;
                        context.Update(tempEmail);

                }
                else
                {
                    tempEmail = new Emails
                    {
                        EmailAddress = message.EmailAddress,
                        FirstName = message.FirstName,
                        LastName = message.LastName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now
                    };
                    context.Email.Add(tempEmail);
                }
                var dbmessage = new Messages
                {
                    Email = tempEmail,
                    Title = message.Title,
                    Message = message.Message,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                context.Message.Add(dbmessage);
                context.SaveChanges();
            }

        }
        public List<MessageModel> GetMessages()
        {
            using (var context = _context.CreateDbContext())
            {
                var messages = context.Message.Include(m=>m.Email).OrderBy(o=>o.CreatedDate).Take(10).ToList();
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

        public List<MessageModel> GetAllMessagesByEmail(string emailAddress)
        {
            using (var context = _context.CreateDbContext())
            {
                var messageModels = context.Message.Where(m => m.Email.EmailAddress == emailAddress).
                    Select(message => new MessageModel
                    {
                        EmailAddress = message.Email.EmailAddress,
                        FirstName = message.Email.FirstName,
                        LastName = message.Email.LastName,
                        Title = message.Title,
                        Message = message.Message
                    }).ToList();

                return messageModels;
            }
        }
    }
}

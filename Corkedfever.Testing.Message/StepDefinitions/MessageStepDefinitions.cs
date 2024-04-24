using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Corkedfever.Client.Message;
using Corkedfever.Client;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Corkedfever.Testing.Message.StepDefinitions
{
    [Binding]
    public sealed class MessageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        public MessageStepDefinitions(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }
        [Given(@"I have a message '([^']*)'")]
        public void GivenIHaveAMessage(string message)
        {
            //parse the json message and store it into the message model
            MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(message);

            //add the message to the scenario context
            _scenarioContext.Add("message", messageModel);

        }

        [When(@"I submit the message")]
        public void WhenISubmitTheMessage()
        {
            //get the message from the scenario context
            MessageModel messageModel = _scenarioContext.Get<MessageModel>("message");

            //create a new message client
            MessageClient messageClient = new MessageClient("http://localhost:26010");

            //submit the message
            messageClient.SubmitAsync(messageModel);
        }

        [Then(@"the message is in the database")]
        public void ThenTheMessageIsInTheDatabase()
        {
            //get the message from the scenario context
            MessageModel messageModel = _scenarioContext.Get<MessageModel>("message");

            //create a new message client
            MessageClient messageClient = new MessageClient("http://localhost:26010");

            //get the message from the database
            List<MessageModel> messagesFromDatabase = messageClient.GetAllMessagesByEmailAddressAsync(messageModel.EmailAddress).Result.ToList();

            //check if the message is in the database
            Assert.IsNotNull(messagesFromDatabase);
            Assert.IsTrue(messagesFromDatabase.Contains(messageModel));
        }

        [Given(@"I have another message '([^']*)'")]
        public void GivenIHaveAnotherMessage(string message)
        {
            MessageModel messageModel = JsonConvert.DeserializeObject<MessageModel>(message);

            //add the message to the scenario context
            _scenarioContext.Add("message2", messageModel);
        }

        [When(@"I submit all the messages")]
        public void WhenISubmitAllTheMessages()
        {            
            //get the message from the scenario context
            MessageModel messageModel = _scenarioContext.Get<MessageModel>("message");
            MessageModel messageModel2 = _scenarioContext.Get<MessageModel>("message2");
            //create a new message client
            MessageClient messageClient = new MessageClient("http://localhost:26010");

            messageClient.SubmitAsync(messageModel);
            messageClient.SubmitAsync(messageModel2);
        }

        [Then(@"Then I can retrieve all the messages for the email")]
        public void ThenThenICanRetrieveAllTheMessagesForTheEmail()
        {
            //get the message from the scenario context
            MessageModel messageModel = _scenarioContext.Get<MessageModel>("message");
            MessageModel messageModel2 = _scenarioContext.Get<MessageModel>("message2");

            //create a new message client
            MessageClient messageClient = new MessageClient("http://localhost:26010");

            //get the message from the database
            List<MessageModel> messagesFromDatabase = messageClient.GetAllMessagesByEmailAddressAsync(messageModel.EmailAddress).Result.ToList();

            //check if the message is in the database
            Assert.IsNotNull(messagesFromDatabase);
            Assert.IsTrue(messagesFromDatabase.Contains(messageModel));
            Assert.IsTrue(messagesFromDatabase.Contains(messageModel2));
        }


    }
}

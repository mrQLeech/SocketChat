using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketClientController;
using SocketMessageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketClientController.Tests
{
    [TestClass()]
    public class SocketClientProcessorTests
    {

        [TestMethod()]
        public void SocketClientProcessorTest()
        {
            var client = new SocketClientProcessor();

            Assert.IsNotNull(client);
        }

        [TestMethod()]
        public void GetClientIdTest()
        {
            var client = new SocketClientProcessor();
            Assert.IsTrue(!string.IsNullOrEmpty(client.GetClientId()));
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            MessageModel mess = new MessageModel("test", MessageType.MESSAGE) ;
            var client = new SocketClientProcessor();

            client.MessageRecieved += (s, e) => {
                Assert.IsInstanceOfType(s, typeof( SocketClientProcessor));
                Assert.IsInstanceOfType(e, typeof(RecievedMessageEventArgs));
                var m = ((RecievedMessageEventArgs)e).Message;
                Assert.AreEqual(m.Text, mess.Text);
            };
            client.SendMessage("test");
        }

        [TestMethod()]
        public void GetMessagesLogTest()
        {
            MessageModel mess = new MessageModel("test", MessageType.MESSAGE);
            var client = new SocketClientProcessor();

            client.MessageRecieved += (s, e) => {
                var m = ((RecievedMessageEventArgs)e).Message;
                Assert.IsTrue(!string.IsNullOrEmpty(m.Text.Trim()));
            };
            client.SendMessage("test");
        }

        [TestMethod()]
        public void CloseConnectionTest()
        {
            Assert.Fail();
        }
    }
}
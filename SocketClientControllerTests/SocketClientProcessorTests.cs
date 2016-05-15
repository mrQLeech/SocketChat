using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocketCommon;

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
            var client = new SocketClientProcessor();

            MessageModel mess = new MessageModel(MessageType.MESSAGE, "test", "test");
            client.MessageRecieved += (s, e) => {
                Assert.IsInstanceOfType(s, typeof( SocketClientProcessor));
                Assert.IsInstanceOfType(e, typeof(RecievedMessageEventArgs));
                var m = ((RecievedMessageEventArgs)e).Message;
                Assert.AreEqual(m.Text, mess.Text);
            };
            client.SendMessage("test");
        }

        [TestMethod()]
        public void RequestMessageLog()
        {           
            //    MessageModel mess = new MessageModel( MessageType.MESSAGE, "test", "test");
            //    var client = new SocketClientProcessor();

            //    client.MessageRecieved += (s, e) => {
            //        var m = ((RecievedMessageEventArgs)e).Message;
            //        Assert.IsTrue(!string.IsNullOrEmpty(m.Text.Trim()));
            //    };
            //    client.SendMessage("test");            
        }


        [TestMethod()]
        public void CloseConnectionTest()
        {
            //Assert.Fail();
        }
    }
}
using System;
using SocketClientController;
using System.Windows.Forms;
using SocketCommon;

namespace SocketClientView
{
    public partial class ChatForm : Form
    {
        private ISocketProcessor connectionProcessor;

        public ChatForm(ISocketProcessor processor)
        {
            InitializeComponent();
            connectionProcessor = processor;

            connectionProcessor.MessageRecieved += OnMessageRecieved;

            connectionProcessor.CreateConnection();
        }


        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private void messageText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }


        private void SendMessage()
        {
            if (!string.IsNullOrEmpty(messageText.Text) 
                 && !string.IsNullOrWhiteSpace(messageText.Text))
            {
                connectionProcessor.SendMessage(messageText.Text);
                messageText.Text = "";
            }            
        }


        private void OnMessageRecieved(Object sender, EventArgs arg)
        {
            var mess = ((RecievedMessageEventArgs)arg).Message;
            if (mess == null) return;

            if (mess.Type == SocketCommon.MessageType.MESSAGE)
            {
                //thread's check
                if (chatText.InvokeRequired) 
                    chatText.Invoke(new Action(() =>
                    {
                        chatText.AppendText(mess.SenderName + ": " + mess.Text + "\r\n");
                    }));
                else 
                {
                    chatText.AppendText(mess.SenderName + ": " + mess.Text + "\r\n");
                }
                
            }


            if (mess.Type == SocketCommon.MessageType.LOG_DATA)
            {
                //thread's check
                if (chatText.InvokeRequired)
                    chatText.Invoke(new Action(() =>
                    {
                        chatText.AppendText("\r\n");
                        chatText.AppendText("*************** LOG ***************" + "\r\n");
                        chatText.AppendText(mess.Text + "\r\n");
                        chatText.AppendText("************* LOG_END *************" + "\r\n");
                        chatText.AppendText("\r\n");
                    }));
                else
                {
                    chatText.AppendText(mess.SenderName + ": " + mess.Text + "\r\n");
                }

            }

        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                connectionProcessor.MessageRecieved -= OnMessageRecieved;
                connectionProcessor.CloseConnection();
                connectionProcessor = null;

            }
            finally { }

        }


        private void logRequestButton_Click(object sender, EventArgs e)
        {
            connectionProcessor.RequestMessageLog();
        }
    }
}

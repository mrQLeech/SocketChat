using System;
using SocketClientController;
using System.Windows.Forms;
using SocketCommon;
using System.Drawing;

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
                var user = mess.SenderName + ": ";
                var text = mess.Text + "\r\n";
                //thread's check
                if (chatText.InvokeRequired) 
                    chatText.Invoke(new Action(() =>
                    {
                        
                        AppendChatColorText(user, Color.Blue);
                        AppendChatColorText(text, Color.Black);
                        
                    }));
                else 
                {
                    AppendChatColorText(user, Color.Blue);
                    AppendChatColorText(text, Color.Black);
                }
                
            }


            if (mess.Type == SocketCommon.MessageType.LOG_DATA)
            {
                //thread's check
                if (chatText.InvokeRequired)
                    chatText.Invoke(new Action(() =>
                    {
                        chatText.AppendText("\r\n");
                        AppendChatColorText("*************** LOG ***************" + "\r\n", Color.Green);
                        AppendChatColorText(mess.Text + "\r\n", Color.Green);
                        AppendChatColorText("************* LOG_END *************" + "\r\n", Color.Green);
                        chatText.AppendText("\r\n");
                    }));
                else
                {
                    chatText.AppendText(mess.SenderName + ": " + mess.Text + "\r\n");
                }

            }

        }


        private void AppendChatColorText (string text, Color color)
        {
            chatText.SelectionStart = chatText.TextLength;
            chatText.SelectionLength = 0;

            chatText.SelectionColor = color;
            chatText.SelectionFont = chatText.Font;
            chatText.AppendText(text);
            chatText.SelectionColor = chatText.ForeColor;
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

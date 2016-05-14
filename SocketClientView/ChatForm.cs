using System;
using SocketClientController;
using System.Windows.Forms;

namespace SocketClientView
{
    public partial class ChatForm : Form
    {
        private IClientProcessor connectionProcessor;

        public ChatForm(IClientProcessor processor)
        {
            InitializeComponent();
            connectionProcessor = processor;
            this.Text = processor.
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
            }            
        }

        
    }
}

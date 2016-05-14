using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketMessageModel
{
    [Serializable]
    public class MessageModel
    {
        public string Text { get; set; }
        public MessageType Type { get; set; }

        public MessageModel() { }

        public MessageModel(string text, MessageType type)
        {
            this.Text = text;
            this.Type = type;
        }

        public bool IsValid()
        {

            if ((Type == MessageType.MESSAGE || Type == MessageType.CONNECTION) 
                && string.IsNullOrEmpty(Text.Trim()))
            {
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketCommon
{
    [Serializable]
    public class MessageModel
    {
        public string Text { get; set; }

        
        public string SenderName { get; set; }


        public MessageType Type { get; set; }


        public MessageModel() { }


        public MessageModel(MessageType type, string name,  string text)
        {
            this.Type = type;
            this.SenderName = name;
            this.Text = text;
        }


        public bool IsValid()
        {
            if ((Type == MessageType.MESSAGE ) 
                && (string.IsNullOrEmpty(Text.Trim()) || string.IsNullOrEmpty(SenderName.Trim())))
            {
                return false;
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.Models.ViewModels
{
    public class EchoMessage
    {
        private readonly string[] MessageLines;

        public EchoMessage() : this(string.Empty) { }
        public EchoMessage(string message)
        {
            //this.Message = message;
            MessageLines = null;
            MessageLines = message.Split('\n');
        }

        //public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Messaggio come sottomesso
        /// </summary>
        public string Message
        {
            get
            {
                return string.Join("\n", MessageLines);
            }
        }

        public string Line(int index)
        {
            return MessageLines[index];
        }

        public int CountLines
        {
            get
            {
                if (MessageLines == null) { return 0; }
                else { return MessageLines.Length; }
            }
        }


    }

}
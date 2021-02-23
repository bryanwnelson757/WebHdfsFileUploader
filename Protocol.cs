using System;
using System.Collections.Generic;
using System.Text;

namespace WebHdfsFileUploader
{
    public class Protocol
    {
        public string Value { get; set; }

        private Protocol(string value) { Value = value; }

        public static Protocol Http {  get { return new Protocol("http"); } }

        public static Protocol Https { get { return new Protocol("https"); } }
    }
}

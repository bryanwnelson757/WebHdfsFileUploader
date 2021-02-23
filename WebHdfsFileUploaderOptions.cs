using System;
using System.Collections.Generic;
using System.Text;

namespace WebHdfsFileUploader
{
    public class WebHdfsFileUploaderOptions
    {
        public Protocol Protocol { get; set; } = Protocol.Http;
        public string Host { get; set; }
        public int Port { get; set; } = 9870;
        public string Path { get; set; }
        public string UserName { get; set; }
        public bool Overwrite { get; set; } = true;
        public int Permission { get; set; }
    }
}

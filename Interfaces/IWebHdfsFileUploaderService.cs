using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebHdfsFileUploader.Interfaces
{
    public interface IWebHdfsFileUploaderService
    {
        Task<HttpResponseMessage> UploadFile(string ntfsPath, WebHdfsFileUploaderOptions options);
    }
}

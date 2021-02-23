using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WebHdfsFileUploader.Interfaces;

namespace WebHdfsFileUploader
{
    public class WebHdfsFileUploaderService : IWebHdfsFileUploaderService
    {
        private static readonly HttpClient _client = new HttpClient();
        private WebHdfsFileUploaderOptions _options;
        private string _overwrite;
        private string baseUrl = "{0}://{1}:{2}/webhdfs/v1/{3}?op=CREATE&overwrite={4}&user.name={5}"; 

        public async Task<HttpResponseMessage> UploadFile(string ntfsPath, WebHdfsFileUploaderOptions options) 
        {
            try
            {
                _options = options;
                _overwrite = options.Overwrite.ToString().ToLower();

                baseUrl = String.Format(baseUrl, _options.Protocol.Value, _options.Host, _options.Port, _options.Path, _overwrite, _options.UserName); 

                if(_options.Permission != 0)
                {
                    baseUrl = $"{baseUrl}&permission={_options.Permission}"; 
                }

                var location = await GetLocation();

                var content = new ByteArrayContent(File.ReadAllBytes(ntfsPath)); 

                return await SaveFile(location.ToString(), content);
            }
            catch(Exception ex)
            {
                throw new Exception("File upload to HDFS failed.", ex); 
            }
        }

        private async Task<HttpResponseMessage> SaveFile(string location, HttpContent content)
        {
            try
            {
                return await GetPutResponse($"{baseUrl}&namenoderpcaddress={location}", content); 
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

        private async Task<string> GetLocation()
        {
            try
            {
                var response = await GetPutResponse($"{baseUrl}&noredirect=false");

                return response.Headers.Location.Authority;
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }

        private async Task<HttpResponseMessage> GetPutResponse(string url, HttpContent content = null)
        {
            try
            {
                using (var request = new HttpRequestMessage(HttpMethod.Put, url))
                {
                    if(content != null)
                    {
                        request.Content = content;
                    }

                    return await _client.SendAsync(request);
                }
            }
            catch(Exception ex)
            {
                throw ex; 
            }
        }
    }
}

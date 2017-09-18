using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using SWC.Tools.Common.Networking.Json.Messages;

namespace SWC.Tools.Common.Networking
{
    internal class MessageSender
    {
        private readonly string _url;
        private readonly DataContractJsonSerializerSettings _settings;

        public MessageSender(string url)
        {
            _url = url;
            _settings = new DataContractJsonSerializerSettings {EmitTypeInformation = EmitTypeInformation.Never, UseSimpleDictionaryFormat = true};
        }

        public string Send(Message message)
        {
            var serializer = new DataContractJsonSerializer(message.GetType(), _settings);

            string batch;
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, message);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    batch = reader.ReadToEnd();
                }
            }

            var result = SendInternal(batch);
            return result.Result;
        }

        public async Task<string> SendInternal(string batch)
        {
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    { "batch", batch }
                };

                var content = new FormUrlEncodedContent(values);

                var request = new HttpRequestMessage() {
                    RequestUri = new Uri(_url),
                    Method = HttpMethod.Post,
                    Content = content
                };
                request.Headers.Authorization = new AuthenticationHeaderValue("GAE", "54690BA3-45EF-4CEF-9A75-F30314596815");

                //var response = await client.PostAsync(_url, content);
                var response = await client.SendAsync(request);

                return await response.Content.ReadAsStringAsync();
            }
        }
        private Dictionary<string, string> GetHeaders()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["DModel"] = "20354 (LENOVO)";
            dictionary["DType"] = "Desktop";
            dictionary["DOS"] = "Windows 8.1 (6.3.9600)";
            dictionary["DMemory"] = "1040 MB";
            dictionary["DProcessors"] = "4";
            dictionary["DProcessorType"] = "Intel(R) Core(TM) i7-4510U CPU @ 2.00GHz";
            dictionary["DGName"] = "Intel(R) HD Graphics Family";
            dictionary["DGVendor"] = "Intel";
            dictionary["DGVersion"] = "Direct3D 11.0 [level 11.0]";
            dictionary["DGMemory"] = "0 MB";
            dictionary["DGShaderLevel"] = "50";
            dictionary["Authorization"] = "FD 0740BD33-E5E9-4CB3-8D46-97D2AFC306E0:18AB7595FC6717FEE9A81431BC66476B9B5E06AAB9BE83EB";
            dictionary["CAppVersion"] = "4.7.0.2";

            return dictionary;
        }
    }
}
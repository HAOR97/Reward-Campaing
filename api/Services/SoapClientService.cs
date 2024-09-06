using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using api.Interfaces;
using api.Utils;

namespace api.Services
{
    public class SoapClientService : ISoapClientService
    {
        private readonly HttpClient _client;


        public SoapClientService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string?> FindPersonAsync(int id)
        {

            string requestUrl = $"https://www.crcind.com/csp/samples/SOAP.Demo.cls?soap_method=FindPerson&id={id}";

            var response = await _client.GetAsync(requestUrl);
            if (response.IsSuccessStatusCode)
            {
                var soapResponse = await response.Content.ReadAsStringAsync();
                return soapResponse;
            }

            return null;
        }
    }
}
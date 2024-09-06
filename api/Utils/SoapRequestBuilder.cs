using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Utils
{
    public class SoapRequestBuilder
    {
        public static string BuildFindPersonRequest(int id)
        {
            return $@"
                <SOAP-ENV:Envelope xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/'>
                    <SOAP-ENV:Body>
                        <FindPerson xmlns='http://tempuri.org'>
                            <id>{id}</id>
                        </FindPerson>
                    </SOAP-ENV:Body>
                </SOAP-ENV:Envelope>";
        }
    }
}
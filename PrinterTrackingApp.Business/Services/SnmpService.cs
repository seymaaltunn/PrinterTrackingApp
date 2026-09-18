using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Business.Services
{
    public class SnmpService
    {
        public string GetPrinterDescription(string ipAddress)
        {
            try
            {
                var endpoint = new IPEndPoint(IPAddress.Parse(ipAddress), 161);

                var variables = Messenger.Get(
                    VersionCode.V2,
                    endpoint,
                    new OctetString("public"),
                    new List<Variable>
                    {
                        new Variable(
                            new ObjectIdentifier("1.3.6.1.2.1.1.1.0")
                        )
                    },
                    3000
                );

                return variables[0].Data.ToString();
            }
            catch (Exception ex)
            {
                return $"SNMP Hatası: {ex.Message}";
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Core.Entities
{
    public class TonerOlcumu
    {
        public int Id { get; set; }

        public int YaziciId { get; set; }

        public string Renk { get; set; } = string.Empty;

        public int? SeviyeYuzde { get; set; }

        public DateTime OlcumZamani { get; set; }

        public bool OlcumBasariliMi { get; set; }

        public string? HataMesaji { get; set; }

        public string VeriKaynagi { get; set; } = "SNMP";
    }
}
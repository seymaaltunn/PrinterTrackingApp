using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Core.Entities
{
    public class YaziciSayacOlcumu
    {
        public int Id { get; set; }

        public int YaziciId { get; set; }

        public int? ToplamSayac { get; set; }

        public int? GunlukBaski { get; set; }

        public DateTime OlcumTarihi { get; set; }

        public DateTime OlcumZamani { get; set; }

        public bool OlcumBasariliMi { get; set; }

        public string? HataMesaji { get; set; }

        public string VeriKaynagi { get; set; } = "SNMP";
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterTrackingApp.Core.Entities
{
    public class Yazici
    {
        public int Id { get; set; }

        public string IpAdresi { get; set; } = string.Empty;

        public string Marka { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        public bool RenkliMi { get; set; }

        public string? BaskiTeknolojisi { get; set; }

        public string Konum { get; set; } = string.Empty;

        public bool FotokopiVarMi { get; set; }

        public string? GorselYolu { get; set; }

        public DateTime? OlusturmaTarihi { get; set; }

        public bool AktifMi { get; set; } = true;

        public bool SnmpAktifMi { get; set; } = true;

        public string SnmpCommunity { get; set; } = "public";

        public string SnmpVersion { get; set; } = "2c";

        public DateTime? SonBasariliIletisim { get; set; }
    }
}
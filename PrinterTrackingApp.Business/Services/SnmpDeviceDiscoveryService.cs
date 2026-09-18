using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System.Net;

namespace PrinterTrackingApp.Business.Services
{
    public sealed class SnmpDeviceInfo
    {
        public bool BasariliMi { get; set; }
        public string Marka { get; set; } = "Bilinmiyor";
        public string Model { get; set; } = "Bilinmiyor";
        public string? Aciklama { get; set; }
        public string Profil { get; set; } = "Standart Printer-MIB";
        public string? Hata { get; set; }
    }

    public class SnmpDeviceDiscoveryService
    {
        public SnmpDeviceInfo Tani(string ipAdresi, string community = "public", string version = "2c")
        {
            var info = new SnmpDeviceInfo();
            try
            {
                if (!IPAddress.TryParse(ipAdresi, out var ip)) throw new Exception("Geçersiz IP adresi.");
                var vars = Messenger.Get(
                    version.Trim() == "1" ? VersionCode.V1 : VersionCode.V2,
                    new IPEndPoint(ip, 161),
                    new OctetString(string.IsNullOrWhiteSpace(community) ? "public" : community.Trim()),
                    new List<Variable> { new(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")) }, 3000);

                string desc = vars.FirstOrDefault()?.Data?.ToString()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(desc)) throw new Exception("sysDescr okunamadı.");
                info.Aciklama = desc;
                (info.Marka, info.Model) = MarkaModelAyir(desc);
                info.Profil = ProfilSec(info.Marka, desc);
                info.BasariliMi = true;
            }
            catch (Exception ex) { info.Hata = ex.Message; }
            return info;
        }

        private static (string marka, string model) MarkaModelAyir(string desc)
        {
            string u = desc.ToUpperInvariant();
            string[] markalar = { "KYOCERA", "SHARP", "HP", "HEWLETT-PACKARD", "CANON", "LEXMARK", "XEROX", "RICOH", "BROTHER", "EPSON", "KONICA MINOLTA", "SAMSUNG" };
            string marka = markalar.FirstOrDefault(u.Contains) ?? "Bilinmiyor";
            if (marka == "HEWLETT-PACKARD") marka = "HP";
            string model = desc;
            int idx = u.IndexOf(marka, StringComparison.OrdinalIgnoreCase);
            if (idx >= 0) model = desc[(idx + marka.Length)..].Trim(' ', '-', ':', ',');
            if (model.Length > 80) model = model[..80];
            if (string.IsNullOrWhiteSpace(model)) model = "Bilinmiyor";
            return (marka, model);
        }

        private static string ProfilSec(string marka, string desc) => marka.ToUpperInvariant() switch
        {
            "KYOCERA" => "Kyocera + Printer-MIB fallback",
            "SHARP" => "Sharp + Printer-MIB",
            "HP" => "HP + Printer-MIB fallback",
            "CANON" => "Canon + Printer-MIB fallback",
            _ => "Standart Printer-MIB"
        };
    }
}

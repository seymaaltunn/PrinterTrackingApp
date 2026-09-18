using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Interfaces;
using System.Globalization;
using System.Net;
using System.Net.Sockets;

namespace PrinterTrackingApp.Business.Services
{
    public class SnmpPrinterMonitor : IPrinterMonitor
    {
        private const int Port = 161;
        private const int TimeoutMs = 3000;

        private readonly string _community;
        private readonly VersionCode _version;

        public SnmpPrinterMonitor(string community = "public", string version = "2c")
        {
            _community = string.IsNullOrWhiteSpace(community) ? "public" : community.Trim();
            _version = version.Trim().Equals("1", StringComparison.OrdinalIgnoreCase)
                ? VersionCode.V1
                : VersionCode.V2;
        }

        public PrinterSnapshot Oku(string ipAdresi)
        {
            var snapshot = new PrinterSnapshot
            {
                IpAdresi = ipAdresi,
                OlcumZamani = DateTime.Now,
                VeriKaynagi = "SNMP"
            };

            if (!IPAddress.TryParse(ipAdresi, out var ip))
            {
                snapshot.BasariliMi = false;
                snapshot.Durum = "Geçersiz IP";
                snapshot.HataMesaji = "Geçerli bir IPv4 adresi girilmedi.";
                return snapshot;
            }

            var endpoint = new IPEndPoint(ip, Port);

            try
            {


                snapshot.CihazAciklamasi = OkuMetinZorunlu(endpoint, "1.3.6.1.2.1.1.1.0");

                snapshot.ToplamSayfa = ToplamSayaciOku(endpoint);
                SarfBilgileriniOku(endpoint, snapshot);

                bool olcumVerisiVar =
                    snapshot.ToplamSayfa.HasValue ||
                    snapshot.SiyahTonerYuzde.HasValue ||
                    snapshot.CyanTonerYuzde.HasValue ||
                    snapshot.MagentaTonerYuzde.HasValue ||
                    snapshot.SariTonerYuzde.HasValue ||
                    snapshot.DrumYuzde.HasValue;

                snapshot.BasariliMi = olcumVerisiVar;

                if (olcumVerisiVar)
                {
                    snapshot.Durum = "SNMP ölçümü başarılı";
                    snapshot.HataMesaji = null;
                }
                else
                {
                    snapshot.Durum = "OID profili gerekli";
                    snapshot.HataMesaji =
                        $"SNMP erişimi başarılı ancak standart Printer-MIB sayaç/sarf OID'lerinden veri alınamadı. " +
                        $"Cihaz: {snapshot.CihazAciklamasi ?? "Bilinmiyor"}. Marka/model için özel OID profili gerekebilir.";
                }
            }
            catch (Lextm.SharpSnmpLib.Messaging.TimeoutException)
            {
                snapshot.BasariliMi = false;
                snapshot.Durum = "SNMP zaman aşımı";
                snapshot.HataMesaji =
                    $"{ipAdresi}:161 adresinden SNMP yanıtı alınamadı. Aynı ağ/VPN erişimini, SNMP'nin açık olduğunu, " +
                    $"community değerini ('{_community}') ve güvenlik duvarında UDP 161 erişimini kontrol edin.";
            }
            catch (SocketException ex)
            {
                snapshot.BasariliMi = false;
                snapshot.Durum = "Ağ hatası";
                snapshot.HataMesaji = $"SNMP ağ bağlantısı kurulamadı: {ex.Message}";
            }
            catch (Exception ex)
            {
                snapshot.BasariliMi = false;
                snapshot.Durum = "SNMP hatası";
                snapshot.HataMesaji = $"SNMP okuma hatası: {ex.Message}";
            }

            return snapshot;
        }

        private string? OkuMetinZorunlu(IPEndPoint endpoint, string oid)
        {
            var sonuc = Get(endpoint, oid);
            string? metin = sonuc?.Data?.ToString();
            return GecerliSnmpDegeriMi(metin) ? metin : null;
        }

        private string? OkuMetin(IPEndPoint endpoint, string oid)
        {
            try
            {
                var sonuc = Get(endpoint, oid);
                string? metin = sonuc?.Data?.ToString();
                return GecerliSnmpDegeriMi(metin) ? metin : null;
            }
            catch
            {
                return null;
            }
        }

        private int? OkuInt(IPEndPoint endpoint, string oid)
        {
            try
            {
                var sonuc = Get(endpoint, oid);
                string? metin = sonuc?.Data?.ToString();

                if (!GecerliSnmpDegeriMi(metin))
                    return null;

                return int.TryParse(metin, NumberStyles.Integer, CultureInfo.InvariantCulture, out int deger)
                    ? deger
                    : null;
            }
            catch
            {
                return null;
            }
        }

        private Variable? Get(IPEndPoint endpoint, string oid)
        {
            var variables = Messenger.Get(
                _version,
                endpoint,
                new OctetString(_community),
                new List<Variable> { new(new ObjectIdentifier(oid)) },
                TimeoutMs);

            return variables.Count > 0 ? variables[0] : null;
        }

        private int? ToplamSayaciOku(IPEndPoint endpoint)
        {

            for (int index = 1; index <= 16; index++)
            {
                int? deger = OkuInt(endpoint, $"1.3.6.1.2.1.43.10.2.1.4.1.{index}");
                if (deger.HasValue && deger.Value >= 0)
                    return deger;
            }

            return null;
        }

        private void SarfBilgileriniOku(IPEndPoint endpoint, PrinterSnapshot snapshot)
        {

            for (int index = 1; index <= 32; index++)
            {
                string? aciklama = OkuMetin(endpoint, $"1.3.6.1.2.1.43.11.1.1.6.1.{index}");
                if (string.IsNullOrWhiteSpace(aciklama))
                    continue;

                int? maksimum = OkuInt(endpoint, $"1.3.6.1.2.1.43.11.1.1.8.1.{index}");
                int? mevcut = OkuInt(endpoint, $"1.3.6.1.2.1.43.11.1.1.9.1.{index}");
                int? yuzde = YuzdeHesapla(mevcut, maksimum);

                if (!yuzde.HasValue)
                    continue;

                string normalized = Normalize(aciklama);

                if (DrumMu(normalized))
                {
                    snapshot.DrumYuzde ??= yuzde;
                    continue;
                }

                if (SiyahMi(normalized)) snapshot.SiyahTonerYuzde ??= yuzde;
                else if (CyanMi(normalized)) snapshot.CyanTonerYuzde ??= yuzde;
                else if (MagentaMi(normalized)) snapshot.MagentaTonerYuzde ??= yuzde;
                else if (SariMi(normalized)) snapshot.SariTonerYuzde ??= yuzde;
            }
        }

        private static int? YuzdeHesapla(int? mevcut, int? maksimum)
        {
            if (!mevcut.HasValue || !maksimum.HasValue || mevcut.Value < 0 || maksimum.Value <= 0)
                return null;

            double yuzde = (double)mevcut.Value / maksimum.Value * 100d;
            return Math.Clamp((int)Math.Round(yuzde), 0, 100);
        }

        private static bool GecerliSnmpDegeriMi(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            string v = value.Trim().ToLowerInvariant();
            return !v.Contains("nosuch") && !v.Contains("no such") && v != "null";
        }

        private static string Normalize(string value) => value.Trim().ToLowerInvariant()
            .Replace("ı", "i").Replace("ş", "s").Replace("ğ", "g")
            .Replace("ü", "u").Replace("ö", "o").Replace("ç", "c");

        private static bool SiyahMi(string s) => s.Contains("black") || s.Contains("siyah") || s == "k" || s.Contains(" toner k");
        private static bool CyanMi(string s) => s.Contains("cyan") || s.Contains("camgobegi") || s == "c";
        private static bool MagentaMi(string s) => s.Contains("magenta") || s.Contains("kirmizi") || s == "m";
        private static bool SariMi(string s) => s.Contains("yellow") || s.Contains("sari") || s == "y";
        private static bool DrumMu(string s) => s.Contains("drum") || s.Contains("imaging unit") || s.Contains("photoconductor") || s.Contains("image unit");
    }
}

using PrinterTrackingApp.Core.Entities;

namespace PrinterTrackingApp.Business.Services
{
    public sealed class TopluImportSonucu
    {
        public int Eklendi { get; set; }
        public int Atlandi { get; set; }
        public int Hatali { get; set; }
        public List<string> Mesajlar { get; } = new();
    }

    public class TopluYaziciImportService
    {
        public TopluImportSonucu CsvAktar(string dosyaYolu)
        {
            var sonuc = new TopluImportSonucu();
            var yaziciService = new YaziciService();
            var discovery = new SnmpDeviceDiscoveryService();
            var satirlar = File.ReadAllLines(dosyaYolu);
            if (satirlar.Length == 0) return sonuc;

            int baslangic = satirlar[0].ToLowerInvariant().Contains("ip") ? 1 : 0;
            for (int i = baslangic; i < satirlar.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(satirlar[i])) continue;
                try
                {
                    char ayirac = satirlar[i].Contains(';') ? ';' : ',';
                    var p = satirlar[i].Split(ayirac).Select(x => x.Trim().Trim('"')).ToArray();
                    string ip = p.ElementAtOrDefault(0) ?? "";
                    string konum = p.ElementAtOrDefault(1) ?? "Belirtilmedi";
                    string community = p.ElementAtOrDefault(2) ?? "public";
                    string version = p.ElementAtOrDefault(3) ?? "2c";

                    var tanim = discovery.Tani(ip, community, version);
                    var y = new Yazici
                    {
                        IpAdresi = ip,
                        Marka = tanim.BasariliMi ? tanim.Marka : "Bilinmiyor",
                        Model = tanim.BasariliMi ? tanim.Model : "Bilinmiyor",
                        Konum = string.IsNullOrWhiteSpace(konum) ? "Belirtilmedi" : konum,
                        BaskiTeknolojisi = "Lazer",
                        AktifMi = true,
                        SnmpAktifMi = true,
                        SnmpCommunity = string.IsNullOrWhiteSpace(community) ? "public" : community,
                        SnmpVersion = string.IsNullOrWhiteSpace(version) ? "2c" : version,
                        OlusturmaTarihi = DateTime.Now
                    };
                    yaziciService.YaziciEkle(y);
                    sonuc.Eklendi++;
                    sonuc.Mesajlar.Add($"{ip}: eklendi ({y.Marka} {y.Model})");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("zaten kayıtlı", StringComparison.OrdinalIgnoreCase)) sonuc.Atlandi++; else sonuc.Hatali++;
                    sonuc.Mesajlar.Add($"Satır {i + 1}: {ex.Message}");
                }
            }
            return sonuc;
        }
    }
}

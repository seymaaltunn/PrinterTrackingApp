using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Interfaces;

namespace PrinterTrackingApp.Business.Services
{
    public class TopluOlcumService
    {
        private readonly YaziciService _yaziciService = new();
        private readonly IslemLogService _logService = new();

        public TopluOlcumSonucu TumAktifYazicilariOlc(
            bool gercekSnmpKullan,
            FakePrinterMonitor fakeMonitor,
            Action<int, int, string>? ilerleme = null)
        {
            var sonuc = new TopluOlcumSonucu
            {
                BaslamaZamani = DateTime.Now
            };

            var yazicilar = _yaziciService.TumYazicilariGetir()
                .Where(x => x.AktifMi)
                .ToList();

            sonuc.ToplamYazici = yazicilar.Count;

            _logService.Kaydet(
                "TOPLU_OLCUM_BASLADI",
                $"Toplu ölçüm başladı. Kaynak: {(gercekSnmpKullan ? "SNMP" : "FAKE")}. Yazıcı sayısı: {yazicilar.Count}",
                true);

            for (int i = 0; i < yazicilar.Count; i++)
            {
                var yazici = yazicilar[i];
                ilerleme?.Invoke(i + 1, yazicilar.Count, yazici.IpAdresi);

                if (gercekSnmpKullan && !yazici.SnmpAktifMi)
                {
                    sonuc.Atlanan++;
                    continue;
                }

                try
                {
                    IPrinterMonitor monitor = gercekSnmpKullan
                        ? new SnmpPrinterMonitor(yazici.SnmpCommunity, yazici.SnmpVersion)
                        : fakeMonitor;

                    var olcum = new YaziciOlcumService(monitor).OlcumAl(yazici);

                    if (olcum.OlcumBasariliMi)
                    {
                        sonuc.Basarili++;
                    }
                    else
                    {
                        sonuc.Basarisiz++;
                        _logService.Kaydet(
                            "OLCUM_HATASI",
                            "Yazıcı ölçümü başarısız oldu.",
                            false,
                            yazici.Id,
                            yazici.IpAdresi,
                            olcum.HataMesaji);
                    }
                }
                catch (Exception ex)
                {
                    sonuc.Basarisiz++;
                    _logService.Kaydet(
                        "OLCUM_EXCEPTION",
                        "Yazıcı ölçümü sırasında beklenmeyen hata oluştu.",
                        false,
                        yazici.Id,
                        yazici.IpAdresi,
                        ex.Message);
                }
            }

            sonuc.BitisZamani = DateTime.Now;

            _logService.Kaydet(
                "TOPLU_OLCUM_BITTI",
                $"Toplu ölçüm tamamlandı. Başarılı: {sonuc.Basarili}, Başarısız: {sonuc.Basarisiz}, Atlanan: {sonuc.Atlanan}",
                sonuc.Basarisiz == 0);

            return sonuc;
        }
    }
}

using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.Core.Interfaces;
using PrinterTrackingApp.DataAccess.Repositories;

namespace PrinterTrackingApp.Business.Services
{
    public class YaziciOlcumService
    {
        private readonly IPrinterMonitor _printerMonitor;
        private readonly YaziciSayacOlcumRepository _olcumRepository;
        private readonly YaziciRepository _yaziciRepository;
        private readonly TonerOlcumRepository _tonerOlcumRepository;
        private readonly DrumOlcumRepository _drumOlcumRepository;

        public PrinterSnapshot? SonSnapshot { get; private set; }

        public YaziciOlcumService(IPrinterMonitor printerMonitor)
        {
            _printerMonitor = printerMonitor;
            _olcumRepository = new YaziciSayacOlcumRepository();
            _yaziciRepository = new YaziciRepository();
            _tonerOlcumRepository = new TonerOlcumRepository();
            _drumOlcumRepository = new DrumOlcumRepository();
        }

        public YaziciSayacOlcumu OlcumAl(Yazici yazici)
        {
            var snapshot = _printerMonitor.Oku(yazici.IpAdresi);
            SonSnapshot = snapshot;
            string kaynak = string.IsNullOrWhiteSpace(snapshot.VeriKaynagi)
                ? "UNKNOWN"
                : snapshot.VeriKaynagi;

            var olcum = new YaziciSayacOlcumu
            {
                YaziciId = yazici.Id,
                ToplamSayac = snapshot.ToplamSayfa,
                OlcumTarihi = snapshot.OlcumZamani.Date,
                OlcumZamani = snapshot.OlcumZamani,
                OlcumBasariliMi = snapshot.BasariliMi,
                HataMesaji = snapshot.HataMesaji,
                VeriKaynagi = kaynak
            };

            if (snapshot.BasariliMi && snapshot.ToplamSayfa.HasValue)
            {
                var gununIlkOlcumu = _olcumRepository.GununIlkBasariliOlcumunuGetir(
                    yazici.Id,
                    snapshot.OlcumZamani.Date);

                if (gununIlkOlcumu?.ToplamSayac != null)
                {
                    int fark = snapshot.ToplamSayfa.Value - gununIlkOlcumu.ToplamSayac.Value;
                    olcum.GunlukBaski = fark >= 0 ? fark : null;
                }

                _yaziciRepository.SonBasariliIletisimiGuncelle(
                    yazici.Id,
                    snapshot.OlcumZamani);
            }

            _olcumRepository.Add(olcum);

            if (snapshot.BasariliMi)
            {
                TonerOlcumleriniKaydet(yazici, snapshot, kaynak);
                DrumOlcumunuKaydet(yazici, snapshot, kaynak);
            }

            return olcum;
        }

        private void TonerOlcumleriniKaydet(Yazici yazici, PrinterSnapshot snapshot, string kaynak)
        {
            TonerKaydet(yazici.Id, "Siyah", snapshot.SiyahTonerYuzde, snapshot.OlcumZamani, kaynak);

            if (!yazici.RenkliMi)
                return;

            TonerKaydet(yazici.Id, "Cyan", snapshot.CyanTonerYuzde, snapshot.OlcumZamani, kaynak);
            TonerKaydet(yazici.Id, "Magenta", snapshot.MagentaTonerYuzde, snapshot.OlcumZamani, kaynak);
            TonerKaydet(yazici.Id, "Sari", snapshot.SariTonerYuzde, snapshot.OlcumZamani, kaynak);
        }

        private void TonerKaydet(int yaziciId, string renk, int? seviye, DateTime olcumZamani, string kaynak)
        {

            if (!seviye.HasValue)
                return;

            _tonerOlcumRepository.Add(new TonerOlcumu
            {
                YaziciId = yaziciId,
                Renk = renk,
                SeviyeYuzde = seviye,
                OlcumZamani = olcumZamani,
                OlcumBasariliMi = true,
                HataMesaji = null,
                VeriKaynagi = kaynak
            });
        }

        private void DrumOlcumunuKaydet(Yazici yazici, PrinterSnapshot snapshot, string kaynak)
        {
            if (!snapshot.DrumYuzde.HasValue)
                return;

            _drumOlcumRepository.Add(new DrumOlcumu
            {
                YaziciId = yazici.Id,
                SeviyeYuzde = snapshot.DrumYuzde,
                OlcumZamani = snapshot.OlcumZamani,
                OlcumBasariliMi = true,
                HataMesaji = null,
                VeriKaynagi = kaynak
            });
        }
    }
}

using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Interfaces;

namespace PrinterTrackingApp.Business.Services
{
    public class FakePrinterMonitor : IPrinterMonitor
    {
        private readonly Random _random = new();
        private readonly Dictionary<string, int> _sayaclar = new();
        private readonly Dictionary<string, int> _siyahToner = new();
        private readonly Dictionary<string, int> _cyanToner = new();
        private readonly Dictionary<string, int> _magentaToner = new();
        private readonly Dictionary<string, int> _sariToner = new();
        private readonly Dictionary<string, int> _drum = new();

        public PrinterSnapshot Oku(string ipAdresi)
        {
            IlkDegerleriOlustur(ipAdresi);

            int yeniBaski = _random.Next(0, 50);
            _sayaclar[ipAdresi] += yeniBaski;

            _siyahToner[ipAdresi] = Azalt(_siyahToner[ipAdresi], yeniBaski);
            _cyanToner[ipAdresi] = Azalt(_cyanToner[ipAdresi], yeniBaski);
            _magentaToner[ipAdresi] = Azalt(_magentaToner[ipAdresi], yeniBaski);
            _sariToner[ipAdresi] = Azalt(_sariToner[ipAdresi], yeniBaski);

            if (yeniBaski > 0 && _random.Next(0, 100) < 15)
                _drum[ipAdresi] = Math.Max(0, _drum[ipAdresi] - 1);

            return new PrinterSnapshot
            {
                IpAdresi = ipAdresi,
                ToplamSayfa = _sayaclar[ipAdresi],
                SiyahTonerYuzde = _siyahToner[ipAdresi],
                CyanTonerYuzde = _cyanToner[ipAdresi],
                MagentaTonerYuzde = _magentaToner[ipAdresi],
                SariTonerYuzde = _sariToner[ipAdresi],
                DrumYuzde = _drum[ipAdresi],
                Durum = "Hazır",
                CihazAciklamasi = "FAKE yazıcı",
                OlcumZamani = DateTime.Now,
                BasariliMi = true,
                HataMesaji = null,
                VeriKaynagi = "FAKE"
            };
        }

        private void IlkDegerleriOlustur(string ipAdresi)
        {
            if (!_sayaclar.ContainsKey(ipAdresi)) _sayaclar[ipAdresi] = _random.Next(10000, 50000);
            if (!_siyahToner.ContainsKey(ipAdresi)) _siyahToner[ipAdresi] = _random.Next(60, 101);
            if (!_cyanToner.ContainsKey(ipAdresi)) _cyanToner[ipAdresi] = _random.Next(60, 101);
            if (!_magentaToner.ContainsKey(ipAdresi)) _magentaToner[ipAdresi] = _random.Next(60, 101);
            if (!_sariToner.ContainsKey(ipAdresi)) _sariToner[ipAdresi] = _random.Next(60, 101);
            if (!_drum.ContainsKey(ipAdresi)) _drum[ipAdresi] = _random.Next(70, 101);
        }

        private int Azalt(int mevcutSeviye, int yeniBaski)
        {
            if (yeniBaski <= 0 || _random.Next(0, 100) >= 35)
                return mevcutSeviye;

            return Math.Max(0, mevcutSeviye - _random.Next(1, 3));
        }
    }
}

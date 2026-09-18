namespace PrinterTrackingApp.Core.DTOs
{
    public class DashboardDurumu
    {
        public int AktifYaziciSayisi { get; set; }
        public int KritikTonerSayisi { get; set; }
        public int KritikDrumSayisi { get; set; }
        public int SonOlcumuBasarisizSayisi { get; set; }
        public DateTime? SonOlcumZamani { get; set; }

        public int ToplamUyari =>
            KritikTonerSayisi + KritikDrumSayisi + SonOlcumuBasarisizSayisi;
    }
}

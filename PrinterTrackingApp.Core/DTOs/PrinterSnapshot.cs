namespace PrinterTrackingApp.Core.DTOs
{
    public class PrinterSnapshot
    {
        public string IpAdresi { get; set; } = string.Empty;
        public int? ToplamSayfa { get; set; }
        public int? SiyahTonerYuzde { get; set; }
        public int? CyanTonerYuzde { get; set; }
        public int? MagentaTonerYuzde { get; set; }
        public int? SariTonerYuzde { get; set; }
        public int? DrumYuzde { get; set; }
        public string? Durum { get; set; }
        public string? CihazAciklamasi { get; set; }
        public DateTime OlcumZamani { get; set; }
        public bool BasariliMi { get; set; }
        public string? HataMesaji { get; set; }
        public string VeriKaynagi { get; set; } = "UNKNOWN";
    }
}

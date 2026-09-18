namespace PrinterTrackingApp.Core.Entities
{
    public class IslemLogu
    {
        public int Id { get; set; }
        public int? YaziciId { get; set; }
        public string? IpAdresi { get; set; }
        public string IslemTipi { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public DateTime Tarih { get; set; }
        public bool BasariliMi { get; set; }
        public string? HataMesaji { get; set; }
    }
}

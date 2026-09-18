namespace PrinterTrackingApp.Core.DTOs
{
    public class UyariDto
    {
        public int YaziciId { get; set; }
        public string IpAdresi { get; set; } = string.Empty;
        public string MarkaModel { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public string Detay { get; set; } = string.Empty;
        public int? SeviyeYuzde { get; set; }
        public DateTime? OlcumZamani { get; set; }
    }
}

namespace PrinterTrackingApp.Core.DTOs
{
    public class TonerLogDto
    {
        public int Id { get; set; }
        public int YaziciId { get; set; }
        public string IpAdresi { get; set; } = string.Empty;
        public string MarkaModel { get; set; } = string.Empty;
        public string Renk { get; set; } = string.Empty;
        public int? SeviyeYuzde { get; set; }
        public DateTime OlcumZamani { get; set; }
        public string VeriKaynagi { get; set; } = string.Empty;
    }
}

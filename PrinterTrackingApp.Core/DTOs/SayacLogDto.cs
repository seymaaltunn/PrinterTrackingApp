namespace PrinterTrackingApp.Core.DTOs
{
    public class SayacLogDto
    {
        public int Id { get; set; }
        public int YaziciId { get; set; }
        public string IpAdresi { get; set; } = string.Empty;
        public string MarkaModel { get; set; } = string.Empty;
        public int? ToplamSayac { get; set; }
        public int? GunlukBaski { get; set; }
        public DateTime OlcumZamani { get; set; }
        public bool BasariliMi { get; set; }
        public string? HataMesaji { get; set; }
        public string VeriKaynagi { get; set; } = string.Empty;
    }
}

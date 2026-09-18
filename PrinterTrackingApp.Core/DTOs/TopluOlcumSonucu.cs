namespace PrinterTrackingApp.Core.DTOs
{
    public class TopluOlcumSonucu
    {
        public int ToplamYazici { get; set; }
        public int Basarili { get; set; }
        public int Basarisiz { get; set; }
        public int Atlanan { get; set; }
        public DateTime BaslamaZamani { get; set; }
        public DateTime BitisZamani { get; set; }

        public TimeSpan Sure => BitisZamani - BaslamaZamani;
    }
}

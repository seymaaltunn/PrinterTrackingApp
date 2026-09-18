using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Repositories;

namespace PrinterTrackingApp.Business.Services
{
    public class LogService
    {
        private readonly YaziciSayacOlcumRepository _sayacRepository = new();
        private readonly TonerOlcumRepository _tonerRepository = new();
        private readonly DrumOlcumRepository _drumRepository = new();
        private readonly IslemLogRepository _islemRepository = new();

        public List<SayacLogDto> SayacLoglariniGetir(DateTime baslangic, DateTime bitis, string? ip)
            => _sayacRepository.GetLoglar(baslangic, bitis, ip);

        public List<TonerLogDto> TonerLoglariniGetir(DateTime baslangic, DateTime bitis, string? ip)
            => _tonerRepository.GetLoglar(baslangic, bitis, ip);

        public List<DrumLogDto> DrumLoglariniGetir(DateTime baslangic, DateTime bitis, string? ip)
            => _drumRepository.GetLoglar(baslangic, bitis, ip);

        public List<IslemLogu> IslemLoglariniGetir(DateTime baslangic, DateTime bitis, string? ip)
            => _islemRepository.GetLoglar(baslangic, bitis, ip);
    }
}

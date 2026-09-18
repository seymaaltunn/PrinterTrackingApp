using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.DataAccess.Repositories;

namespace PrinterTrackingApp.Business.Services
{
    public class DashboardService
    {
        public const int VarsayilanKritikEsik = 20;

        private readonly DashboardRepository _repository = new();

        public DashboardDurumu DurumuGetir(int kritikEsik = VarsayilanKritikEsik)
        {
            KritikEsigiDogrula(kritikEsik);
            return _repository.GetDurum(kritikEsik);
        }

        public List<UyariDto> UyarilariGetir(int kritikEsik = VarsayilanKritikEsik)
        {
            KritikEsigiDogrula(kritikEsik);
            return _repository.GetUyarilar(kritikEsik);
        }

        private static void KritikEsigiDogrula(int kritikEsik)
        {
            if (kritikEsik < 0 || kritikEsik > 100)
                throw new ArgumentOutOfRangeException(nameof(kritikEsik), "Kritik eşik 0-100 arasında olmalıdır.");
        }
    }
}

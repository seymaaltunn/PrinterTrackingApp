using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Repositories;

namespace PrinterTrackingApp.Business.Services
{
    public class IslemLogService
    {
        private readonly IslemLogRepository _repository = new();

        public void Kaydet(
            string islemTipi,
            string aciklama,
            bool basariliMi,
            int? yaziciId = null,
            string? ipAdresi = null,
            string? hataMesaji = null)
        {
            try
            {
                _repository.Add(new IslemLogu
                {
                    YaziciId = yaziciId,
                    IpAdresi = ipAdresi,
                    IslemTipi = islemTipi,
                    Aciklama = aciklama,
                    Tarih = DateTime.Now,
                    BasariliMi = basariliMi,
                    HataMesaji = hataMesaji
                });
            }
            catch
            {

            }
        }
    }
}

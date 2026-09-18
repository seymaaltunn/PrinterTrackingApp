using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Repositories;

namespace PrinterTrackingApp.Business.Services
{
    public class YaziciService
    {
        private readonly YaziciRepository _yaziciRepository;
        private readonly IslemLogService _logService;

        public YaziciService()
        {
            _yaziciRepository = new YaziciRepository();
            _logService = new IslemLogService();
        }

        public List<Yazici> TumYazicilariGetir()
        {
            return _yaziciRepository.GetAll();
        }

        public void YaziciEkle(Yazici yazici)
        {
            YaziciyiDogrula(yazici);

            if (_yaziciRepository.IpAdresiVarMi(yazici.IpAdresi))
                throw new Exception("Bu IP adresine sahip bir yazıcı zaten kayıtlı.");

            try
            {
                _yaziciRepository.Add(yazici);
                _logService.Kaydet(
                    "YAZICI_EKLENDI",
                    $"Yazıcı eklendi: {yazici.Marka} {yazici.Model}",
                    true,
                    null,
                    yazici.IpAdresi);
            }
            catch (Exception ex)
            {
                _logService.Kaydet(
                    "YAZICI_EKLEME_HATASI",
                    "Yazıcı eklenemedi.",
                    false,
                    null,
                    yazici.IpAdresi,
                    ex.Message);
                throw;
            }
        }

        public void YaziciSil(int id)
        {
            if (id <= 0)
                throw new Exception("Geçersiz yazıcı seçimi.");

            var yazici = _yaziciRepository.GetAll().FirstOrDefault(x => x.Id == id);

            try
            {
                _yaziciRepository.Delete(id);
                _logService.Kaydet(
                    "YAZICI_SILINDI",
                    $"Yazıcı silindi. ID: {id}",
                    true,
                    id,
                    yazici?.IpAdresi);
            }
            catch (Exception ex)
            {
                _logService.Kaydet(
                    "YAZICI_SILME_HATASI",
                    $"Yazıcı silinemedi. ID: {id}",
                    false,
                    id,
                    yazici?.IpAdresi,
                    ex.Message);
                throw;
            }
        }

        public void YaziciGuncelle(Yazici yazici)
        {
            if (yazici.Id <= 0)
                throw new Exception("Geçersiz yazıcı seçimi.");

            YaziciyiDogrula(yazici);

            if (_yaziciRepository.IpAdresiBaskaYazicidaVarMi(yazici.IpAdresi, yazici.Id))
                throw new Exception("Bu IP adresi başka bir yazıcı tarafından kullanılıyor.");

            try
            {
                _yaziciRepository.Update(yazici);
                _logService.Kaydet(
                    "YAZICI_GUNCELLENDI",
                    $"Yazıcı güncellendi: {yazici.Marka} {yazici.Model}",
                    true,
                    yazici.Id,
                    yazici.IpAdresi);
            }
            catch (Exception ex)
            {
                _logService.Kaydet(
                    "YAZICI_GUNCELLEME_HATASI",
                    "Yazıcı güncellenemedi.",
                    false,
                    yazici.Id,
                    yazici.IpAdresi,
                    ex.Message);
                throw;
            }
        }

        private static void YaziciyiDogrula(Yazici yazici)
        {
            if (string.IsNullOrWhiteSpace(yazici.IpAdresi))
                throw new Exception("IP adresi boş bırakılamaz.");

            string[] ipParcalari = yazici.IpAdresi.Split('.');
            if (ipParcalari.Length != 4)
                throw new Exception("IP adresi 4 bölümden oluşmalıdır. Örnek: 192.168.1.10");

            foreach (string parca in ipParcalari)
            {
                if (!int.TryParse(parca, out int sayi) || sayi < 0 || sayi > 255)
                    throw new Exception("Geçerli bir IPv4 adresi giriniz. Örnek: 192.168.1.10");
            }

            if (string.IsNullOrWhiteSpace(yazici.Marka))
                throw new Exception("Marka boş bırakılamaz.");

            if (string.IsNullOrWhiteSpace(yazici.Model))
                throw new Exception("Model boş bırakılamaz.");

            if (string.IsNullOrWhiteSpace(yazici.Konum))
                throw new Exception("Konum boş bırakılamaz.");
        }
    }
}

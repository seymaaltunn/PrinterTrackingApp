using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.Business.Services
{
    public class DatabaseBackupService
    {
        public string YedekAl(string hedefKlasor)
        {
            if (string.IsNullOrWhiteSpace(hedefKlasor))
                throw new ArgumentException("Yedek klasörü belirtilmedi.", nameof(hedefKlasor));


            using (var connection = DbConnectionFactory.CreateConnection())
            {
                connection.Open();
            }

            string kaynak = DbConnectionFactory.GetDatabasePath();
            if (!File.Exists(kaynak))
                throw new FileNotFoundException("SQLite veritabanı bulunamadı.", kaynak);

            Directory.CreateDirectory(hedefKlasor);

            string ad = $"YaziciTakip_{DateTime.Now:yyyyMMdd_HHmmss}.db";
            string hedef = Path.Combine(hedefKlasor, ad);

            int sira = 1;
            while (File.Exists(hedef))
            {
                hedef = Path.Combine(
                    hedefKlasor,
                    $"YaziciTakip_{DateTime.Now:yyyyMMdd_HHmmss}_{sira++}.db");
            }

            File.Copy(kaynak, hedef, overwrite: false);
            return hedef;
        }

        public string VarsayilanYedekKlasoru()
        {
            string belgeler = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return Path.Combine(belgeler, "YaziciTakipYedekleri");
        }
    }
}

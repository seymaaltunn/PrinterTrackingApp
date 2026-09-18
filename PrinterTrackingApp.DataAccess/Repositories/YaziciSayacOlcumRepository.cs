using Microsoft.Data.Sqlite;
using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.DataAccess.Repositories
{
    public class YaziciSayacOlcumRepository
    {
        public void Add(YaziciSayacOlcumu olcum)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO yazici_sayac_olcumleri
                (yazici_id, toplam_sayac, gunluk_baski, olcum_tarihi,
                 olcum_zamani, olcum_basarili_mi, hata_mesaji, veri_kaynagi)
                VALUES
                (@yazici_id, @toplam_sayac, @gunluk_baski, @olcum_tarihi,
                 @olcum_zamani, @olcum_basarili_mi, @hata_mesaji, @veri_kaynagi)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@yazici_id", olcum.YaziciId);
            command.Parameters.AddWithValue("@toplam_sayac", (object?)olcum.ToplamSayac ?? DBNull.Value);
            command.Parameters.AddWithValue("@gunluk_baski", (object?)olcum.GunlukBaski ?? DBNull.Value);
            command.Parameters.AddWithValue("@olcum_tarihi", olcum.OlcumTarihi.Date);
            command.Parameters.AddWithValue("@olcum_zamani", olcum.OlcumZamani);
            command.Parameters.AddWithValue("@olcum_basarili_mi", olcum.OlcumBasariliMi);
            command.Parameters.AddWithValue("@hata_mesaji", (object?)olcum.HataMesaji ?? DBNull.Value);
            command.Parameters.AddWithValue("@veri_kaynagi", olcum.VeriKaynagi);
            command.ExecuteNonQuery();
        }

        public YaziciSayacOlcumu? SonBasariliOlcumuGetir(int yaziciId)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT id, yazici_id, toplam_sayac, gunluk_baski,
                       olcum_tarihi, olcum_zamani, olcum_basarili_mi,
                       hata_mesaji, veri_kaynagi
                FROM yazici_sayac_olcumleri
                WHERE yazici_id = @yazici_id
                  AND olcum_basarili_mi = 1
                  AND toplam_sayac IS NOT NULL
                ORDER BY olcum_zamani DESC
                LIMIT 1";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@yazici_id", yaziciId);
            using var reader = command.ExecuteReader();
            return reader.Read() ? MapEntity(reader) : null;
        }

        public YaziciSayacOlcumu? GununIlkBasariliOlcumunuGetir(int yaziciId, DateTime tarih)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT id, yazici_id, toplam_sayac, gunluk_baski,
                       olcum_tarihi, olcum_zamani, olcum_basarili_mi,
                       hata_mesaji, veri_kaynagi
                FROM yazici_sayac_olcumleri
                WHERE yazici_id = @yazici_id
                  AND olcum_basarili_mi = 1
                  AND toplam_sayac IS NOT NULL
                  AND olcum_tarihi = @olcum_tarihi
                ORDER BY olcum_zamani ASC
                LIMIT 1";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@yazici_id", yaziciId);
            command.Parameters.AddWithValue("@olcum_tarihi", tarih.Date);
            using var reader = command.ExecuteReader();
            return reader.Read() ? MapEntity(reader) : null;
        }

        public List<SayacLogDto> GetLoglar(DateTime baslangic, DateTime bitis, string? ipAdresi)
        {
            var liste = new List<SayacLogDto>();
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT o.id, o.yazici_id, y.ip_adresi, y.marka, y.model,
                       o.toplam_sayac, o.gunluk_baski, o.olcum_zamani,
                       o.olcum_basarili_mi, o.hata_mesaji, o.veri_kaynagi
                FROM yazici_sayac_olcumleri o
                INNER JOIN yazicilar y ON y.id = o.yazici_id
                WHERE o.olcum_zamani >= @baslangic
                  AND o.olcum_zamani < @bitis_sonrasi
                  AND (@ip = '' OR y.ip_adresi LIKE '%' || @ip || '%')
                ORDER BY o.olcum_zamani DESC";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@bitis", bitis.Date);
            command.Parameters.AddWithValue("@bitis_sonrasi", bitis.Date.AddDays(1));
            command.Parameters.AddWithValue("@ip", ipAdresi?.Trim() ?? string.Empty);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste.Add(new SayacLogDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    YaziciId = reader.GetInt32(reader.GetOrdinal("yazici_id")),
                    IpAdresi = reader.GetString(reader.GetOrdinal("ip_adresi")),
                    MarkaModel = $"{reader.GetString(reader.GetOrdinal("marka"))} {reader.GetString(reader.GetOrdinal("model"))}",
                    ToplamSayac = reader.IsDBNull(reader.GetOrdinal("toplam_sayac")) ? null : reader.GetInt32(reader.GetOrdinal("toplam_sayac")),
                    GunlukBaski = reader.IsDBNull(reader.GetOrdinal("gunluk_baski")) ? null : reader.GetInt32(reader.GetOrdinal("gunluk_baski")),
                    OlcumZamani = reader.GetDateTime(reader.GetOrdinal("olcum_zamani")),
                    BasariliMi = reader.GetBoolean(reader.GetOrdinal("olcum_basarili_mi")),
                    HataMesaji = reader.IsDBNull(reader.GetOrdinal("hata_mesaji")) ? null : reader.GetString(reader.GetOrdinal("hata_mesaji")),
                    VeriKaynagi = reader.GetString(reader.GetOrdinal("veri_kaynagi"))
                });
            }

            return liste;
        }

        private static YaziciSayacOlcumu MapEntity(SqliteDataReader reader)
        {
            return new YaziciSayacOlcumu
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                YaziciId = reader.GetInt32(reader.GetOrdinal("yazici_id")),
                ToplamSayac = reader.IsDBNull(reader.GetOrdinal("toplam_sayac")) ? null : reader.GetInt32(reader.GetOrdinal("toplam_sayac")),
                GunlukBaski = reader.IsDBNull(reader.GetOrdinal("gunluk_baski")) ? null : reader.GetInt32(reader.GetOrdinal("gunluk_baski")),
                OlcumTarihi = reader.GetDateTime(reader.GetOrdinal("olcum_tarihi")),
                OlcumZamani = reader.GetDateTime(reader.GetOrdinal("olcum_zamani")),
                OlcumBasariliMi = reader.GetBoolean(reader.GetOrdinal("olcum_basarili_mi")),
                HataMesaji = reader.IsDBNull(reader.GetOrdinal("hata_mesaji")) ? null : reader.GetString(reader.GetOrdinal("hata_mesaji")),
                VeriKaynagi = reader.GetString(reader.GetOrdinal("veri_kaynagi"))
            };
        }
    }
}

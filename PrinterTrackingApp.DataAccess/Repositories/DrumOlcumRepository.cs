using Microsoft.Data.Sqlite;
using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.DataAccess.Repositories
{
    public class DrumOlcumRepository
    {
        public void Add(DrumOlcumu olcum)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO drum_olcumleri
                (yazici_id, seviye_yuzde, olcum_zamani,
                 olcum_basarili_mi, hata_mesaji, veri_kaynagi)
                VALUES
                (@yazici_id, @seviye_yuzde, @olcum_zamani,
                 @olcum_basarili_mi, @hata_mesaji, @veri_kaynagi)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@yazici_id", olcum.YaziciId);
            command.Parameters.AddWithValue("@seviye_yuzde", (object?)olcum.SeviyeYuzde ?? DBNull.Value);
            command.Parameters.AddWithValue("@olcum_zamani", olcum.OlcumZamani);
            command.Parameters.AddWithValue("@olcum_basarili_mi", olcum.OlcumBasariliMi);
            command.Parameters.AddWithValue("@hata_mesaji", (object?)olcum.HataMesaji ?? DBNull.Value);
            command.Parameters.AddWithValue("@veri_kaynagi", olcum.VeriKaynagi);
            command.ExecuteNonQuery();
        }

        public List<DrumLogDto> GetLoglar(DateTime baslangic, DateTime bitis, string? ipAdresi)
        {
            var liste = new List<DrumLogDto>();
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT o.id, o.yazici_id, y.ip_adresi, y.marka, y.model,
                       o.seviye_yuzde, o.olcum_zamani, o.veri_kaynagi
                FROM drum_olcumleri o
                INNER JOIN yazicilar y ON y.id = o.yazici_id
                WHERE o.olcum_zamani >= @baslangic
                  AND o.olcum_zamani < @bitis_sonrasi
                  AND (@ip = '' OR y.ip_adresi LIKE '%' || @ip || '%')
                ORDER BY o.olcum_zamani DESC, o.id DESC";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@bitis", bitis.Date);
            command.Parameters.AddWithValue("@bitis_sonrasi", bitis.Date.AddDays(1));
            command.Parameters.AddWithValue("@ip", ipAdresi?.Trim() ?? string.Empty);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste.Add(new DrumLogDto
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    YaziciId = reader.GetInt32(reader.GetOrdinal("yazici_id")),
                    IpAdresi = reader.GetString(reader.GetOrdinal("ip_adresi")),
                    MarkaModel = $"{reader.GetString(reader.GetOrdinal("marka"))} {reader.GetString(reader.GetOrdinal("model"))}",
                    SeviyeYuzde = reader.IsDBNull(reader.GetOrdinal("seviye_yuzde")) ? null : reader.GetInt32(reader.GetOrdinal("seviye_yuzde")),
                    OlcumZamani = reader.GetDateTime(reader.GetOrdinal("olcum_zamani")),
                    VeriKaynagi = reader.GetString(reader.GetOrdinal("veri_kaynagi"))
                });
            }

            return liste;
        }
    }
}

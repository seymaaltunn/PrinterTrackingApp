using Microsoft.Data.Sqlite;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.DataAccess.Repositories
{
    public class IslemLogRepository
    {
        public void Add(IslemLogu log)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO islem_loglari
                (yazici_id, ip_adresi, islem_tipi, aciklama, tarih, basarili_mi, hata_mesaji)
                VALUES
                (@yazici_id, @ip_adresi, @islem_tipi, @aciklama, @tarih, @basarili_mi, @hata_mesaji)";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@yazici_id", (object?)log.YaziciId ?? DBNull.Value);
            command.Parameters.AddWithValue("@ip_adresi", (object?)log.IpAdresi ?? DBNull.Value);
            command.Parameters.AddWithValue("@islem_tipi", log.IslemTipi);
            command.Parameters.AddWithValue("@aciklama", log.Aciklama);
            command.Parameters.AddWithValue("@tarih", log.Tarih);
            command.Parameters.AddWithValue("@basarili_mi", log.BasariliMi);
            command.Parameters.AddWithValue("@hata_mesaji", (object?)log.HataMesaji ?? DBNull.Value);
            command.ExecuteNonQuery();
        }

        public List<IslemLogu> GetLoglar(DateTime baslangic, DateTime bitis, string? ipAdresi)
        {
            var liste = new List<IslemLogu>();
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT id, yazici_id, ip_adresi, islem_tipi, aciklama,
                       tarih, basarili_mi, hata_mesaji
                FROM islem_loglari
                WHERE tarih >= @baslangic
                  AND tarih < @bitis_sonrasi
                  AND (@ip = '' OR ip_adresi LIKE '%' || @ip || '%')
                ORDER BY tarih DESC";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@baslangic", baslangic.Date);
            command.Parameters.AddWithValue("@bitis", bitis.Date);
            command.Parameters.AddWithValue("@bitis_sonrasi", bitis.Date.AddDays(1));
            command.Parameters.AddWithValue("@ip", ipAdresi?.Trim() ?? string.Empty);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste.Add(new IslemLogu
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    YaziciId = reader.IsDBNull(reader.GetOrdinal("yazici_id"))
                        ? null : reader.GetInt32(reader.GetOrdinal("yazici_id")),
                    IpAdresi = reader.IsDBNull(reader.GetOrdinal("ip_adresi"))
                        ? null : reader.GetString(reader.GetOrdinal("ip_adresi")),
                    IslemTipi = reader.GetString(reader.GetOrdinal("islem_tipi")),
                    Aciklama = reader.GetString(reader.GetOrdinal("aciklama")),
                    Tarih = reader.GetDateTime(reader.GetOrdinal("tarih")),
                    BasariliMi = reader.GetBoolean(reader.GetOrdinal("basarili_mi")),
                    HataMesaji = reader.IsDBNull(reader.GetOrdinal("hata_mesaji"))
                        ? null : reader.GetString(reader.GetOrdinal("hata_mesaji"))
                });
            }

            return liste;
        }
    }
}

using Microsoft.Data.Sqlite;
using PrinterTrackingApp.Core.DTOs;
using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.DataAccess.Repositories
{
    public class DashboardRepository
    {
        public DashboardDurumu GetDurum(int kritikEsik)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT
                    (SELECT COUNT(*) FROM yazicilar WHERE aktif_mi = 1) AS aktif_yazici,

                    (SELECT COUNT(*)
                     FROM (
                        SELECT o.yazici_id, o.renk, o.seviye_yuzde,
                               ROW_NUMBER() OVER (
                                   PARTITION BY o.yazici_id, o.renk
                                   ORDER BY o.olcum_zamani DESC, o.id DESC
                               ) AS rn
                        FROM toner_olcumleri o
                        INNER JOIN yazicilar y ON y.id = o.yazici_id
                        WHERE y.aktif_mi = 1
                          AND o.olcum_basarili_mi = 1
                          AND o.seviye_yuzde IS NOT NULL
                     ) t
                     WHERE t.rn = 1 AND t.seviye_yuzde <= @esik) AS kritik_toner,

                    (SELECT COUNT(*)
                     FROM (
                        SELECT o.yazici_id, o.seviye_yuzde,
                               ROW_NUMBER() OVER (
                                   PARTITION BY o.yazici_id
                                   ORDER BY o.olcum_zamani DESC, o.id DESC
                               ) AS rn
                        FROM drum_olcumleri o
                        INNER JOIN yazicilar y ON y.id = o.yazici_id
                        WHERE y.aktif_mi = 1
                          AND o.olcum_basarili_mi = 1
                          AND o.seviye_yuzde IS NOT NULL
                     ) d
                     WHERE d.rn = 1 AND d.seviye_yuzde <= @esik) AS kritik_drum,

                    (SELECT COUNT(*)
                     FROM (
                        SELECT o.yazici_id, o.olcum_basarili_mi,
                               ROW_NUMBER() OVER (
                                   PARTITION BY o.yazici_id
                                   ORDER BY o.olcum_zamani DESC, o.id DESC
                               ) AS rn
                        FROM yazici_sayac_olcumleri o
                        INNER JOIN yazicilar y ON y.id = o.yazici_id
                        WHERE y.aktif_mi = 1
                     ) s
                     WHERE s.rn = 1 AND s.olcum_basarili_mi = 0) AS basarisiz_olcum,

                    (SELECT MAX(olcum_zamani) FROM yazici_sayac_olcumleri) AS son_olcum;";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@esik", kritikEsik);

            using var reader = command.ExecuteReader();
            if (!reader.Read())
                return new DashboardDurumu();

            return new DashboardDurumu
            {
                AktifYaziciSayisi = reader.GetInt32(reader.GetOrdinal("aktif_yazici")),
                KritikTonerSayisi = reader.GetInt32(reader.GetOrdinal("kritik_toner")),
                KritikDrumSayisi = reader.GetInt32(reader.GetOrdinal("kritik_drum")),
                SonOlcumuBasarisizSayisi = reader.GetInt32(reader.GetOrdinal("basarisiz_olcum")),
                SonOlcumZamani = reader.IsDBNull(reader.GetOrdinal("son_olcum"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("son_olcum"))
            };
        }

        public List<UyariDto> GetUyarilar(int kritikEsik)
        {
            var liste = new List<UyariDto>();

            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                WITH SonToner AS
                (
                    SELECT o.yazici_id, o.renk, o.seviye_yuzde, o.olcum_zamani,
                           ROW_NUMBER() OVER (
                               PARTITION BY o.yazici_id, o.renk
                               ORDER BY o.olcum_zamani DESC, o.id DESC
                           ) AS rn
                    FROM toner_olcumleri o
                    INNER JOIN yazicilar y ON y.id = o.yazici_id
                    WHERE y.aktif_mi = 1
                      AND o.olcum_basarili_mi = 1
                      AND o.seviye_yuzde IS NOT NULL
                ),
                SonDrum AS
                (
                    SELECT o.yazici_id, o.seviye_yuzde, o.olcum_zamani,
                           ROW_NUMBER() OVER (
                               PARTITION BY o.yazici_id
                               ORDER BY o.olcum_zamani DESC, o.id DESC
                           ) AS rn
                    FROM drum_olcumleri o
                    INNER JOIN yazicilar y ON y.id = o.yazici_id
                    WHERE y.aktif_mi = 1
                      AND o.olcum_basarili_mi = 1
                      AND o.seviye_yuzde IS NOT NULL
                ),
                SonSayac AS
                (
                    SELECT o.yazici_id, o.olcum_basarili_mi, o.hata_mesaji, o.olcum_zamani,
                           ROW_NUMBER() OVER (
                               PARTITION BY o.yazici_id
                               ORDER BY o.olcum_zamani DESC, o.id DESC
                           ) AS rn
                    FROM yazici_sayac_olcumleri o
                    INNER JOIN yazicilar y ON y.id = o.yazici_id
                    WHERE y.aktif_mi = 1
                )

                SELECT y.id AS yazici_id, y.ip_adresi, y.marka, y.model,
                       'TONER' AS kategori,
                       t.renk || ' toner seviyesi düşük' AS detay,
                       t.seviye_yuzde,
                       t.olcum_zamani
                FROM SonToner t
                INNER JOIN yazicilar y ON y.id = t.yazici_id
                WHERE t.rn = 1 AND t.seviye_yuzde <= @esik

                UNION ALL

                SELECT y.id, y.ip_adresi, y.marka, y.model,
                       'DRUM',
                       'Drum seviyesi düşük',
                       d.seviye_yuzde,
                       d.olcum_zamani
                FROM SonDrum d
                INNER JOIN yazicilar y ON y.id = d.yazici_id
                WHERE d.rn = 1 AND d.seviye_yuzde <= @esik

                UNION ALL

                SELECT y.id, y.ip_adresi, y.marka, y.model,
                       'İLETİŞİM',
                       COALESCE(NULLIF(s.hata_mesaji, ''), 'Son ölçüm başarısız'),
                       NULL,
                       s.olcum_zamani
                FROM SonSayac s
                INNER JOIN yazicilar y ON y.id = s.yazici_id
                WHERE s.rn = 1 AND s.olcum_basarili_mi = 0

                ORDER BY kategori, ip_adresi;";

            using var command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@esik", kritikEsik);

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                liste.Add(new UyariDto
                {
                    YaziciId = reader.GetInt32(reader.GetOrdinal("yazici_id")),
                    IpAdresi = reader.GetString(reader.GetOrdinal("ip_adresi")),
                    MarkaModel = $"{reader.GetString(reader.GetOrdinal("marka"))} {reader.GetString(reader.GetOrdinal("model"))}",
                    Kategori = reader.GetString(reader.GetOrdinal("kategori")),
                    Detay = reader.GetString(reader.GetOrdinal("detay")),
                    SeviyeYuzde = reader.IsDBNull(reader.GetOrdinal("seviye_yuzde"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("seviye_yuzde")),
                    OlcumZamani = reader.IsDBNull(reader.GetOrdinal("olcum_zamani"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("olcum_zamani"))
                });
            }

            return liste;
        }
    }
}

using Microsoft.Data.Sqlite;
using PrinterTrackingApp.Core.Entities;
using PrinterTrackingApp.DataAccess.Context;

namespace PrinterTrackingApp.DataAccess.Repositories
{
    public class YaziciRepository
    {
        public List<Yazici> GetAll()
        {
            var yazicilar = new List<Yazici>();
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                SELECT id, ip_adresi, marka, model, renkli_mi, baski_teknolojisi,
                       konum, fotokopi_var_mi, gorsel_yolu, olusturma_tarihi,
                       aktif_mi, snmp_aktif_mi, snmp_community, snmp_version,
                       son_basarili_iletisim
                FROM yazicilar";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                yazicilar.Add(new Yazici
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    IpAdresi = reader.GetString(reader.GetOrdinal("ip_adresi")),
                    Marka = reader.GetString(reader.GetOrdinal("marka")),
                    Model = reader.GetString(reader.GetOrdinal("model")),
                    RenkliMi = reader.GetBoolean(reader.GetOrdinal("renkli_mi")),
                    BaskiTeknolojisi = GetNullableString(reader, "baski_teknolojisi"),
                    Konum = reader.GetString(reader.GetOrdinal("konum")),
                    FotokopiVarMi = reader.GetBoolean(reader.GetOrdinal("fotokopi_var_mi")),
                    GorselYolu = GetNullableString(reader, "gorsel_yolu"),
                    OlusturmaTarihi = GetNullableDateTime(reader, "olusturma_tarihi"),
                    AktifMi = reader.GetBoolean(reader.GetOrdinal("aktif_mi")),
                    SnmpAktifMi = reader.GetBoolean(reader.GetOrdinal("snmp_aktif_mi")),
                    SnmpCommunity = reader.GetString(reader.GetOrdinal("snmp_community")),
                    SnmpVersion = reader.GetString(reader.GetOrdinal("snmp_version")),
                    SonBasariliIletisim = GetNullableDateTime(reader, "son_basarili_iletisim")
                });
            }

            return yazicilar;
        }

        public void Add(Yazici yazici)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                INSERT INTO yazicilar
                (ip_adresi, marka, model, renkli_mi, baski_teknolojisi, konum,
                 fotokopi_var_mi, gorsel_yolu, olusturma_tarihi, aktif_mi,
                 snmp_aktif_mi, snmp_community, snmp_version, son_basarili_iletisim)
                VALUES
                (@ip_adresi, @marka, @model, @renkli_mi, @baski_teknolojisi, @konum,
                 @fotokopi_var_mi, @gorsel_yolu, @olusturma_tarihi, @aktif_mi,
                 @snmp_aktif_mi, @snmp_community, @snmp_version, @son_basarili_iletisim)";

            using var command = new SqliteCommand(query, connection);
            YaziciParametreleriniEkle(command, yazici, idDahil: false);
            command.ExecuteNonQuery();
        }

        public void Update(Yazici yazici)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();

            const string query = @"
                UPDATE yazicilar SET
                    ip_adresi = @ip_adresi,
                    marka = @marka,
                    model = @model,
                    renkli_mi = @renkli_mi,
                    baski_teknolojisi = @baski_teknolojisi,
                    konum = @konum,
                    fotokopi_var_mi = @fotokopi_var_mi,
                    gorsel_yolu = @gorsel_yolu,
                    aktif_mi = @aktif_mi,
                    snmp_aktif_mi = @snmp_aktif_mi,
                    snmp_community = @snmp_community,
                    snmp_version = @snmp_version,
                    son_basarili_iletisim = @son_basarili_iletisim
                WHERE id = @id";

            using var command = new SqliteCommand(query, connection);
            YaziciParametreleriniEkle(command, yazici, idDahil: true);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            using var command = new SqliteCommand("DELETE FROM yazicilar WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public bool IpAdresiVarMi(string ipAdresi)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            using var command = new SqliteCommand("SELECT COUNT(*) FROM yazicilar WHERE ip_adresi = @ip", connection);
            command.Parameters.AddWithValue("@ip", ipAdresi);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public bool IpAdresiBaskaYazicidaVarMi(string ipAdresi, int mevcutYaziciId)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            using var command = new SqliteCommand(
                "SELECT COUNT(*) FROM yazicilar WHERE ip_adresi = @ip AND id <> @id", connection);
            command.Parameters.AddWithValue("@ip", ipAdresi);
            command.Parameters.AddWithValue("@id", mevcutYaziciId);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public void SonBasariliIletisimiGuncelle(int yaziciId, DateTime tarih)
        {
            using var connection = DbConnectionFactory.CreateConnection();
            connection.Open();
            using var command = new SqliteCommand(
                "UPDATE yazicilar SET son_basarili_iletisim = @tarih WHERE id = @id", connection);
            command.Parameters.AddWithValue("@id", yaziciId);
            command.Parameters.AddWithValue("@tarih", tarih);
            command.ExecuteNonQuery();
        }

        private static void YaziciParametreleriniEkle(SqliteCommand command, Yazici yazici, bool idDahil)
        {
            if (idDahil)
                command.Parameters.AddWithValue("@id", yazici.Id);

            command.Parameters.AddWithValue("@ip_adresi", yazici.IpAdresi);
            command.Parameters.AddWithValue("@marka", yazici.Marka);
            command.Parameters.AddWithValue("@model", yazici.Model);
            command.Parameters.AddWithValue("@renkli_mi", yazici.RenkliMi);
            command.Parameters.AddWithValue("@baski_teknolojisi", (object?)yazici.BaskiTeknolojisi ?? DBNull.Value);
            command.Parameters.AddWithValue("@konum", yazici.Konum);
            command.Parameters.AddWithValue("@fotokopi_var_mi", yazici.FotokopiVarMi);
            command.Parameters.AddWithValue("@gorsel_yolu", (object?)yazici.GorselYolu ?? DBNull.Value);
            command.Parameters.AddWithValue("@olusturma_tarihi", yazici.OlusturmaTarihi ?? DateTime.Now);
            command.Parameters.AddWithValue("@aktif_mi", yazici.AktifMi);
            command.Parameters.AddWithValue("@snmp_aktif_mi", yazici.SnmpAktifMi);
            command.Parameters.AddWithValue("@snmp_community", yazici.SnmpCommunity);
            command.Parameters.AddWithValue("@snmp_version", yazici.SnmpVersion);
            command.Parameters.AddWithValue("@son_basarili_iletisim", (object?)yazici.SonBasariliIletisim ?? DBNull.Value);
        }

        private static string? GetNullableString(SqliteDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetString(ordinal);
        }

        private static DateTime? GetNullableDateTime(SqliteDataReader reader, string column)
        {
            int ordinal = reader.GetOrdinal(column);
            return reader.IsDBNull(ordinal) ? null : reader.GetDateTime(ordinal);
        }
    }
}

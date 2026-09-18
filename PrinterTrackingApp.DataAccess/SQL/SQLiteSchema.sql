PRAGMA foreign_keys = ON;

CREATE TABLE IF NOT EXISTS yazicilar (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    ip_adresi TEXT NOT NULL UNIQUE,
    marka TEXT NOT NULL,
    model TEXT NOT NULL,
    renkli_mi INTEGER NOT NULL DEFAULT 0,
    baski_teknolojisi TEXT NULL,
    konum TEXT NOT NULL DEFAULT '',
    fotokopi_var_mi INTEGER NOT NULL DEFAULT 0,
    gorsel_yolu TEXT NULL,
    olusturma_tarihi TEXT NOT NULL,
    aktif_mi INTEGER NOT NULL DEFAULT 1,
    snmp_aktif_mi INTEGER NOT NULL DEFAULT 1,
    snmp_community TEXT NOT NULL DEFAULT 'public',
    snmp_version TEXT NOT NULL DEFAULT '2c',
    son_basarili_iletisim TEXT NULL
);

CREATE TABLE IF NOT EXISTS yazici_sayac_olcumleri (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    yazici_id INTEGER NOT NULL,
    toplam_sayac INTEGER NULL,
    gunluk_baski INTEGER NULL,
    olcum_tarihi TEXT NOT NULL,
    olcum_zamani TEXT NOT NULL,
    olcum_basarili_mi INTEGER NOT NULL,
    hata_mesaji TEXT NULL,
    veri_kaynagi TEXT NOT NULL,
    FOREIGN KEY (yazici_id) REFERENCES yazicilar(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS toner_olcumleri (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    yazici_id INTEGER NOT NULL,
    renk TEXT NOT NULL,
    seviye_yuzde INTEGER NULL,
    olcum_zamani TEXT NOT NULL,
    olcum_basarili_mi INTEGER NOT NULL,
    hata_mesaji TEXT NULL,
    veri_kaynagi TEXT NOT NULL,
    FOREIGN KEY (yazici_id) REFERENCES yazicilar(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS drum_olcumleri (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    yazici_id INTEGER NOT NULL,
    seviye_yuzde INTEGER NULL,
    olcum_zamani TEXT NOT NULL,
    olcum_basarili_mi INTEGER NOT NULL,
    hata_mesaji TEXT NULL,
    veri_kaynagi TEXT NOT NULL,
    FOREIGN KEY (yazici_id) REFERENCES yazicilar(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS islem_loglari (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    yazici_id INTEGER NULL,
    ip_adresi TEXT NULL,
    islem_tipi TEXT NOT NULL,
    aciklama TEXT NOT NULL,
    tarih TEXT NOT NULL,
    basarili_mi INTEGER NOT NULL,
    hata_mesaji TEXT NULL
);

CREATE INDEX IF NOT EXISTS ix_sayac_yazici_zaman ON yazici_sayac_olcumleri(yazici_id, olcum_zamani DESC);
CREATE INDEX IF NOT EXISTS ix_toner_yazici_renk_zaman ON toner_olcumleri(yazici_id, renk, olcum_zamani DESC);
CREATE INDEX IF NOT EXISTS ix_drum_yazici_zaman ON drum_olcumleri(yazici_id, olcum_zamani DESC);
CREATE INDEX IF NOT EXISTS ix_islem_tarih ON islem_loglari(tarih DESC);

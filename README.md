# PrinterTrackingApp

Windows Forms tabanlı, SNMP ile ağ yazıcılarının sayaç ve sarf durumlarını takip etmek amacıyla geliştirilmiş yazıcı takip ve yönetim uygulamasıdır.

## Proje Hakkında

PrinterTrackingApp; kurum içerisindeki ağ yazıcılarının tek bir masaüstü uygulaması üzerinden kaydedilmesi, izlenmesi ve ölçüm bilgilerinin saklanması amacıyla geliştirilmiştir.

Uygulama ile yazıcıların temel bilgileri yönetilebilir, SNMP üzerinden desteklenen sayaç ve sarf bilgileri okunabilir, ölçüm geçmişi incelenebilir ve çok sayıda yazıcı için toplu işlemler gerçekleştirilebilir.

Proje C# ve .NET 8 kullanılarak Windows Forms üzerinde geliştirilmiştir. Veritabanı olarak SQLite kullanılmaktadır.

## Özellikler

* Yazıcı ekleme, silme ve güncelleme
* IPv4 adresi doğrulama ve aynı IP adresinin tekrar eklenmesini engelleme
* Aktif/pasif yazıcı yönetimi
* SNMP v1 ve v2c desteği
* SNMP üzerinden toplam baskı sayacı okuma
* Desteklenen cihazlarda toner seviyelerini okuma
* Desteklenen cihazlarda drum bilgisini değerlendirme
* Günlük baskı miktarını hesaplama
* Tekli ve toplu yazıcı ölçümü
* Belirlenen aralıklarla otomatik ölçüm
* Sayaç, toner ve drum ölçüm geçmişi
* İşlem logları
* Dashboard ve kritik sarf uyarıları
* CSV dosyasından toplu yazıcı ekleme
* SQLite veritabanı yedekleme
* Gerçek SNMP ve test amaçlı Fake Monitor altyapısı

## Kullanılan Teknolojiler

* C#
* .NET 8
* Windows Forms
* SQLite
* Microsoft.Data.Sqlite
* SNMP
* Lextm.SharpSnmpLib
* Printer-MIB

## Proje Mimarisi

Proje katmanlı mimari kullanılarak dört temel projeye ayrılmıştır:

```text
PrinterTrackingApp
|
+-- PrinterTrackingApp.Core
|   +-- DTOs
|   +-- Entities
|   +-- Interfaces
|
+-- PrinterTrackingApp.Business
|   +-- Services
|
+-- PrinterTrackingApp.DataAccess
|   +-- Context
|   +-- Repositories
|   +-- SQL
|
+-- PrinterTrackingApp.UI
|   +-- Forms
|
+-- PrinterTrackingApp.sln
+-- YAZICI\_LISTESI\_ORNEK.csv
```

### Core

Uygulamada kullanılan entity, DTO ve interface tanımlarını içerir.

### Business

Uygulamanın iş kurallarını ve servislerini içerir. SNMP sorguları, ölçüm işlemleri, toplu ölçüm, CSV aktarımı, dashboard ve yedekleme gibi işlemler bu katmanda yönetilir.

### DataAccess

SQLite veritabanı bağlantısı, repository sınıfları ve veritabanı işlemlerini içerir.

### UI

Windows Forms tabanlı kullanıcı arayüzünü içerir.

## SNMP Yapısı

Yazıcılarla haberleşmek için SNMP kullanılmaktadır. Standart cihaz bilgilerinin okunmasında Printer-MIB OID'lerinden yararlanılır.

Kullanılan temel alanlardan bazıları:

```text
sysDescr
1.3.6.1.2.1.1.1.0

prtMarkerLifeCount
1.3.6.1.2.1.43.10.2.1.4.1.\*

Supply Description
1.3.6.1.2.1.43.11.1.1.6.1.\*

Supply Max Capacity
1.3.6.1.2.1.43.11.1.1.8.1.\*

Supply Level
1.3.6.1.2.1.43.11.1.1.9.1.\*
```

SNMP cihazlarında üretici ve modele bağlı olarak desteklenen OID'ler ve döndürülen değerlerin anlamı değişebilir. Bu nedenle uygulama geçerli maksimum kapasite bilgisi bulunmayan toner veya drum değerlerini tahmini yüzde olarak göstermez.

## Gerçek Cihaz Testi

SNMP altyapısı SHARP MX-B450W cihazı üzerinde gerçek ağ ortamında test edilmiştir.

Test sırasında uygulamanın SNMP üzerinden aldığı toplam sayaç değeri cihazın web yönetim ekranındaki değerle karşılaştırılmış ve eşleştiği doğrulanmıştır. Ek baskı işlemlerinden sonra sayaç artışı da tekrar kontrol edilmiştir.

Farklı üreticiler üzerinde yapılan testlerde standart Printer-MIB değerlerinin her cihazda aynı anlamı taşımayabileceği gözlemlenmiştir. Bu nedenle üretici ve cihaz özelliklerine bağlı farklılıklar dikkate alınmaktadır.

## SQLite Veritabanı

Projenin ilk geliştirme aşamasında SQL Server kullanılmış, uygulamanın tek bilgisayarda lokal olarak çalışacağı kullanım senaryosu netleştikten sonra dağıtımı kolaylaştırmak amacıyla SQLite'a geçilmiştir.

Veritabanı varsayılan olarak kullanıcının Local Application Data dizininde oluşturulur:

```text
%LOCALAPPDATA%\\YaziciTakipSistemi\\YaziciTakip.db
```

Gerçek veritabanı dosyaları Git repository içerisine dahil edilmez.

Uygulama içerisindeki yedekleme özelliği ile SQLite veritabanının tarih ve saat bilgisi içeren bir kopyası oluşturulabilir.

## CSV ile Toplu Yazıcı Ekleme

Çok sayıda yazıcının tek tek girilmesini önlemek amacıyla CSV üzerinden toplu ekleme desteği bulunmaktadır.

Repository içerisinde örnek dosya olarak `YAZICI\_LISTESI\_ORNEK.csv` bulunmaktadır.

Örnek yapı:

```csv
IP;Konum;Community;Version
172.16.63.123;Arşiv;public;2c
10.41.10.86;Bilgi İşlem;public;2c
```

Gerçek kurum ağında kullanılacak IP adresleri ve SNMP ayarları ilgili cihazlara göre düzenlenmelidir.

## Çalıştırma

Projeyi geliştirme ortamında çalıştırmak için:

1. Repository'yi klonlayın.
2. `PrinterTrackingApp.sln` dosyasını Visual Studio ile açın.
3. NuGet paketlerinin geri yüklenmesini bekleyin.
4. Başlangıç projesi olarak `PrinterTrackingApp.UI` projesini seçin.
5. Uygulamayı çalıştırın.

Gerçek SNMP ölçümü yapılabilmesi için bilgisayarın hedef yazıcıya ağ üzerinden erişebilmesi ve yazıcı üzerinde SNMP'nin etkin olması gerekir.

## Notlar

* SNMP community değeri cihaz ayarlarıyla aynı olmalıdır.
* Güvenlik duvarı veya ağ yapılandırması SNMP erişimini engelleyebilir.
* Her yazıcı modeli toner ve drum yüzdesini standart Printer-MIB üzerinden sunmayabilir.
* Geçerli kapasite bilgisi alınamayan sarf değerleri tahmini olarak gösterilmez.
* `bin`, `obj`, `.vs`, publish çıktıları, SQLite veritabanları ve yedek dosyaları repository dışında tutulmalıdır.

## Amaç

Bu proje, kurum içerisindeki ağ yazıcılarının yönetim ve takip işlemlerini tek bir masaüstü uygulamasında birleştirmek ve SNMP üzerinden alınabilen cihaz bilgilerinin geçmişe yönelik olarak izlenmesini sağlamak amacıyla geliştirilmiştir.


# Sorgu Analizi ve Sorgu Optimizasyonu

Bu proje, bir şirketin çalışanlarını, unvanlarını, birimlerini ve ikramiyelerini yöneten bir veritabanı tasarlamak amacıyla oluşturulmuştur. Veritabanı, çalışanların hangi birimlerde çalıştığını, unvanlarını, maaşlarını ve aldıkları ikramiyeleri kaydetmek için tasarlanmıştır. Ödev kapsamında çeşitli sorgular bu veritabanı üzerinden işlenecektir.

## İçindekiler
- [Genel Bilgi](#genel-bilgi)
- [Kurulum](#kurulum)
- [Tablo Yapısı](#tablo-yapısı)
- [Veri Ekleme ve Silme](#veri-ekleme-ve-silme)

---
## Genel Bilgi
Bu proje, bir şirketin çalışanları, birimleri, unvanları ve ikramiyeleri ile ilgili bilgileri yönetmek için tasarlanmış bir veritabanıdır. Proje kapsamında çalışanların maaş bilgileri, unvan değişiklikleri ve aldıkları ikramiyeler ilişkilendirilerek bir veritabanı yapısı oluşturulmuştur.

---
## Kurulum
1. **Microsoft SQL Server** ve **SQL Server Management Studio (SSMS)** kurulu olmalıdır.
2. Veritabanını oluşturmak için [db.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/db.sql) dosyasındaki SQL komutlarını çalıştırabilirsiniz.
3. Gerekli tabloları oluşturmak için [tables.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/tables.sql) dosyasındaki SQL komutlarını çalıştırabilirsiniz.
---
## Tablo Yapısı
Veritabanı şu temel tabloları içermektedir:

- **Birimler Tablosu (birimler)**
- **Çalışanlar Tablosu (calisanlar)**
- **İkramiye Tablosu (ikramiye)**
- **Unvan Tablosu (unvan)**

---
## Veri Ekleme ve Silme
Tablolar için test verileri eklemek için [data.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/data.sql) dosyasına bakabilirsiniz.

**Veri Ekleme**
```sql
INSERT INTO birimler (birim_id, birim_ad) VALUES (1, 'Yazılım'), (2, 'Donanım'), (3, 'Güvenlik');
INSERT INTO calisanlar (calisan_id, ad, soyad, maas, katilmaTarihi, calisan_birim_id) VALUES (1, 'Ali', 'Yılmaz', 100000, '2014-02-02', 1);
```

**Veri Silme**
```sql
DELETE FROM calisanlar WHERE calisan_id = 1;
```
---
## Ödev Kapsamında İstenilen Sorgular
- [query3.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query3.sql) “Yazılım” veya “Donanım” birimlerinde çalışanların ad, soyad ve maaş bilgilerini listeleyen SQL sorgusunu yazınız. (Tek bir sorgu ile) 
- [query4.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query4.sql) Maaşı en yüksek olan çalışanların ad, soyad ve maaş bilgilerini listeleyen SQL sorgusunu yazınız. (Tek bir sorgu ile) 
- [query5.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query5.sql) Birimlerin her birinde kaç adet çalışan olduğunu ve birimlerin isimlerini listeleyen sorguyu yazınız. (Tek bir sorgu ile) (Örneğin; x biriminde 3 
çalışan var gibi düşünebilirsiniz.) (Gruplamalı sorgu ile) 
- [query6.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query6.sql) Birden fazla çalışana ait olan unvanların isimlerini ve o unvan altında çalışanların sayısını listeleyen sorguyu yazınız. (Tek bir sorgu ile) 
- [query7.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query7.sql) Maaşları “50000” ve “100000” arasında değişen çalışanların ad, soyad ve maaş bilgilerini listeleyen sorguyu yazınız. (Tek bir sorgu ile) 
- [query8.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query8.sql) İkramiye hakkına sahip çalışanlara ait ad, soyad, birim, unvan ve ikramiye ücreti bilgilerini listeleyen sorguyu yazınız. (Tek bir sorgu ile) 
- [query9.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query9.sql) Ünvanı “Yönetici” ve “Müdür” olan çalışanların ad, soyad ve ünvanlarını listeleyen sorguyu yazınız. (Tek bir sorgu ile) 
- [query10.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-3/query10.sql) Her bir birimde en yüksek maaş alan çalışanların ad, soyad ve maaş bilgilerini listeleyen sorguyu yazınız. (Tek bir sorgu ile)

---
Bu proje, çalışan yönetimini kolaylaştırmak için temel bir veritabanı yapısı sunmaktadır. Daha fazla analiz ve raporlama için genişletilebilir bir yapıya sahiptir.

**TODO**

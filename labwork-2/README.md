# Er Diyagramları ve Normalizasyon Kuralları

Bu proje, oyun ligi (temsili olarak NHL) için basit bir veritabanı oluşturarak, takımlar, oyuncular, maçlar ve hakemler arasındaki ilişkileri yönetmeyi amaçlamaktadır. Veritabanı, takımların oyuncularını, maç sonuçlarını, oyuncu performanslarını ve hakem atamalarını kaydetmek için tasarlanmıştır.


## İçindekiler
- [Genel Bilgi](#genel-bilgi)
- [Kurulum](#kurulum)
- [Tablo Yapısı](#tablo-yapısı)
- [Veri Ekleme ve Silme](#veri-ekleme-ve-silme)
- [ER Diyagramı](#er-diyagramı)
- [Normalizasyon](#normalizasyon)

## Genel Bilgi
Bu proje, NHL takımları ve oyuncularının verilerini yönetmek ve analiz etmek için tasarlanmış bir veritabanı sistemidir. Proje kapsamında, takımlar, oyuncular, maçlar ve hakemler arasındaki ilişkiler basit bir şekilde modellenmiştir.

## Kurulum
1. Microsoft SQL Server ve SQL Server Management Studio (SSMS) kurulu olmalıdır.
2. NHL veritabanı oluşturulmalıdır. Bunun için [db.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/db.sql) dosyasına bakabilirsiniz.
3. Gerekli tabloların oluşturulması için [tables.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/tables.sql) dosyasına bakabilirsiniz.

## Tablo Yapısı
Veritabanı şu tabloları içermektedir:
- **Teams** (Takımlar)
- **Players** (Oyuncular)
- **Matches** (Maçlar)
- **Referees** (Hakemler)
- **Participation** (Oyuncu Katılımı)

Detaylı tablo yapısı ve SQL komutları için [tables.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/tables.sql) dosyasına göz atabilirsiniz.

## Veri Ekleme ve Silme
Tablolar için test verileri eklemek için [data.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/data.sql) dosyasına bakabilirsiniz.

Veri eklemek için örnek:
```sql
INSERT INTO Players (PlayerID, TeamID, Name, BirthDate, StartYear, JerseyNumber) VALUES (1, 1, 'Chris Kreider', '1991-04-30', 2012, 20);
```

Veri silmek için örnek:
```sql
DELETE FROM Players WHERE PlayerID = 1;
```

## ER Diyagramı
Veritabanının ER diyagramını oluşturmak için Microsoft SQL Server Management Studio (SSMS) üzerindeki **Database Diagram** aracını kullanabilirsiniz.

## Normalizasyon
Noramlizasyon yapılmamış bir kayıt tablosu **1NF, 2NF, 3NF ve 4NF** aşamalarına uygun şekilde normalleştirilerek veri tekrarları ortadan kaldırılmıştır. 

---

Bu proje, NHL için temel veritabanı tasarımını kapsamaktadır ve genişletilebilir bir yapıya sahiptir. Yeni veriler eklenerek analiz süreçleri geliştirilebilir. 

**TODO**

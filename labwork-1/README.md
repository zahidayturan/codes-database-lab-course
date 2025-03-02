# Microsoft SQL Server Veritabanı Yönetimi ve Yedekleme İşlemleri

Bu proje, SQL Server kullanarak bir veritabanı oluşturma, tablolar ekleme, veri ekleme, kullanıcı yetkilendirme, yedekleme ve geri yükleme gibi temel veritabanı yönetim işlemlerini kapsamaktadır.

## İçindekiler
- [Genel Bilgi](#genel-bilgi)
- [Kurulum](#kurulum)
- [Tablo Yapısı](#tablo-yap%C4%B1s%C4%B1)
- [Kullanıcı Yetkilendirme](#kullan%C4%B1c%C4%B1-yetkilendirme)
- [Veri Ekleme ve Silme](#veri-ekleme-ve-silme)
- [Veritabanı Yedekleme ve Geri Yükleme](#veritaban%C4%B1-yedekleme-ve-geri-y%C3%BCkleme)
- [Otomatik Yedekleme Planlama](#otomatik-yedekleme-planlama)

## Genel Bilgi
Bu proje, **Föy1** isimli bir veritabanı oluşturarak içinde **Employees, Departments, Products, Orders ve Customers** tablolarını barındıran bir yapıyı test etmek amacıyla geliştirilmiştir. Veritabanının güvenliğini sağlamak için yetkilendirme ve verileri koruma adımları uygulanmıştır.

## Kurulum
1. SQL Server ve SQL Server Management Studio (SSMS) yüklenmelidir. (Tercihen  Microsoft SQL Server 2022 Developer Version)
2. `Föy1` isimli veritabanı oluşturulmalıdır. Veri tabanı yolunu vermelisiniz. [db.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/db.sql) dosyasına göz atın.
3. Gerekli tablolar komutlar ile eklenmelidir. [table.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/table.sql) dosyasına göz atın.


## Kullanıcı Yetkilendirme
Belirli kullanıcılara ekleme, silme ve güncelleme yetkisi verilmektedir. [auth.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/auth.sql) dosyasına göz atın.


## Veri Ekleme ve Silme
Oluşturulan tablolara bazı veriler eklemek için [data.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/data.sql) dosyasına göz atın.

Kullanıcı izinleri ile  veriler eklenebilir. [crudWithUser.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/crudWithUser.sql) dosyasına göz atın.

Silme için aşağıda ki kod gibi benzer yapılar oluşturulabilir:
```sql
DELETE FROM Employees WHERE EmployeeID = 1;
```

## Veritabanı Yedekleme ve Geri Yükleme
**Yedekleme:** [backUp.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/backUp.sql) dosyasına göz atın.

**Geri Yükleme:** [restore.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-1/restore.sql) dosyasına göz atın.


## Otomatik Yedekleme Planlama
Aylık ve günlük yedekleme için SQL Server Agent kullanılarak **her ayın 15'i** ve **her gün 10:00**'da yedekleme işlemi planlanmıştır.

---



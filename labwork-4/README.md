# Kısıtlayıcılar, Görünüm ve İndeksler

Bu çalışma, veritabanı tasarımı ve sorgulama süreçlerinde çeşitli kısıtlayıcılar, görünümler (views) ve indeksler gibi önemli konuları kapsamaktadır. Ödevde oluşturulan tablolar, client_master, product_master, salesman_master, sales_order ve sales_order_details olmak üzere beş ana tablodan oluşmaktadır. Her bir tablo, sistemdeki müşteri, ürün, satış elemanı ve sipariş verilerini tutmaktadır ve bu tablolarda foreign key kısıtlamaları kullanılarak veri tutarlılığı sağlanmıştır.

## İçindekiler
- [Genel Bilgi](#genel-bilgi)
- [Kurulum](#kurulum)
- [Tablo Yapısı](#tablo-yapısı)
- [Veri Ekleme ve Silme](#veri-ekleme-ve-silme)

---
## Genel Bilgi
Bu çalışma, MSSQL veritabanı yönetim sisteminde temel veritabanı tasarımı ve sorgulama tekniklerini öğrenmek amacıyla hazırlanmıştır. Ödevde, çeşitli tabloların oluşturulması, kısıtlayıcıların ve ilişkilerin tanımlanması, görünümlerin (views) kullanılması ve indekslerin optimizasyon amacıyla nasıl yapılandırılacağı gibi konular ele alınmaktadır. Amaç, veri tutarlılığını sağlamak, sorgu performansını artırmak ve genel veritabanı yönetimi süreçlerini daha verimli hale getirmektir. Bu kapsamda, SQL dilinde yapılan işlemlerle veritabanı üzerinde kapsamlı bir düzenleme gerçekleştirilmiş ve temel veritabanı yönetimi teknikleri uygulanmıştır.

---
## Kurulum
1. **Microsoft SQL Server** ve **SQL Server Management Studio (SSMS)** kurulu olmalıdır.
2. Veritabanını oluşturmak için [db.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/db.sql) dosyasındaki SQL komutlarını çalıştırabilirsiniz.
3. Gerekli tabloları oluşturmak için [tables.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/tables.sql) dosyasındaki SQL komutlarını çalıştırabilirsiniz.
---
## Tablo Yapısı
Veritabanı şu temel tabloları içermektedir:

- **client_master Tablosu**
- **Product_master Tablosu**
- **Salesman_Master Tablosu**
- **Sales_Order Tablosu**
- **Sales_Order_Details Tablosu**

---
## Veri Ekleme ve Silme
Tablolar için test verileri eklemek için [data.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-2/data.sql) dosyasına bakabilirsiniz.

---
## Ödev Kapsamında İstenilen Sorgular
- [query1.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query1.sql) salesman_master tablosu üzerinde tgt_to_get değeri 200 den büyük olanlar için bir view oluşturunuz.
- [query2.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query2.sql) product_master tablosu üzerinde product_view isminde bir view oluşturunuz ve sütun isimlerini pro_no, desc, profit, Unit_measure, qty olacak şekilde sırasıyla değiştiriniz. istenmektedir
- [query3.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query3.sql) product_view  isimli view’den Qty_on_hand değeri ‘10’ olan product isimlerini  getiren bir sorgu yazınız
- [query4.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query4.sql) sipariş tarihi 10 gün geçen siparişleri müşteri isimleri ve ürün isimleri olarak listeleyen bir SQL sorgusu yazınız
- [query5.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query5.sql) sales_order tablosunu kullanarak günlük siparişleri listelemeye yarayan bir view  oluşturunuz
- [query6.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query6.sql) Her hangi bir tablo oluşturunuz. Oluşturmuş olduğunuz tabloda anlatılan  kısıtlayıcılardan en az iki kullanıma örnek vererek gösteriniz 
- [query7.sql](https://github.com/zahidayturan/codes-database-lab-course/blob/main/labwork-4/query7.sql) Her hangi bir tablo oluşturunuz. Oluşturmuş olduğunuz tablo için indeks yapısı kullanarak en az iki sorgu çalıştırıp gösteriniz.

---
Bu proje, müşteri, sipariş ve ürün yönetimini kolaylaştırmak için temel bir veritabanı yapısı sunmaktadır. Daha fazla analiz ve raporlama için genişletilebilir bir yapıya sahiptir.

**TODO**

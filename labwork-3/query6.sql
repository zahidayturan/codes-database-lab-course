USE Foy3
SELECT unvan_calisan, COUNT(unvan_calisan_id) AS calisan_sayisi
FROM unvan
GROUP BY unvan_calisan
HAVING COUNT(unvan_calisan_id) > 1;
-- Birden fazla çalışana ait olan unvanların isimlerini ve o unvan altında çalışanların sayısı
USE Foy3
SELECT b.birim_ad, COUNT(c.calisan_id) AS calisan_sayisi
FROM birimler b
LEFT JOIN calisanlar c ON b.birim_id = c.calisan_birim_id
GROUP BY b.birim_ad;
--  Birimlerin her birinde kaç adet çalışan olduğunu ve birimlerin isimleri
USE Foy3
SELECT c.ad, c.soyad, c.maas
FROM calisanlar c
JOIN birimler b ON c.calisan_birim_id = b.birim_id
WHERE b.birim_ad IN ('Yazılım', 'Donanım');
-- “Yazılım” veya “Donanım” birimlerinde çalışanların ad, soyad ve maaş bilgileri
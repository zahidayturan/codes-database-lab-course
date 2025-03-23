USE Foy3
SELECT c.ad, c.soyad, b.birim_ad, u.unvan_calisan, i.ikramiye_ucret
FROM calisanlar c
JOIN birimler b ON c.calisan_birim_id = b.birim_id
JOIN ikramiye i ON c.calisan_id = i.ikramiye_calisan_id
LEFT JOIN unvan u ON c.calisan_id = u.unvan_calisan_id;
--  İkramiye hakkına sahip çalışanlara ait ad, soyad, birim, unvan ve ikramiye ücreti bilgileri

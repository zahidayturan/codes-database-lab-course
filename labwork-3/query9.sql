USE Foy3
SELECT c.ad, c.soyad, u.unvan_calisan
FROM calisanlar c
JOIN unvan u ON c.calisan_id = u.unvan_calisan_id
WHERE u.unvan_calisan IN ('Yönetici', 'Müdür');
--  Ünvanı “Yönetici” ve “Müdür” olan çalışanların ad, soyad ve ünvanları
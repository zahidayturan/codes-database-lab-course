package app.test;

import com.hazelcast.client.HazelcastClient;
import com.hazelcast.client.config.ClientConfig;
import com.hazelcast.client.config.ClientNetworkConfig;
import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.core.IMap;
import app.model.Student;

public class HazelcastTest {

    static HazelcastInstance hz;
    static IMap<String, Student> map;

    public static void main(String[] args) {
        try {
            // Hazelcast client konfigürasyonu
            ClientConfig clientConfig = new ClientConfig();

            // Docker'da çalışan Hazelcast'e bağlanacak şekilde network ayarı yapılıyor
            ClientNetworkConfig networkConfig = clientConfig.getNetworkConfig();
            networkConfig.addAddress("localhost:5701"); // Docker host'u üzerinden Hazelcast'e bağlan

            // Hazelcast client başlatılıyor
            hz = HazelcastClient.newHazelcastClient(clientConfig);

            // "ogrenciler" isminde bir map oluşturuluyor
            map = hz.getMap("ogrenciler");

            for (int i = 0; i < 10000; i++) {
                String id = "2025" + String.format("%06d", i);
                Student s = new Student(id, "Ad Soyad " + i, "Bilgisayar");
                map.put(id, s);
            }

            // Örnek bir veri al
            Student student = map.get("2025000001");

            System.out.println("Öğrenci No: " + student.getOgrenciNo());
            System.out.println("Ad Soyad: " + student.getAdSoyad());
            System.out.println("Bölüm: " + student.getBolum());

        } catch (Exception e) {
            System.err.println("Bağlantı hatası: " + e.getMessage());
            e.printStackTrace(); // Daha ayrıntılı hata bilgisi için
        } finally {
            if (hz != null) {
                hz.shutdown(); // Bağlantıyı kapat
            }
        }
    }
}
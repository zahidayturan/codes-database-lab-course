package app.test;

import app.model.Student;
import com.google.gson.Gson;
import redis.clients.jedis.Jedis;

public class RedisTest {
    public static void main(String[] args) {
        // Redis'e bağlan
        Jedis jedis = new Jedis("localhost", 6379);  // Docker kullanıyorsanız, container adını veya IP'yi yazabilirsiniz.

        // JSON formatında veri çek
        String studentJson = jedis.get("2025000001");

        // Eğer veri varsa
        if (studentJson != null) {
            // Gson ile JSON'u Student objesine dönüştür
            Gson gson = new Gson();
            Student student = gson.fromJson(studentJson, Student.class);

            // Öğrenci bilgilerini yazdır
            System.out.println("Öğrenci No: " + student.getOgrenciNo());
            System.out.println("Ad Soyad: " + student.getAdSoyad());
            System.out.println("Bölüm: " + student.getBolum());
        } else {
            System.out.println("Öğrenci bulunamadı.");
        }

        // Bağlantıyı kapat
        jedis.close();
    }
}

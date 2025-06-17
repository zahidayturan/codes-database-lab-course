package app.test;

import app.model.Student;
import com.google.gson.Gson;
import com.mongodb.client.MongoClient;
import com.mongodb.client.MongoClients;
import com.mongodb.client.MongoCollection;
import com.mongodb.client.MongoDatabase;
import org.bson.Document;

public class MongoTest {
    static MongoClient client;
    static MongoCollection<Document> collection;
    static Gson gson = new Gson();

    public static void main(String[] args) {
        try {
            // MongoDB'ye bağlan
            client = MongoClients.create("mongodb://localhost:27017");
            MongoDatabase database = client.getDatabase("nosqllab");
            collection = database.getCollection("ogrenciler");

            // Öğrenci verisini al
            Document doc = collection.find(new Document("ogrenciNo", "2025000001")).first();

            // Eğer veri varsa
            if (doc != null) {
                // Gson ile JSON'u Student objesine dönüştür
                Student student = gson.fromJson(doc.toJson(), Student.class);

                // Öğrenci bilgilerini yazdır
                System.out.println("Öğrenci No: " + student.getOgrenciNo());
                System.out.println("Ad Soyad: " + student.getAdSoyad());
                System.out.println("Bölüm: " + student.getBolum());
            } else {
                System.out.println("Öğrenci bulunamadı.");
            }
        } catch (Exception e) {
            System.out.println("Bağlantı hatası: " + e.getMessage());
        } finally {
            if (client != null) {
                client.close();  // Bağlantıyı kapat
            }
        }
    }
}

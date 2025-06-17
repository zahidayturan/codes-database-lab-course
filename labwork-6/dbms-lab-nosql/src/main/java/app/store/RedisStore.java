package app.store;

import redis.clients.jedis.Jedis;
import app.model.Student;
import com.google.gson.Gson;

public class RedisStore {
    private static Jedis jedis;
    private static Gson gson = new Gson();

    public static void init() {
        jedis = new Jedis("localhost", 6379);
        if (jedis.dbSize() == 0) {
            for (int i = 0; i < 10000; i++) {
                String id = "2025" + String.format("%06d", i);
                Student s = new Student(id, "Ad Soyad " + i, "Bilgisayar");
                jedis.set(id, gson.toJson(s));
            }
        }
    }
    public static Student get(String id) {
        try {
            String json = jedis.get(id);
            if (json != null) {
                return gson.fromJson(json, Student.class);
            } else {
                return null;
            }
        } catch (Exception e) {
            System.out.println("Redis hatası: " + e.getMessage());
            return null;
        }
    }

}

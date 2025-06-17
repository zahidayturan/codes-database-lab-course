package app.store;

import com.hazelcast.client.HazelcastClient;
import com.hazelcast.client.config.ClientConfig;
import com.hazelcast.core.HazelcastInstance;
import com.hazelcast.core.IMap;
import app.model.Student;
import com.hazelcast.client.config.ClientNetworkConfig;

public class HazelcastStore {
    static HazelcastInstance hz;
    static IMap<String, Student> map;

    public static void init() {

        ClientConfig clientConfig = new ClientConfig();
        ClientNetworkConfig networkConfig = clientConfig.getNetworkConfig();
        networkConfig.addAddress("localhost:5701");
        hz = HazelcastClient.newHazelcastClient(clientConfig);
        map = hz.getMap("ogrenciler");
        if(map.isEmpty()) {
            for (int i = 0; i < 10000; i++) {
                String id = "2025" + String.format("%06d", i);
                Student s = new Student(id, "Ad Soyad " + i, "Bilgisayar");
                map.put(id, s);
            }
        }

    }
    public static Student get(String id) {
        return map.get(id);
    }
}

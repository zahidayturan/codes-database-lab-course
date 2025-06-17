package app;
import static spark.Spark.*;

import app.store.HazelcastStore;
import app.store.MongoStore;
import com.google.gson.Gson;
import app.store.RedisStore;

public class Main {
    public static void main(String[] args) {
        port(8080);
        Gson gson = new Gson();

        RedisStore.init();
        MongoStore.init();
        HazelcastStore.init();

        get("/hello", (req, res) -> "Hello, World!");

        get("/hello/:name", (request, response) -> {
            String name = request.params(":name");
            return "{\"message\": \"Merhaba, " + name + "!\"}";
        });

        // Endpoint: Redis'ten öğrenci bilgilerini al
        get("/nosql-lab-rd/student_no/:id", (req, res) ->
                gson.toJson(RedisStore.get(req.params(":id"))));

        get("/nosql-lab-mon/student_no/:id", (req, res) ->
                gson.toJson(MongoStore.get(req.params(":id"))));

        get("/nosql-lab-hz/student_no/:id", (req, res) ->
                gson.toJson(HazelcastStore.get(req.params(":id"))));
    }
}

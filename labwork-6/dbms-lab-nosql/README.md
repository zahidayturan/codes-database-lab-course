NoSQL Deney Sonu Mini Proje
=================================

### NoSQL deneyinde kullanılan Redis, Hazelcast ve MongoDB teknolojileri kullanarak 3 farklı endpoint ile hizmet veren bir servis tasarlanması beklenmektedir. Her 3 teknolojide de 10.000 kayıtlık bir veri tabanı girişi yapılması gerekmektedir.

# Docker Compose Kurulumu: Hazelcast, Redis, ve MongoDB

Bu proje, Hazelcast, Redis ve MongoDB servislerini Docker Compose kullanarak kolayca çalıştırmak için yapılandırılmıştır. Aşağıdaki adımları takip ederek bu servisleri bir arada çalıştırabilirsiniz.

**Gereksinimler**
- Docker (https://www.docker.com/get-started)
- Docker Compose (https://docs.docker.com/compose/install/)

**Kurulum Adımları**
Projeyi İndirin veya Kopyalayın:
Eğer bu dosyayı bir .zip olarak indiriyorsanız, ya da Git ile klonladıysanız, proje klasörüne gidin.

**Docker ve Docker Compose Kurulumunu Kontrol Edin:**

Docker ve Docker Compose'un düzgün çalışıp çalışmadığını kontrol etmek için aşağıdaki komutları çalıştırabilirsiniz:
```bash
docker --version
docker-compose --version
```

**Docker Compose'u Başlatın:**

Docker Compose dosyasını çalıştırarak servisleri başlatabilirsiniz. Bu dosya proje ana dizininde bulunmaktadır. Docker terminal üzerinden proje ana dizinine gidiniz:
```bash
docker-compose up -d
```

Servislerin çalışıp çalışmadığını kontrol etmek için:
```bash
docker-compose ps
```

**Servislere Erişim:**

- Hazelcast: 5701 portundan erişilebilir.
- Redis: 6379 portundan erişilebilir.
- MongoDB: 27017 portundan erişilebilir.

**Redis'e bağlanmak için:**
```bash
redis-cli -h localhost -p 6379
```
**MongoDB'ye bağlanmak için ise:**
```bash
mongo --host localhost --port 27017
```
# Proje İlave Bilgileri

Tutulacak kayıt örneği:

```json,
 "student_no" : "111111",
 "name" : "yüzüğü neden kartallarla götürmediler",
 "department" : "orta dünya ork dili ve edebiyatı"
```

Uygulamanın başlangıç aşamasında 10.000 adet kaydı, veritabanlarına rastgele oluşturup ekletebilirsiniz. Veya anlamlı bir kayıt girişi de yapabilirsiniz.

1. endpoint doğrudan redis-ten alıp getirmelidir.
2. endpoint doğrudan hazelcast-ten alıp getirmelidir.
3. endpoint doğrudan mongodb-den alıp getirmelidir.

`URL1: localhost:8080/nosql-lab-rd/student_no/2025000001`\
`URL2: localhost:8080/nosql-lab-hz/student_no/2025000001`\
`URL3: localhost:8080/nosql-lab-mon/student_no/2025000001`


Proje dizini:
```
src/
└── main/
    ├── java/
    │   └── app/
    │       ├── Main.java
    │       ├── model/Student.java
    │       ├── store/RedisStore.java
    │       ├── store/HazelcastStore.java
    │       └── store/MongoStore.java
    │       └── test/RedisTest.java
    │       └── test/HazelcastStore.java
    │       └── test/MongoTest.java
    ├── resources/
pom.xml
```

Kodladığınız endpoint-leri test etmek için siege komutunu yükleyiniz. (1000 istek, eş zamanlı 10 istemci)
`sudo apt-get install siege`
```bash
    # Redis
    siege -H "Accept: application/json" -c10 -r100 "http://localhost:8080/nosql-lab-rd/student_no/2025000001" > ~/redis-siege.results
    
    # Hazelcast
    siege -H "Accept: application/json" -c10 -r100 "http://localhost:8080/nosql-lab-hz/student_no/2025000001" > ~/hz-siege.results

    # MongoDB
    siege -H "Accept: application/json" -c10 -r100 "http://localhost:8080/nosql-lab-mon/student_no/2025000001" > ~/mongodb-siege.results
```
Sonuçlar standart çıkışa değil `*.results` isimli dosyalara bastırabilir.

siege komutu parametreleri:
* H : gelen yanıt bir json verisi
* c10: 10 eş zamanlı istemci (concurrent users)
* r100: Her istemci 100 istek atsın (10x100 = toplam 1000 istek)
* "URL": Test etmek istediğin endpoint

Şablon siege çıktısı:
```
    Transactions:                   1000 hits
    Availability:                 100.00 %
    Elapsed time:                  10.34 secs
    Data transferred:               0.80 MB
    Response time:                  0.08 secs
    Transaction rate:              96.71 trans/sec
    Throughput:                     0.08 MB/sec
    Concurrency:                    7.89
    Successful transactions:        1000
    Failed transactions:               0
```
\
Koşum zamanı testi:
```bash
    time seq 1 100 | xargs -n1 -P10 -I{} curl -s "http://localhost:8080/nosql-lab-rd/student_no=2025000001" > ~/redis-time.results

    time seq 1 100 | xargs -n1 -P10 -I{} curl -s "http://localhost:8080/nosql-lab-rd/student_no=2025000001" > ~/hz-time.results

    time seq 1 100 | xargs -n1 -P10 -I{} curl -s "http://localhost:8080/nosql-lab-mon/student_no=2025000001" > ~/mongodb-time.results
```
```
    Execution time:               0.234
```
Eğer wsl ile bağlanıyorsanız ve bağlantı hatası alıyorsanız localhost yerine wsl ip değerini yazınız.


Orijinal şablon repo:
\
`git clone https://github.com/ismailhakkituran/dbms-lab-nosql.git`
\




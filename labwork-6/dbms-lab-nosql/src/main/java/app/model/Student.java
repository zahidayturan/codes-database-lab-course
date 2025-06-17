package app.model;
import java.io.Serializable;

public class Student implements Serializable {

    private static final long serialVersionUID = 1L;
    private String ogrenciNo;
    private String adSoyad;
    private String bolum;

    // Constructor
    public Student(String ogrenciNo, String adSoyad, String bolum) {
        this.ogrenciNo = ogrenciNo;
        this.adSoyad = adSoyad;
        this.bolum = bolum;
    }

    // Getter ve Setter metodları
    public String getOgrenciNo() {
        return ogrenciNo;
    }

    public String getAdSoyad() {
        return adSoyad;
    }

    public String getBolum() {
        return bolum;
    }

}

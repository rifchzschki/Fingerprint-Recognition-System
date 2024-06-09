# Tubes3_KacaprukReborn
##### Tangkapan layar dari program :)
> ![program](src/assets/tes.png) <br>
>
> ![program2](src/assets/hasil.png)

## Deskripsi Program

> Pada tugas besar kali ini kami membuat program pencocokan sidik jari dengan memanfaatkan algoritma pencocokan string seperti Knuth-Morris-Pratt (KMP) dan Boyer-Moore (BM). Dari input gambar sidik jari yang diinputkan, akan dihasilkan biodata dari pemilik sidik jari tersebut.
>
> Pada mulanya gambar akan ditransformasi menjadi sekumpulan karakter ascii. Kemudian karakter ascii tersebut akan dicocokkan dengan yang ada di database dengan menggunakan algoritma KMP ataupun BM. Setelah itu akan didapatkan nama pemilik dari sidik jari tersebut. Akan tetapi, karena untuk faktor keamanan, data biodata pemilik sidik jari tersebut memiliki nama alay. Untuk mencocokkan nama asli dengan nama alay tersebut kita dapat menggunakan pencocokan menggunakan regex
>
> Keamanan dari database juga dapat ditambah dengan fitur enkripsi-dekripsi (AES 256). Dengan mengaktifkan fitur ini, data pada biodata selain data dengan tipe enum akan dienkripsi sehingga di database hanya menyisakan ciphertext. Data akan didekripsi apabila diperlukan oleh aplikasi.

## Deskripsi Algoritma
### KMP
> Algoritma KMP adalah salah satu algoritma string matching yang efisien karena dapat melakukan pencarian dengan kompleksitas waktu O(n + m), di mana n adalah panjang teks dan m adalah panjang pola. Algoritma ini bekerja dengan menggunakan informasi yang diperoleh dari pola untuk menghindari pencocokan karakter yang tidak perlu. Secara umum, terdapat dua tahapan saat melakukan pemrosesan dengan algoritma KMP, yaitu preprocessing dan matching
>
>  - Preprocessing:<br>
>    - Membuat tabel "longest proper prefix which is also suffix" (LPS) untuk pola. Tabel ini membantu menentukan seberapa banyak pergeseran yang diperlukan ketika terjadi ketidakcocokan.
>    - Tabel LPS dibuat dengan menganalisis pattern itu sendiri dan mencatat panjang dari prefiks terpanjang yang juga merupakan sufiks untuk setiap posisi dalam pattern.
> 
>  - Matching:<br>
> Mulai pencocokan dari awal teks dan pola.
>    - Jika terjadi kecocokan karakter antara teks dan pola, teruskan ke karakter berikutnya.
>    - Jika terjadi ketidakcocokan, gunakan tabel LPS untuk menentukan pergeseran yang diperlukan tanpa harus membandingkan ulang karakter yang sudah diketahui cocok.

### BM
> Algoritma Boyer-Moore (BM) adalah salah satu algoritma string matching yang efisien dan sering digunakan karena dapat melakukan pencarian dengan kompleksitas waktu rata-rata yang sangat baik. Algoritma ini memanfaatkan informasi yang diperoleh dari pola dan teks untuk menghindari pencocokan karakter yang tidak perlu, sehingga mempercepat proses pencarian. Secara umum, terdapat dua tahapan utama dalam pemrosesan dengan algoritma BM, yaitu preprocessing dan matching.
>
>  - Preprocessing:<br>
> Pada tahap preprocessing, algoritma Boyer-Moore membangun dua tabel penting:<br>
>    - Tabel Bad Character (Karakter Buruk): Tabel ini digunakan untuk menentukan seberapa jauh pola dapat digeser ketika terjadi ketidakcocokan karakter. Untuk setiap karakter dalam alfabet, tabel ini mencatat posisi kemunculan terakhir karakter tersebut dalam pola. Jika karakter yang tidak cocok ditemukan dalam teks, pola dapat digeser ke kanan sejauh mungkin sehingga karakter yang tidak cocok tersebut berada di bawah kemunculan terakhirnya dalam pola.
>    - Tabel Good Suffix (Sufiks Baik): Tabel ini digunakan untuk menentukan pergeseran pola ketika terjadi ketidakcocokan pada bagian akhir pola. Tabel ini mencatat informasi tentang seberapa jauh pola dapat digeser ketika sebagian dari pola sudah cocok. Ini membantu dalam menghindari perbandingan ulang karakter yang sudah diketahui cocok.
>
>  - Matching:<br>
>    - Mulai Pencocokan dari Akhir Pola: Algoritma Boyer-Moore memulai pencocokan dari akhir pola dan teks, bukan dari awal seperti pada kebanyakan algoritma pencocokan lainnya. Ini memungkinkan penggunaan informasi dari tabel Bad Character dan Good Suffix secara lebih efektif.
>    - Jika Terjadi Kecocokan Karakter: Jika karakter dari pola dan teks cocok, lanjutkan pencocokan ke karakter sebelumnya (bergerak ke kiri dalam pola dan teks).
>    - Jika Terjadi Ketidakcocokan Karakter: Ketika karakter tidak cocok ditemukan, gunakan tabel Bad Character untuk menentukan seberapa jauh pola dapat digeser. Jika tabel Bad Character tidak memberikan pergeseran yang cukup besar, gunakan tabel Good Suffix untuk menentukan pergeseran yang lebih baik.

### REGEX
bentar ya aing mau ashar plus cari makan dlu hehehe
### AES 256

## Kebutuhan Program

1. Program ini dikompilasi dengan menggunakan .NET SDK. Proses instalasi .NET SDK dapat dilihat pada [link berikut](https://dotnet.microsoft.com/en-us/download).

2. Selain itu, program ini juga dijalankan dalam server sql, Anda dapat melakukan instalasi mariaDB pada [link berikut](https://mariadb.org/download/?t=mariadb&p=mariadb&r=11.4.2&os=windows&cpu=x86_64&pkg=msi&mirror=bkns)

## Cara Kompilasi

Setelah menginstall .NET SDK dan berhasil menjalankan server sql, kompilasi program dapat dilakukan dengan mengikuti langkah-langkah berikut:

1. Clone repository lalu masuk ke dalam directory nya

   ```bash
   git clone https://github.com/rifchzschki/Tubes3_KacaprukReborn

   cd Tubes3_KacaprukReborn
   ```

2. Lakukan konfigurasi database terlebih dahuhlu

   - Pastikan file database(.sql) yang anda inginkan telah berada di direktori "src/Database/"
   - Masuk ke server sql anda

   ```bash
   mysql -u {username} -p
   ```

   - Buat database baru di server sql anda lalu keluar dari server sql

   ```bash
   create database {database_name};
   quit;
   ```

   - Lakukan restore file sql ke dalam server sql anda

   ```bash
   cd src/Database/
   mysql -u {username} -p {database_name} < {external_file_name}.sql
   ```

   - Database sudah tertanam di server sql Anda!

3. Pastikan pada connection di file DatabaseHelper sudah sesuai dengan konfigurasi akun sql Anda

   ```bash
   private static string connectionString = "Server={server_name};Database={database_name};Uid={username};Pwd={password};";
   ```

4. Jika Anda ingin meningkatkan keamanan data biodata pada database, Anda dapat mengaktifkannya pada file program.cs. 
    > HANYA DIJALANKAN SEKALI SAAT PERTAMA KALI DATABASE DI RESTORE, setelah itu uncomment lagi
   ```bash
   Models.Converter.EncryptDatabase();
   ```

5. Program siap dijalankan dengan perintah berikut

   ```bash
   cd "src"
   dotnet run
   ```

6. Program sudah dapat digunakan

## Anggota

| NIM      | NAMA                         |
| -------- | ---------------------------- |
| 13522015 | Yusuf Ardian Sandi           |
| 13522032 | Tazkia Nizami                |
| 13522120 | M Rifki Virziadeili Harisman |

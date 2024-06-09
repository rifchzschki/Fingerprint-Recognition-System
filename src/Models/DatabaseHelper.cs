using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient; // Menggunakan namespace MySql.Data.MySqlClient


namespace Tubes3.Models
{
    // DatabaseHelper class to handle database operations
    public class DatabaseHelper
    {
        private static string connectionString = "Server=localhost;Database=dummy_stima;Uid=root;Pwd=Zxczxc123456;"; // String koneksi MySQL/MariaDB
        private MySqlConnection connection = new MySqlConnection(connectionString);

        public DatabaseHelper()      // Constructor to initialize the connection string (databasePath is the path to the .db file)
        {
        }

        public List<Biodata> GetBiodatas()      // Get all biodata from the database (.db file)
        {
            var biodatas = new List<Biodata>();

            connection.Open();

            string query = "SELECT * FROM biodata;";
            // Membuat objek perintah SQL
            using (MySqlCommand command = new MySqlCommand(query, connection)) // Menggunakan MySqlCommand
            {

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var biodata = new Biodata
                        {
                            Nik = reader.GetString(0),
                            Nama = reader.GetString(1),
                            Tempat_lahir = reader.GetString(2),
                            Tanggal_lahir = reader.GetDateTime(3),
                            Jenis_kelamin = reader.GetString(4),
                            Golongan_darah = reader.GetString(5),
                            Alamat = reader.GetString(6),
                            Agama = reader.GetString(7),
                            Status_perkawinan = reader.GetString(8),
                            Pekerjaan = reader.GetString(9),
                            Kewarganegaraan = reader.GetString(10)
                        };

                        biodatas.Add(biodata);
                    }
                    
                }
            }
            connection.Close();

            return biodatas;
        }

        public List<SidikJari> GetSidikJaris()      // Get all fingerprint data from the database (.db file)
        {
            var sidikJaris = new List<SidikJari>();

            connection.Open();

            string query = "SELECT * FROM sidik_jari;";
            // Membuat objek perintah SQL
            using (MySqlCommand command = new MySqlCommand(query, connection)) // Menggunakan MySqlCommand
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sidikJari = new SidikJari
                        {
                            Berkas_citra = reader.GetString(0),
                            Nama = reader.GetString(1)
                        };

                        sidikJaris.Add(sidikJari);
                    }
                }
            }
            connection.Close();

            return sidikJaris;
        }

        public Biodata? GetBiodataFromAlay(string alay){
            connection.Open();
            Biodata bio;
            string query = "SELECT * FROM biodata where nama = \'" + alay +"\';";
            // Test.TestHere(query);
            using (MySqlCommand command = new MySqlCommand(query, connection)) // Menggunakan MySqlCommand
            {
                using (MySqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        bio =  new Biodata
                        {
                            Nik = reader.GetString(0),
                            Nama = reader.GetString(1),
                            Tempat_lahir = reader.GetString(2),
                            Tanggal_lahir = reader.GetDateTime(3),
                            Jenis_kelamin = reader.GetString(4),
                            Golongan_darah = reader.GetString(5),
                            Alamat = reader.GetString(6),
                            Agama = reader.GetString(7),
                            Status_perkawinan = reader.GetString(8),
                            Pekerjaan = reader.GetString(9),
                            Kewarganegaraan = reader.GetString(10)
                        };
                        return bio;
                    }
                }
            }
            connection.Close();
            return null;
        }

        public List<string> GetNamaFromAlay(){
            connection.Open();
            List<string> alays = new List<string>();
            string query = "SELECT nama FROM biodata;";
            using (MySqlCommand command = new MySqlCommand(query, connection)) // Menggunakan MySqlCommand
            {
                using (MySqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read())
                    {
                        alays.Add(reader.GetString(0));
                    }
                }
            }
            connection.Close();
            return alays;
        }

        public void varcharToText(){
            // Mengubah VARCHAR ke TEXT di biodata
            string query1 = "ALTER TABLE biodata MODIFY NIK VARCHAR(255);";
            string query2 = "ALTER TABLE biodata MODIFY nama TEXT;";
            string query3 = "ALTER TABLE biodata MODIFY tempat_lahir TEXT;";
            string query4 = "ALTER TABLE biodata MODIFY golongan_darah TEXT;";
            string query5 = "ALTER TABLE biodata MODIFY alamat TEXT;";
            string query6 = "ALTER TABLE biodata MODIFY agama TEXT;";
            string query7 = "ALTER TABLE biodata MODIFY pekerjaan TEXT;";
            string query8 = "ALTER TABLE biodata MODIFY kewarganegaraan TEXT;";
            string query9 = "ALTER TABLE sidik_jari MODIFY nama TEXT;";
            connection.Open();
            using (MySqlCommand command = new MySqlCommand(query1, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Nik diubah menjadi varchar(255).");
            }
            using (MySqlCommand command = new MySqlCommand(query2, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Nama diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query3, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Tempat lahir diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query4, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Golongan darah diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query5, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Alamat diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query6, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Agama diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query7, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Pekerjaan diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query8, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Kewarganegaraan diubah menjadi TEXT.");
            }
            using (MySqlCommand command = new MySqlCommand(query9, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Nama sidik jari diubah menjadi TEXT.");
            }
            connection.Close();

        }

        public void UpdateBiodata(Biodata newBiodata, string oldNik)
        {
            string query = "UPDATE biodata SET " +
                        "nik = @newNik, " +
                        "nama = @nama, " +
                        "tempat_lahir = @tempat_lahir, " +
                        "tanggal_lahir = @tanggal_lahir, " +
                        "jenis_kelamin = @jenis_kelamin, " +
                        "golongan_darah = @golongan_darah, " +
                        "alamat = @alamat, " +
                        "agama = @agama, " +
                        "status_perkawinan = @status_perkawinan, " +
                        "pekerjaan = @pekerjaan, " +
                        "kewarganegaraan = @kewarganegaraan " +
                        "WHERE nik = @oldNik;";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@newNik", newBiodata.Nik);
                command.Parameters.AddWithValue("@nama", newBiodata.Nama);
                command.Parameters.AddWithValue("@tempat_lahir", newBiodata.Tempat_lahir);
                command.Parameters.AddWithValue("@tanggal_lahir", newBiodata.Tanggal_lahir);
                command.Parameters.AddWithValue("@jenis_kelamin", newBiodata.Jenis_kelamin);
                command.Parameters.AddWithValue("@golongan_darah", newBiodata.Golongan_darah);
                command.Parameters.AddWithValue("@alamat", newBiodata.Alamat);
                command.Parameters.AddWithValue("@agama", newBiodata.Agama);
                command.Parameters.AddWithValue("@status_perkawinan", newBiodata.Status_perkawinan);
                command.Parameters.AddWithValue("@pekerjaan", newBiodata.Pekerjaan);
                command.Parameters.AddWithValue("@kewarganegaraan", newBiodata.Kewarganegaraan);
                command.Parameters.AddWithValue("@oldNik", oldNik);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateSidikJari(SidikJari newSidikJari, string oldNama)
        {
            string query = "UPDATE sidik_jari SET " +
                        "berkas_citra = @citra, " +
                        "nama = @nama " +
                        "WHERE nama = @oldNama;";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@citra", newSidikJari.Berkas_citra);
                command.Parameters.AddWithValue("@nama", newSidikJari.Nama);
                command.Parameters.AddWithValue("@oldNama", oldNama);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }

    public class Biodata
    {
        public required string Nik { get; set; }        // Non nullable
        public string? Nama { get; set; }               // the rest are nullable
        public string? Tempat_lahir { get; set; }
        public DateTime? Tanggal_lahir { get; set; }
        public string? Jenis_kelamin { get; set; }
        public string? Golongan_darah { get; set; }
        public string? Alamat { get; set; }
        public string? Agama { get; set; }
        public string? Status_perkawinan { get; set; }
        public string? Pekerjaan { get; set; }
        public string? Kewarganegaraan { get; set; }

        public StringBuilder showInfo(string namaAsli){
            StringBuilder sb = new StringBuilder();
            sb.Append("NIK: "); sb.Append(Nik); sb.Append("\n");
            sb.Append("Nama: "); sb.Append(namaAsli); sb.Append("\n");
            sb.Append("Tempat, Tanggal lahir: "); sb.Append(Tempat_lahir); sb.Append(", "); sb.Append(Tanggal_lahir); sb.Append("\n");
            sb.Append("Jenis Kelamin: "); sb.Append(Jenis_kelamin); sb.Append("\n");
            sb.Append("Golongan Darah: "); sb.Append(Golongan_darah); sb.Append("\n");
            sb.Append("Alamat: "); sb.Append(Alamat); sb.Append("\n");
            sb.Append("Agama: "); sb.Append(Agama); sb.Append("\n");
            sb.Append("Status perkawinan: "); sb.Append(Status_perkawinan); sb.Append("\n");
            sb.Append("Pekerjaan: "); sb.Append(Pekerjaan); sb.Append("\n");
            sb.Append("Kewarganeraan: "); sb.Append(Kewarganegaraan); sb.Append("\n");
            return sb;
        }
    }

    public class SidikJari
    {
        public string? Berkas_citra { get; set; }       // all nullable
        public string? Nama { get; set; }
    }


}
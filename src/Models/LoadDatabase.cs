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
            Test.TestHere(query);
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

        public StringBuilder showInfo(){
            StringBuilder sb = new StringBuilder();
            sb.Append("NIK: "); sb.Append(Nik); sb.Append("\n");
            sb.Append("Nama: "); sb.Append(Nama); sb.Append("\n");
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
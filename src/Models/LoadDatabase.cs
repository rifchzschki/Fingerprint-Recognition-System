using System;
using System.Collections.Generic;
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

            return biodatas;
        }

        public List<SidikJari> GetSidikJaris()      // Get all fingerprint data from the database (.db file)
        {
            var sidikJaris = new List<SidikJari>();

        
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM sidik_jari";
            // command.CommandText = "SELECT Id, Name, Email FROM Users";

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
            

            return sidikJaris;
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
    }

    public class SidikJari
    {
        public string? Berkas_citra { get; set; }       // all nullable
        public string? Nama { get; set; }
    }


}
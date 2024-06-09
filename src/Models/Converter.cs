using System;
using System.Collections.Generic;


namespace Tubes3.Models{
    public static class Converter
    {
        static EncryptionHelper algorithm = new EncryptionHelper();
        // proses mengenkripsi biodata
        public static Biodata EncryptBiodata(Biodata biodata)
        {
            return new Biodata
            {
                Nik = Convert.ToBase64String(algorithm.encryption(biodata.Nik)),
                Nama = Convert.ToBase64String(algorithm.encryption(biodata.Nama)),
                Tempat_lahir = Convert.ToBase64String(algorithm.encryption(biodata.Tempat_lahir)),
                Tanggal_lahir = biodata.Tanggal_lahir, // Tidak dienkripsi
                Jenis_kelamin = biodata.Jenis_kelamin, // Tidak dienkripsi
                Golongan_darah = Convert.ToBase64String(algorithm.encryption(biodata.Golongan_darah)),
                Alamat = Convert.ToBase64String(algorithm.encryption(biodata.Alamat)),
                Agama = Convert.ToBase64String(algorithm.encryption(biodata.Agama)),
                Status_perkawinan = biodata.Status_perkawinan, // Tidak dienkripsi
                Pekerjaan = Convert.ToBase64String(algorithm.encryption(biodata.Pekerjaan)),
                Kewarganegaraan = Convert.ToBase64String(algorithm.encryption(biodata.Kewarganegaraan))
            };
        }

        // proses dekripsi biodata
        public static Biodata DecryptBiodata(Biodata biodata)
        {
            return new Biodata
            {
                Nik = algorithm.decryption(Convert.FromBase64String(biodata.Nik)),
                Nama = algorithm.decryption(Convert.FromBase64String(biodata.Nama)),
                Tempat_lahir = algorithm.decryption(Convert.FromBase64String(biodata.Tempat_lahir)),
                Tanggal_lahir = biodata.Tanggal_lahir, // Tidak dienkripsi
                Jenis_kelamin = biodata.Jenis_kelamin, // Tidak dienkripsi
                Golongan_darah = algorithm.decryption(Convert.FromBase64String(biodata.Golongan_darah)),
                Alamat = algorithm.decryption(Convert.FromBase64String(biodata.Alamat)),
                Agama = algorithm.decryption(Convert.FromBase64String(biodata.Agama)),
                Status_perkawinan = biodata.Status_perkawinan, // Tidak dienkripsi,
                Pekerjaan = algorithm.decryption(Convert.FromBase64String(biodata.Pekerjaan)),
                Kewarganegaraan = algorithm.decryption(Convert.FromBase64String(biodata.Kewarganegaraan))
            };
        }
        
        // proses dekripsi database
        public static void EncryptDatabase(){
            DatabaseHelper dh = new DatabaseHelper();
            List<Biodata> biodatas = dh.GetBiodatas(); 
            dh.varcharToText();
            foreach (Biodata bio in biodatas)
            {
                Biodata bioEnc = EncryptBiodata(bio);
                dh.UpdateBiodata(bioEnc, bio.Nik);
            }
        }
    }

}
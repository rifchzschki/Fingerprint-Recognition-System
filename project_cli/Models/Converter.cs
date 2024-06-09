using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Tubes3.Models{
    public static class Converter
    {
        static EncryptionHelper algorithm = new EncryptionHelper();
        public static Biodata EncryptBiodata(Biodata biodata)
        {

            // Console.WriteLine(biodata.Nik);
            // Console.WriteLine(biodata.Nama);
            // byte[] nik = algorithm.encryption(biodata.Nik);
            // byte[] nama = algorithm.encryption(biodata.Nama);
            // EncryptionHelper.PrintByteArray(nik);
            // EncryptionHelper.PrintByteArray(nama);
            // Console.WriteLine(Convert.ToBase64String(algorithm.encryption(biodata.Nik)));
            // Console.WriteLine(Convert.ToBase64String(algorithm.encryption(biodata.Nama)));
            // Console.WriteLine();
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

        public static SidikJari EncryptSidikJari(SidikJari sidikJari)
        {
            return new SidikJari
            {
                Berkas_citra = Convert.ToBase64String(algorithm.encryption(sidikJari.Berkas_citra)),
                Nama = Convert.ToBase64String(algorithm.encryption(sidikJari.Nama))
            };
        }

        public static SidikJari DecryptSidikJari(SidikJari sidikJari)
        {
            return new SidikJari
            {
                Berkas_citra = algorithm.decryption(Convert.FromBase64String(sidikJari.Berkas_citra)),
                Nama = algorithm.decryption(Convert.FromBase64String(sidikJari.Nama))
            };
        }
        

        public static void EncryptDatabase(){
            DatabaseHelper dh = new DatabaseHelper();
            List<Biodata> biodatas = dh.GetBiodatas(); 
            Test.TestHere(biodatas);
            dh.varcharToText();
            List<Biodata> biodatasEnc = new List<Biodata>(); 
            // dh.UpdateBiodata(EncryptBiodata(biodatas[0]), biodatas[0].Nik);
            foreach (Biodata bio in biodatas)
            {

                Console.WriteLine(bio.Nama);
                Biodata bioEnc = EncryptBiodata(bio);
                string namaCiphertext = Convert.ToBase64String(algorithm.encryption(bio.Nama));
                biodatasEnc.Add(bioEnc);
                Console.WriteLine(namaCiphertext);

                // string namaDec = algorithm.decryption(Convert.FromBase64String(namaCiphertext));
                // Console.WriteLine(namaDec);
                // dh.UpdateBiodata(bioEnc, bio.Nik);
            }
            List<Biodata> biodatasDecrypted = new List<Biodata>(); 
            // List<Biodata> biodatasEnc = dh.GetBiodatas(); 
            foreach (Biodata bio in biodatasEnc)
            {
                Biodata bioDec = DecryptBiodata(bio);
                biodatasDecrypted.Add(bioDec);
                Console.WriteLine(bioDec.Nama);
                Console.WriteLine(bioDec.Alamat);
                Console.WriteLine(bioDec.Agama);
                Console.WriteLine(bioDec.Pekerjaan);

            }
            string nama = "John Doe";
            Console.Writeline(ConvertAlay.findAlayMatch(alays, nama));


        }
    }

}
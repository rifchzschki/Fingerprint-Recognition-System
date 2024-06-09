using System;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Tubes3.Models{
    public class EncryptionHelper
    {
        private static AES256 machine = new AES256();

        // untuk print array byte (buat debug)
        public static void PrintByteArray(byte[] array)
        {
            foreach (byte b in array)
            {
                Console.Write(b.ToString("X2") + " ");
            }
            Console.WriteLine();
        }

        // untuk menghapus spasi di belakang karakter paling akhir
        static void TrimSpace(ref string str){
            int i=0;
            int index=0;
            while(i<str.Length){
                if(str[i] != ' '){
                    index = i;
                }
                i++;
            }
            str = str.Substring(0, index+1);
        }

        // menambah spasi setelah karakter paling akhir bila diperlukan
        static void AddSpace(ref string str, int size){
            while(str.Length<size){
                str+=" ";
            }
        }

        // proses yang menjembatani proses enkripsi
        public byte[] encryption(string inputString){
            int block_size = 16;
            AddSpace(ref inputString, ((inputString.Length/block_size)+1)*16);
            byte[] plaintext = new byte[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                plaintext[i] = (byte)inputString[i];
            }
            byte[] ciphertext = new byte[plaintext.Length]; 
            bool encryptionResult = machine.Encrypt(plaintext, ref ciphertext);
            if (encryptionResult)
            {
                return ciphertext;
            }else{
                return null;
            }
        }

        // Proses yang menjembatani proses dekripsi
        public string decryption(byte[] ciphertext){
            byte[] decryptedPlaintext = new byte[ciphertext.Length];
            bool decryptionResult = machine.Decrypt(ciphertext, ref decryptedPlaintext);
            
            if (decryptionResult)
            {
                // Tampilkan hasil dekripsi
                string res = Encoding.UTF8.GetString(decryptedPlaintext);
                TrimSpace(ref res);
                Console.WriteLine("Decrypted plaintext: " + res);
                return res;
            }
            else
            {
                return null;
            }
        }
    }


}
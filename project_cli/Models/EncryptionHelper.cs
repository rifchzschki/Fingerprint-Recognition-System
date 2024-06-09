using System;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Tubes3.Models{
    public class EncryptionHelper
    {
        private static AES1128 machine = new AES128();
        // Helper function to print byte array
        public static void PrintByteArray(byte[] array)
        {
            foreach (byte b in array)
            {
                Console.Write(b.ToString("X2") + " ");
            }
            Console.WriteLine();
        }

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

        static void AddSpace(ref string str, int size){
            while(str.Length<size){
                str+=" ";
            }
        }

        public byte[] encryption(string inputString){
            int block_size = 16;
            AddSpace(ref inputString, ((inputString.Length/block_size)+1)*16);
            byte[] plaintext = new byte[inputString.Length];
            for (int i = 0; i < inputString.Length; i++)
            {
                plaintext[i] = (byte)inputString[i];
            }
            byte[] ciphertext = new byte[plaintext.Length]; // Pastikan ukuran ciphertext cukup besar
            // PrintByteArray(ciphertext);
            bool encryptionResult = machine.Encrypt(plaintext, ref ciphertext);
            if (encryptionResult)
            {
                return ciphertext;
            }else{
                return null;
            }
        }
        public string decryption(byte[] ciphertext){
            byte[] decryptedPlaintext = new byte[ciphertext.Length]; // Pastikan ukuran plaintext cukup besar
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


        // static void Main()
        // {
        //     // string inputString = "hello word                                                                                                                                                                                                                                                      ";
        //     // byte[] plaintext = { 0x32, 0x43, 0xf6, 0xa8, 0x88, 0x5a, 0x30, 0x8d, 0x31, 0x31, 0x98, 0xa2, 0xe0, 0x37, 0x07, 0x34 };

        //     // Console.WriteLine(plaintext);

        //     // Membuat instance dari kelas yang berisi metode Encrypt dan Decrypt (gantilah dengan nama kelas yang sesuai)
        //     // Misal: MyClass aes = new MyClass();

        //     // Panggil fungsi Encrypt
            
        //     if (encryptionResult)
        //     {
        //         // Tampilkan hasil enkripsi
        //         Console.WriteLine("Plaintext: " + Encoding.UTF8.GetString(plaintext));
        //         // Console.WriteLine("Ciphertext (hex): " + BitConverter.ToString(ciphertext).Replace("-", ""));
        //         Console.WriteLine("Ciphertext (str): ");
        //         Console.WriteLine(Encoding.UTF8.GetString(ciphertext));

        //         // Panggil fungsi Decrypt untuk mendekripsi ciphertext yang sudah dienkripsi
        //         byte[] decryptedPlaintext = new byte[ciphertext.Length]; // Pastikan ukuran plaintext cukup besar
        //         bool decryptionResult = aes.Decrypt(ciphertext, ref decryptedPlaintext);
                
        //         if (decryptionResult)
        //         {
        //             // Tampilkan hasil dekripsi
        //             string res = Encoding.UTF8.GetString(decryptedPlaintext);
        //             TrimSpace(ref res);
        //             Console.WriteLine("Decrypted plaintext: " + res);
        //         }
        //         else
        //         {
        //             Console.WriteLine("Decryption failed!");
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine("Encryption failed!");
        //     }
        
        // }
    }


}
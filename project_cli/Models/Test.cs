using System.Collections.Generic;
using System.IO;
using Tubes3.Models;

// Just for testing, add new method as your need
public static class Test {
    // Test to print a string
    public static void TestHere(string printTarget) {
        // Specify the path to your text file
        string filePath = "../test/txt_result/printTarget.txt";

        // Write some text to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Hello, world!");
            writer.WriteLine("This is a text file created by C#.");
            writer.WriteLine("This write to file method is only for testing purpose.");
            writer.WriteLine("Your printTarget is: " + printTarget);
        }
    }

    // Test to print anything
    public static void TestHere() {
        // Specify the path to your text file
        string filePath = "../test/txt_result/testHere.txt";

        // Write some text to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Hello, world!");
            writer.WriteLine("This is a text file created by C#.");
            writer.WriteLine("This write to file method is only for testing purpose.");
        }
    }

    // Test to print a list of biodata
    public static void TestHere(List<Biodata> biodatas) {
        // Specify the path to your text file
        string filePath = "../test/txt_result/biodatas.txt";

        // Write some text to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Hello, world!");
            writer.WriteLine("This is a text file created by C#.");
            writer.WriteLine("This write to file method is only for testing purpose.");
            writer.WriteLine("Your biodatas are:");
            foreach (var biodata in biodatas)
            {
                writer.WriteLine();
                writer.WriteLine(biodata.Nik);
                writer.WriteLine(biodata.Nama);
                writer.WriteLine(biodata.Tempat_lahir);
                writer.WriteLine(biodata.Tanggal_lahir);
                writer.WriteLine(biodata.Jenis_kelamin);
                writer.WriteLine(biodata.Golongan_darah);
                writer.WriteLine(biodata.Alamat);
                writer.WriteLine(biodata.Agama);
                writer.WriteLine(biodata.Status_perkawinan);
                writer.WriteLine(biodata.Pekerjaan);
                writer.WriteLine(biodata.Kewarganegaraan);
                writer.WriteLine();
            }
        }
    }

    // Test to print a list of sidikJari
    public static void TestHere(List<SidikJari> sidikJaris) {
        // Specify the path to your text file
        string filePath = "../test/txt_result/sidikJaris.txt";

        // Write some text to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Hello, world!");
            writer.WriteLine("This is a text file created by C#.");
            writer.WriteLine("This write to file method is only for testing purpose.");
            writer.WriteLine("Your sidikJaris are:");
            foreach (var sidikJari in sidikJaris)
            {
                writer.WriteLine();
                writer.WriteLine(sidikJari.Nama);
                writer.WriteLine(sidikJari.Berkas_citra);
                writer.WriteLine();
            }
        }
    }

    // Test to print a dictionary of imageAsciiMap<ascii, (path, nama)>
    public static void TestHere(Dictionary<string, (string, string?)> imageAsciiMap) {
        // Specify the path to your text file
        string filePath = "../test/txt_result/imageAsciiMap.txt";

        // Write some text to the file
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Hello, world!");
            writer.WriteLine("This is a text file created by C#.");
            writer.WriteLine("This write to file method is only for testing purpose.");
            writer.WriteLine("Your imageAsciiMap are:");
            foreach (var imageAscii in imageAsciiMap)
            {
                writer.WriteLine();
                writer.WriteLine(imageAscii.Key);
                writer.WriteLine(imageAscii.Value);
                writer.WriteLine();
            }
        }
    }

}
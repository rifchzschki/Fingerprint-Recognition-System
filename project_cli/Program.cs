using Tubes3.Models;

class Program {
    static void Main(string[] args) {
        // Inisiasi variabel yg ga penting bagi program utama
        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        
        Console.WriteLine("Welcome to KacaprukReborn App");
        Console.WriteLine("######## CLI Version ########\n");

        Console.Write($"Process all images in \"test\" folder...  ");
        // Process all images in the database to ASCII
        Dictionary<string, (string, string?)> imageAsciiMap = ImageProcessor.ProcessImagesToAscii();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"(DONE)\n");
        Console.ForegroundColor = originalForegroundColor;
        
        
        Console.WriteLine($"Input your image filename (without extension): ");
        string? filename = Console.ReadLine();

        // Validate filename
        while (filename == null || filename == "") {
            // Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nFilename cannot be empty!\n");
            // Console.ForegroundColor = originalForegroundColor;
            Console.WriteLine($"Input your image filename (without extension): ");
            filename = Console.ReadLine();
        }
        
        Console.WriteLine($"\nSelect the algorithm you want to use:");
        Console.WriteLine($"0. Knuth-Morris-Pratt (KMP)");
        Console.WriteLine($"1. Boyer-Moore (BM)\n");
        Console.Write($"Your choice: ");
        string? selectedAlgorithm = Console.ReadLine();

        // Validate selected algorithm
        while (selectedAlgorithm == null || (selectedAlgorithm != "0" && selectedAlgorithm != "1")) {
            // Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nInvalid algorithm choice!\n");
            // Console.ForegroundColor = originalForegroundColor;
            Console.WriteLine($"Select the algorithm you want to use:");
            Console.WriteLine($"0. Knuth-Morris-Pratt (KMP)");
            Console.WriteLine($"1. Boyer-Moore (BM)\n");
            Console.Write($"Your choice: ");
            selectedAlgorithm = Console.ReadLine();
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        Console.WriteLine($"\nProcessing image...  ");
        // Lanjutin yg kurang dari sini
    }
}
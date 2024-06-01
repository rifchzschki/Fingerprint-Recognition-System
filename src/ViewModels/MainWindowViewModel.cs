using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Tubes3.Models;

namespace Tubes3.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Bitmap? _uploadedImage;
        private Bitmap? _matchedImage;
        private string _personData;

        private string _asciiRepresentation;

        private string _timeElapsed;

        private string _selectedAlgorithm;
        private string _similarityPercentage;

        public Bitmap? UploadedImage
        {
            get => _uploadedImage;
            set => this.RaiseAndSetIfChanged(ref _uploadedImage, value);
        }

        public Bitmap? MatchedImage
        {
            get => _matchedImage;
            set => this.RaiseAndSetIfChanged(ref _matchedImage, value);
        }

        public string PersonData
        {
            get => _personData;
            set => this.RaiseAndSetIfChanged(ref _personData, value);
        }

        public string AsciiRepresentation
        {
            get => _asciiRepresentation;
            set => this.RaiseAndSetIfChanged(ref _asciiRepresentation, value);
        }

        public string TimeElapsed
        {
            get => _timeElapsed;
            set => this.RaiseAndSetIfChanged(ref _timeElapsed, value);
        }

        public string SimilarityPercentage
        {
            get => _similarityPercentage;
            set => this.RaiseAndSetIfChanged(ref _similarityPercentage, value);
        }


        public string SelectedAlgorithm
        {
            get => _selectedAlgorithm;
            set => this.RaiseAndSetIfChanged(ref _selectedAlgorithm, value);
        }


        public string pattern { get; set; }



        [Obsolete]
        public ReactiveCommand<Unit, Unit> UploadCommand { get; }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; }

        [Obsolete]
        public MainWindowViewModel()
        {
            // Initialize properties with default values
            _uploadedImage = null;
            _matchedImage = null;
            _personData = string.Empty;
            _asciiRepresentation = string.Empty;
            _timeElapsed = string.Empty;
            _similarityPercentage = string.Empty;
            _selectedAlgorithm = "0";
            pattern = string.Empty;


            UploadCommand = ReactiveCommand.CreateFromTask(UploadImage);
            SearchCommand = ReactiveCommand.CreateFromTask(SearchFingerprint);
        }

        [Obsolete]
        private async Task UploadImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Filters = new List<FileDialogFilter>
                {
                    new FileDialogFilter { Name = "Bitmap Images", Extensions = { "bmp" } }
                }
            };
            var result = await openFileDialog.ShowAsync(new Window());

            if (result != null && result.Length > 0)
            {
                var filePath = result[0];
                if (File.Exists(filePath))
                {
                    using (var stream = File.OpenRead(filePath))
                    {
                        var bitmap = new Bitmap(stream);
                        UploadedImage = bitmap;

                        // Convert image to binary and then to ASCII - 8 bit
                        var AsciiRepresentation = ImageProcessor.ConvertBitmapToAscii(stream);

                        // ambil pattern dari 30 pixel di baris tengah
                        pattern = ImageProcessor.GetPatternFromAscii(AsciiRepresentation);

                        // var asciiFilePath = "halo.txt"; // buat cek hasil konversi
                        // File.WriteAllText(asciiFilePath, AsciiRepresentation);
                    }
                }
            }
        }

        private Task SearchFingerprint()
        {

            if (UploadedImage == null)
            {
                return Task.CompletedTask;
            }

            // LAKUIN QUERY KE DATABASE DISINI

            string? nama;
            // Simulate fingerprint matching logic

            Dictionary<string, (string, string?)> imageAsciiMap = ImageProcessor.ProcessImagesToAscii();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            if (SelectedAlgorithm == "0") // KMP
            {
                nama = KMP.FindPatternInTexts(pattern, imageAsciiMap);

            }
            else // BM
            {
                nama = "";
                // Proses pake algoritma BM
            }
            DatabaseHelper dh = new DatabaseHelper();
            Test.TestHere("a");
            List<string> alays= dh.GetNamaFromAlay();
            Test.TestHere("b");
            Biodata bio;
            Test.TestHere("c");
            string? alay = ConvertAlay.findAlayMatch(alays, nama);
            Test.TestHere("d");

            if(alay == null){
                // handle nama tidak ditemukan
            }else{
                List<Biodata> b = new List<Biodata>();
                bio = dh.GetBiodataFromAlay(alay);
                b.Add(bio);
                Test.TestHere(b);
                // string? nama_dummy = bio.Nama;
                
                // ambil biodata dari database
                // tampilin biodata

                StringBuilder sb = bio.showInfo();
                PersonData = sb.ToString();

            }
            stopwatch.Stop();
            TimeElapsed = $"{stopwatch.ElapsedMilliseconds} ms";

            // ambil gambar dari database

            // tampilin gambar
            MatchedImage = UploadedImage; // For demonstration, just use the uploaded image as matched image



            // Cari similaritas
            SimilarityPercentage = "95%";
            return Task.CompletedTask;
        }
    }
}

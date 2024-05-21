using Avalonia.Controls;
using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using Tubes3.Models;

namespace Tubes3.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Bitmap? _uploadedImage;
        private Bitmap? _matchedImage;
        private string _personData;
        private string _executionTime;
        private string _matchPercentage;

        private string _asciiRepresentation;

        private string _timeElapsed;

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

        public string ExecutionTime
        {
            get => _executionTime;
            set => this.RaiseAndSetIfChanged(ref _executionTime, value);
        }

        public string MatchPercentage
        {
            get => _matchPercentage;
            set => this.RaiseAndSetIfChanged(ref _matchPercentage, value);
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

        public ReactiveCommand<Unit, Unit> UploadCommand { get; }
        public ReactiveCommand<Unit, Unit> SearchCommand { get; }

        [Obsolete]
        public MainWindowViewModel()
        {
            // Initialize properties with default values
            _uploadedImage = null;
            _matchedImage = null;
            _personData = string.Empty;
            _executionTime = string.Empty;
            _matchPercentage = string.Empty;
            _asciiRepresentation = string.Empty;
            _timeElapsed = string.Empty;
            _similarityPercentage = string.Empty;

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
                        var binaryData = ImageProcessor.ConvertImageToBinary(new Bitmap(filePath));
                        AsciiRepresentation = ImageProcessor.SetStringtoASCII(binaryData);

                        var asciiFilePath = "halo.txt"; // buat cek hasil konversi
                        File.WriteAllText(asciiFilePath, AsciiRepresentation);
                    }
                }
            }
        }

        private async Task SearchFingerprint()
        {
            if (UploadedImage == null)
            {
                // Handle the case where no image is uploaded
                return;
            }

            // Simulate fingerprint matching logic
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            await Task.Delay(1000); // Simulate a delay for the search operation

            stopwatch.Stop();
            ExecutionTime = $"Execution Time: {stopwatch.ElapsedMilliseconds} ms";

            // Simulate finding a match
            MatchedImage = UploadedImage; // For demonstration, just use the uploaded image as matched image
            PersonData = "Name: Rudi Kurniawan\nID: 23123123";
            MatchPercentage = "Match Percentage: 95%";
        }
    }
}

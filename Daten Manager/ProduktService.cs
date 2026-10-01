using Daten_Manager;
using Microsoft.JSInterop;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace Daten_Manager
{
    public class ProduktService
    {

        private readonly HttpClient _http;
        private readonly IJSRuntime _jsRuntime;
        public ProduktService(IJSRuntime jsRuntime, HttpClient http)
        {
            _jsRuntime = jsRuntime;
            _http = http;
        }
        public string fileContent { get; set; }
        public List<Produkt> hochgeladeneProdukte { get; set; } = [];
        public bool isFileLoaded { get; set; } = false;

        public async Task LoadProductDataAsync(MemoryStream memoryStream)
        {
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
            {
                var json = archive.GetEntry("bearbeiteteProdukte.json")!;
                using var reader = new StreamReader(json.Open());
                var fileContent = await reader.ReadToEndAsync();
                hochgeladeneProdukte = JsonSerializer.Deserialize<List<Produkt>>(fileContent);

                Bilder.Clear();

                var bildPfade = hochgeladeneProdukte.Select(produkt => produkt.bildPfad).Distinct();

                foreach (var bildPfad in bildPfade)
                {
                    var bildEntry = archive.GetEntry(bildPfad)!;
                    using Stream entryStream = bildEntry.Open();

                    using MemoryStream targetStream = new MemoryStream();
                    await entryStream.CopyToAsync(targetStream);

                    byte[] fileBytes = targetStream.ToArray();

                    Bilder[bildPfad] = fileBytes;
                }

                foreach (var produkt in hochgeladeneProdukte)
                {
                    var extension = Path.GetExtension(produkt.bildPfad).ToLowerInvariant();

                    var contentType = extension == ".png" ? "image/png" : "image/jpeg";

                    var base64String = Convert.ToBase64String(Bilder[produkt.bildPfad]);

                    produkt.bildDataUrl = $"data:{contentType};base64,{base64String}";
                }
            }

            isFileLoaded = true;
        }
        public Produkt? GetProductById(int id)
        {
            return hochgeladeneProdukte.First(p => p.produktId == id);
        }

        public async Task ExportDataToJsonAsync()
        {
            string json = JsonSerializer.Serialize(hochgeladeneProdukte);

            using var memoryStream = new MemoryStream();

            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, leaveOpen: true))
            {
                var file1Entry = archive.CreateEntry("bearbeiteteProdukte.json");
                using (var writer = new StreamWriter(file1Entry.Open(), Encoding.UTF8))
                {
                    await writer.WriteLineAsync(json);
                }

                var bildPfade = hochgeladeneProdukte.Select(produkt => produkt.bildPfad).Distinct();

                foreach (var bildPfad in bildPfade)
                {
                    if (!Bilder.TryGetValue(bildPfad, out var imageData))
                    {
                        imageData = await _http.GetByteArrayAsync(bildPfad);
                    }

                    var file2Entry = archive.CreateEntry(bildPfad);
                    using (var imageStream = file2Entry.Open())
                    {
                        await imageStream.WriteAsync(imageData, 0, imageData.Length);
                    }
                }
            }

            memoryStream.Position = 0;

            using var streamRef = new DotNetStreamReference(stream: memoryStream);
            await _jsRuntime.InvokeVoidAsync("downloadFileFromStream", "BearbeitetProduktdaten.zip", streamRef);
        }

        public Dictionary<string, byte[]> Bilder { get; } = new();
    }
}

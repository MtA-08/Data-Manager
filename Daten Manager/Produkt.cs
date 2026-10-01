namespace Daten_Manager
{
    public class Produkt
    {
        public int produktId { get; set; }
        public string name { get; set; } = string.Empty;
        public string hersteller { get; set; } = string.Empty;
        public decimal preis { get; set; }
        public List<string> eigenschaften { get; set; } = [];
        public string bildPfad { get; set; } = string.Empty;
        public string produktTyp { get; set; } = string.Empty;
        public string stichwörter { get; set; } = string.Empty;
        public string idealoUrl { get; set; } = string.Empty;

        [System.Text.Json.Serialization.JsonIgnore]
        public string? bildDataUrl { get; set; } = string.Empty;

        public Produkt CreateCopy()
        {
            var kopie = (Produkt)MemberwiseClone();
            kopie.eigenschaften = eigenschaften?.ToList() ?? [];

            return kopie;
        }
    }
}

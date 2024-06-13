namespace BlazorApp2.Shared.Models
{
    public class Card
    {
        public string Type { get; set; }
        public string CatName { get; set; }

        public Card(string type, string catName = null)
        {
            Type = type;
            CatName = catName;
        }
    }
}

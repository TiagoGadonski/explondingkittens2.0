namespace BlazorApp2.Shared.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
        public bool IsCurrentPlayer { get; set; }
        public bool HasLost { get; set; } = false;
    }
}

namespace BlazorApp2.Shared.Models
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>();
        }

        public Deck(List<Card> shuffledCards)
        {
            cards = new List<Card>(shuffledCards);
        }

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public Card Draw()
        {
            if (cards.Count > 0)
            {
                Card drawnCard = cards[0];
                cards.RemoveAt(0);
                return drawnCard;
            }
            return null;
        }

        public List<Card> Peek(int count)
        {
            return cards.Take(count).ToList();
        }

        public void ReturnCardToPosition(Card card, int position)
        {
            if (position < 0 || position > cards.Count)
            {
                position = cards.Count; // Adicionar ao final se a posição for inválida
            }
            cards.Insert(position, card);
        }

        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }

        public int GetDeckSize()
        {
            return cards.Count;
        }

        public void Shuffle()
        {
            var rng = new Random();
            cards = cards.OrderBy(a => rng.Next()).ToList();
        }
    }
}

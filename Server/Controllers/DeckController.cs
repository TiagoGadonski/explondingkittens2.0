using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeckController : ControllerBase
    {
        private static readonly string[] CatTypes = new[]
        {
            "Tacocat", "Hairy Potato Cat", "Rainbow Ralphing Cat", "Beard Cat", "Cattermelon"
        };

        private static Deck deck;

        public DeckController()
        {
            if (deck == null)
            {
                deck = InitializeDeck(4); // Inicialize com um número padrão de jogadores, pode ser ajustado conforme necessário
            }
        }

        [HttpGet("start/{numPlayers}")]
        public List<List<Card>> GetInitialCards(int numPlayers)
        {
            deck = InitializeDeck(numPlayers);
            var initialHands = new List<List<Card>>();
            for (int i = 0; i < numPlayers; i++)
            {
                var hand = new List<Card>();
                for (int j = 0; j < 7; j++)
                {
                    hand.Add(deck.Draw());
                }
                hand.Add(new Card("Defuse"));
                initialHands.Add(hand);
            }
            return initialHands;
        }

        [HttpGet("peek/{count}")]
        public List<Card> PeekNextCards(int count)
        {
            return deck.Peek(count);
        }

        [HttpGet("draw")]
        public Card DrawCard()
        {
            return deck.Draw();
        }

        [HttpPost("return/{position}")]
        public IActionResult ReturnCardToDeck([FromRoute] int position, [FromBody] Card card)
        {
            deck.ReturnCardToPosition(card, position);
            return Ok();
        }

        [HttpPost("shuffle")]
        public IActionResult ShuffleDeck()
        {
            deck.Shuffle();
            return Ok();
        }

        private Deck InitializeDeck(int numPlayers)
        {
            var deck = new Deck();
            int numExplodingKittens = numPlayers - 1;
            int numDefuses = 6;

            // Adicionando cartas Exploding Kitten
            for (int i = 0; i < numExplodingKittens; i++)
            {
                deck.AddCard(new Card("Exploding Kitten"));
            }

            // Adicionando cartas Defuse
            for (int i = 0; i < numDefuses; i++)
            {
                deck.AddCard(new Card("Defuse"));
            }

            // Adicionando outras cartas
            AddCardsToDeck(deck, "Attack", 4);
            AddCardsToDeck(deck, "Skip", 4);
            AddCardsToDeck(deck, "Favor", 4);
            AddCardsToDeck(deck, "Shuffle", 4);
            AddCardsToDeck(deck, "See the Future", 5);
            AddCardsToDeck(deck, "Nope", 5);

            // Adicionando cartas de gato
            foreach (var catType in CatTypes)
            {
                AddCardsToDeck(deck, catType, 4);
            }

            deck.Shuffle();
            return deck;
        }

        private void AddCardsToDeck(Deck deck, string type, int count)
        {
            for (int i = 0; i < count; i++)
            {
                deck.AddCard(new Card(type));
            }
        }
    }
}

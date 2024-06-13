using BlazorApp2.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeckController : ControllerBase
    {
        private static Deck deck;
        private static List<Card> shuffledDeck;

        [HttpGet("start/{numPlayers}")]
        public ActionResult<List<List<Card>>> StartGame(int numPlayers)
        {
            shuffledDeck = GenerateDeck(numPlayers);
            deck = new Deck(shuffledDeck);

            var initialCards = new List<List<Card>>();
            for (int i = 0; i < numPlayers; i++)
            {
                var playerCards = new List<Card>();
                for (int j = 0; j < 7; j++)
                {
                    playerCards.Add(deck.Draw());
                }
                playerCards.Add(new Card("Defuse")); // Adicionar 1 carta Defuse para cada jogador
                initialCards.Add(playerCards);
            }

            return Ok(initialCards);
        }

        private List<Card> GenerateDeck(int numPlayers)
        {
            var cards = new List<Card>();

            // Adicionar cartas especiais
            for (int i = 0; i < 10; i++) // 10 * 6 = 60 cartas
            {
                cards.Add(new Card("Attack"));
                cards.Add(new Card("Skip"));
                cards.Add(new Card("Favor"));
                cards.Add(new Card("Shuffle"));
                cards.Add(new Card("See the Future"));
                cards.Add(new Card("Nope"));
            }

            // Adicionar cartas de gatos
            string[] catNames = { "Tacocat", "Hairy Potato Cat", "Rainbow Ralphing Cat", "Beard Cat", "Cattermelon" };
            foreach (var catName in catNames)
            {
                for (int i = 0; i < 4; i++) // Adicionar 4 de cada carta de gato
                {
                    cards.Add(new Card("Cat Card", catName));
                }
            }

            // Adicionar cartas de Exploding Kitten e Defuse
            for (int i = 0; i < numPlayers + 1; i++) // Aumentar o número de Exploding Kitten para testes
            {
                cards.Add(new Card("Exploding Kitten"));
            }
            for (int i = 0; i < numPlayers; i++) // Diminuir o número de Defuse
            {
                cards.Add(new Card("Defuse"));
            }

            // Embaralhar o baralho
            var rng = new Random();
            return cards.OrderBy(card => rng.Next()).ToList();
        }

        [HttpGet("draw")]
        public ActionResult<Card> DrawCard()
        {
            var card = deck.Draw();
            return card != null ? Ok(card) : NotFound("No more cards.");
        }

        [HttpGet("peek/{count}")]
        public ActionResult<List<Card>> PeekCards(int count)
        {
            var cards = deck.Peek(count);
            return cards != null ? Ok(cards) : NotFound("No more cards.");
        }

        [HttpPost("return/{position}")]
        public IActionResult ReturnCardToPosition([FromBody] Card card, int position)
        {
            deck.ReturnCardToPosition(card, position);
            return Ok();
        }
    }
}

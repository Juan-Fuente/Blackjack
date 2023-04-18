using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibraryLab2
{
    public class BlackjackGame
    {
        BlackjackHand _player = new BlackjackHand();
        BlackjackHand _dealer = new BlackjackHand(true);
        Deck _deck = new Deck();
    }
    // PART A-2
    public enum CardSuit
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }

    public enum CardFace
    {
        A,
        two,
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        J,
        Q,
        K
    }

    //PART A-3
    public interface ICard
    {
        CardFace Face
        {
            get;
        }
        CardSuit Suit
        {
            get;
        }

        void Draw(int x, int y);
    }

    //PART A-4

    public class Card : ICard
    {
        public CardSuit Suit { get; private set; }

        public CardFace Face { get; private set; }

        public Card(CardFace face, CardSuit suit)
        {
            Suit = suit;
            Face = face;
        }

        public virtual void Draw(int x, int y)
        {
            Console.CursorLeft = x;
            Console.CursorTop = y;
            string face = string.Empty;
            string suit = string.Empty;

            switch ((int)Suit)
            {
                case 0:
                    suit = "\u2660";
                    break;
                case 1:
                    suit = "\u2665";
                    break;
                case 2:
                    suit = "\u2663";
                    break;
                case 3:
                    suit = "\u2666";
                    break;
            }


            switch ((int)Face)
            {
                case 0:
                    face = "A";
                    break;
                case 1:
                    face = "2";
                    break;
                case 2:
                    face = "3";
                    break;
                case 3:
                    face = "4";
                    break;
                case 4:
                    face = "5";
                    break;
                case 5:
                    face = "6";
                    break;
                case 6:
                    face = "7";
                    break;
                case 7:
                    face = "8";
                    break;
                case 8:
                    face = "9";
                    break;
                case 9:
                    face = "10";
                    break;
                case 10:
                    face = "J";
                    break;
                case 11:
                    face = "Q";
                    break;
                case 12:
                    face = "K";
                    break;
            }

            if (Suit == (CardSuit)1 || Suit == (CardSuit)3)
            {
                if (Face == (CardFace)9)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\b{face}   ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b  {suit}  ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b   {face}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\b{face}    ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b  {suit}  ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b    {face}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            else
            {
                if (Face == (CardFace)9)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"\b{face}   ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b  {suit}  ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b   {face}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"\b{face}    ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b  {suit}  ");
                    Console.CursorLeft = x;
                    Console.WriteLine($"\b    {face}");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }

    }

    //PART A-5 AND PART B-3

    public class CardFactory
    {
        public static ICard CreateCard(CardFace face, CardSuit suit)
        {
            ICard card = new Card(face, suit);
            return card;
        }
        public static ICard CreateBlackjackCard(CardFace face, CardSuit suit)
        {
            ICard card = new BlackjackCard(face, suit);
            return card;
        }
    }

    // PART A-6
    public class Deck
    {
        List<ICard> _cards;

        public Deck() //void doesn't seem work
        {
            _cards = new List<ICard>();

            FillDeck();

        }
        public void FillDeck()
        {
            foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
            {
                foreach (CardFace face in Enum.GetValues(typeof(CardFace)))
                {
                    _cards.Add(CardFactory.CreateBlackjackCard(face, suit));
                }
            }
        }
        public ICard Deal()
        {
            if (_cards.Count == 0)
            {
                CreateDeck();
                Shuffle();
            }
            ICard dealCard = _cards[0];
            _cards.RemoveAt(0);
            return dealCard;
        }
        public void Shuffle()
        {
            Random rng = new Random();

            for (var i = _cards.Count - 1; i > 0; i--)
            {
                int k = rng.Next(i + 1);
                ICard value = _cards[k];
                _cards[k] = _cards[i];
                _cards[i] = value;

            }
        }
        public void CreateDeck()
        {
            int left = Console.CursorLeft + 1;
            int top = Console.CursorTop;

            int count = 0;

            foreach (ICard card in _cards)
            {
                card.Draw(left, top);

                count++;
                if (count % 11 == 0)
                {
                    top = top + 7;
                    left = 2;
                }
                else
                {
                    left = left + 7;
                }
            }
        }
    }

    // PART A-7
    public class Hand
    {
        protected List<ICard> _cards = new List<ICard>();
        public virtual void AddCard(ICard card)
        {
            _cards.Add(card);
        }
        public virtual void Draw(int x, int y)
        {
            foreach (ICard crd in _cards)
            {
                crd.Draw(x, y);
                x += 7;
                if (x + 30 >= Console.WindowWidth)
                {
                    x = 0;
                    y += 5;
                }
            }
        }
    }

    //PART B-1
    public class BlackjackCard : Card
    {
        public int[] value { get; set; }
        public BlackjackCard(CardFace face, CardSuit suit) : base(face, suit)
        { //change if else to switch due 2 bug
            switch (face)
            {
                case CardFace.two:
                    value = new int[] { 2 };
                    break;
                case CardFace.three:
                    value = new int[] { 3 };
                    break;
                case CardFace.four:
                    value = new int[] { 4 };
                    break;
                case CardFace.five:
                    value = new int[] { 5 };
                    break;
                case CardFace.six:
                    value = new int[] { 6 };
                    break;
                case CardFace.seven:
                    value = new int[] { 7 };
                    break;
                case CardFace.eight:
                    value = new int[] { 8 };
                    break;
                case CardFace.nine:
                    value = new int[] { 9 };
                    break;
                case CardFace.ten:
                    value = new int[] { 10 };
                    break;
                case CardFace.A:
                    value = new int[] { 1, 11 };
                    break;
                default:
                    value = new int[] { 10 };
                    break;

            }
        }
    }

    // PART B-2
    public class BlackjackHand : Hand
    {
        public int Score { get; private set; }
        public bool IsDealer { get; private set; }

        public BlackjackHand(bool isDealer = false)
        {
            Score = 0;
            IsDealer = isDealer;
        }

        public override void AddCard(ICard card)
        {
            _cards.Add(card);

            calculateScore();
        }

        public void calculateScore()
        {
            int score = 0;

            List<BlackjackCard> aces = new List<BlackjackCard>();

            foreach (BlackjackCard card in _cards)
            {
                if (card.Face == CardFace.A)
                {
                    aces.Add(card);
                    score += 1;
                    continue;
                }

                score += card.value[0];
            }

            foreach (BlackjackCard ace in aces)
            {
                if (score + 10 <= 21)
                {
                    score += 10;
                }
            }

            Score = score;
        }

        public override void Draw(int x, int y)
        {
            if (IsDealer)
            {
                Console.WriteLine("{0, -15} ({1,2})  ", "Dealer", Score);
                base.Draw(x, y + 2);

            }
            else
            {
                Console.WriteLine("{0, -15} ({1, 2})", "Player", Score);
                base.Draw(x, y + 2);
            }
        }

        public void Draw(int x, int y, bool hide)
        {
            if (!hide)
            {
                Draw(x, y);
            }
            else
            {

                if (IsDealer)
                {
                    Console.WriteLine("{0, -15} ({1,2})  ", "Dealer's Cards: ", "??");
                    int left = Console.BufferWidth;


                    int count = 1;
                    foreach (ICard card in _cards)
                    {
                        int top = y + 2;
                        left = left - 15;
                        Console.SetCursorPosition(left, top);
                        if (count == 1)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("     ");
                            Console.SetCursorPosition(left, ++top);
                            Console.Write("     ");
                            Console.SetCursorPosition(left, ++top);
                            Console.Write("     ");
                        }
                        else
                        {
                            card.Draw(left, top);
                        }
                        count = count + 1;
                    }
                }
                else
                {
                    Console.WriteLine("{0, -15} ({1, 2})", "Player's Cards: ", Score);
                    base.Draw(x, y + 2);
                }
            }
        }
    }
}



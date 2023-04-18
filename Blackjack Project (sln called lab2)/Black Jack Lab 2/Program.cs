using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLab2;

namespace Black_Jack_Console_App
{
    //PART A-8
    class Program
    {
        public static void PlayRound()
        {
            BlackjackHand _player = new BlackjackHand();

            BlackjackHand _dealer = new BlackjackHand(true);

            Deck _deck = new Deck();

            //DealInitialCards();

            //PlayersTurn();

            //DealersTurn();

            //DeclareWinner();



//
        // methods broke at the end don't know why
        //
        //
        //merged them together to avoid bug





        //public static void DealInitialCards()
        
            

            _deck.Shuffle();

            for (int i = 0; i < 2; i++)
            {
                _player.AddCard(_deck.Deal());
                _dealer.AddCard(_deck.Deal());
            }

            CursorPosition(ref _dealer, ref _player);

            if (_player.Score == 21)
            {

                CursorPosition(ref _dealer, ref _player, false);
                Console.WriteLine("Player won! BlackJack! No way! :D");
                return;
            }
            else if (_dealer.Score == 21)
            {
                CursorPosition(ref _dealer, ref _player, false);
                Console.WriteLine("Dealer got BlackJack!!! :(");
                return;
            }
        
        //public static void PlayersTurn()
        
            

            int choice;
            do
            {
                string[] options = { "1. Hit", "2. Stand" };

                ReadChoice(options, out choice, "Choice? ");

                if (choice == 1)
                {
                    _player.AddCard(_deck.Deal());

                    CursorPosition(ref _dealer, ref _player);
                    if (_player.Score > 21)
                    {
                        CursorPosition(ref _dealer, ref _player, false);
                        Console.WriteLine("Player got busted!");
                        return;
                    }
                    else if (_player.Score == 21)
                    {
                        CursorPosition(ref _dealer, ref _player, false);
                        Console.WriteLine("Player won! BlackJack! No way! :D");
                        return;
                    }
                }
            } while (choice != 2);

        
        //public static void DealersTurn()
        
            do
            {
                if (_dealer.Score < 17)
                {
                    _dealer.AddCard(_deck.Deal());
                }

                CursorPosition(ref _dealer, ref _player);
            } while (_dealer.Score < 17);

            

        
        //public static void DeclareWinner()
            
            
            CursorPosition(ref _dealer, ref _player, false);

            if (_dealer.Score == 21)
            {
                Console.WriteLine("Dealer got BlackJack!!! :(");
                return;
            }
            else
            {
                Console.WriteLine("Draw");
                return;
            }
        }
        public static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            string[] menuOptions =
            {
                "1. Play BlackJack",
                "2. Shuffle and Show Deck",
                "3. Exit"
            };

            int choice;

            do
            {
                ReadChoice(menuOptions, out choice, "Choice? ");

                switch (choice)
                {
                    case 1:
                        bool exit = false;
                        do
                        {
                            PlayRound();

                            bool valid = false;
                            string answer = string.Empty;
                            do
                            {
                                ReadString("Play again? (Yes or No) (y or n) ", ref answer);

                                if (answer == "yes" || answer == "y" || answer == "no" || answer == "n")
                                {
                                    valid = true;
                                }
                            } while (!valid);

                            exit = (answer == "no" || answer == "n");
                        } while (!exit);


                        break;
                    case 2:
                        Deck cardDeck = new Deck();
                        cardDeck.Shuffle();
                        cardDeck.CreateDeck();
                        break;
                    case 3:
                        break;
                }
            } while (choice != 3);

        }
        public static int ReadInteger(string prompt = "Please enter a number ", int min = 0, int max = Int32.MaxValue)
        {

            bool intEntered = false;
            int inputInt = min - 1;

            while (!intEntered)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                try
                {
                    inputInt = Int32.Parse(input);

                    if (inputInt < min || inputInt > max)
                    {
                        Console.WriteLine("Number is too large");
                        continue;
                    }
                    else
                    {
                        intEntered = true;
                    }
                }
                catch 
                {
                    Console.WriteLine("Not a number where I'm from...");
                    continue;
                }

            }

            return inputInt;
        }
        public static void CursorPosition(ref BlackjackHand dealer, ref BlackjackHand player, bool hide = true)
        {
            Console.Clear();
            dealer.Draw(0, 0, hide);
            Console.SetCursorPosition(0, 15);
            player.Draw(0, 15, hide);

        }
        public static void ReadString(string prompt, ref string returInput)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Write(prompt);
                string tempInput = Console.ReadLine();

                tempInput = tempInput.Trim().ToLower();

                if (tempInput.Length > 0)
                {
                    returInput = tempInput;
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Please enter yes or no");
                }
            }
        }
        public static void ReadChoice(string[] options, out int selection, string prompt = "Please select an option ")
        {
            Console.WriteLine();
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }

            Console.WriteLine();

            selection = ReadInteger(prompt, 1, options.Length + 1);
        }
    }
}
    









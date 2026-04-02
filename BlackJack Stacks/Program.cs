using System;
using System.Globalization;
using System.Media;

namespace BlackJack_Stacks
{
    internal class Program
    {
        // Constants
        private const decimal MIN_BET = 0.01m;
        private const decimal MAX_BET = 1_000_000m;
        private const decimal MIN_STARTING_CHIPS = 100m;
        private const decimal MAX_STARTING_CHIPS = 1_000_000m;
        private const char WIN_KEY = 'W';
        private const char LOSE_KEY = 'L';
        private const char PUSH_KEY = 'P';
        private const string YES_PREFIX = "Y";

        private class Player  // Small, focused Player model
        {
            public string Name { get; set; }
            public decimal Balance { get; set; }
        }

        // State
        private static readonly Player[] Players = new Player[2];
        private static int Round = 1;

        static void Main(string[] args)
        {
            Console.Title = "BlackJack Stacks - Virtual Chips by charles Martin";
            Console.ForegroundColor = ConsoleColor.White;

            InitializePlayers();
            BetLoop();
        }

        /// <summary>
        /// Initializes players with names and starting balances
        /// </summary>
        static void InitializePlayers()
        {
            GetNames();
            decimal startingChips = GetDecimal("\nHow much money does each player start with?  ", MIN_STARTING_CHIPS, MAX_STARTING_CHIPS);
            Players[0].Balance = startingChips;
            Players[1].Balance = startingChips;
        }

        /// <summary>
        /// Robust numeric input for money values (uses decimal for currency)
        /// </summary>
        static decimal GetDecimal(string prompt, decimal min, decimal max)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                var line = Console.ReadLine() ?? "";
                if (decimal.TryParse(line, NumberStyles.Number | NumberStyles.AllowCurrencySymbol, CultureInfo.CurrentCulture, out value))
                {
                    if (value >= min && value <= max)
                        return value;

                    SystemSounds.Asterisk.Play();
                    Console.WriteLine($"Please enter a value between {min:C2} and {max:C2}.");
                }
                else
                {
                    SystemSounds.Asterisk.Play();
                    Console.WriteLine("Invalid number. Try again.");
                }
            }
        }

        /// <summary>
        /// Prints text to console with optional same-line output
        /// </summary>
        static void Print(string msg = "", bool sameLine = false)
        {
            if (sameLine)
                Console.Write(msg);
            else
                Console.WriteLine(msg);
        }

        /// <summary>
        /// Gets player and dealer names from user input
        /// </summary>
        static void GetNames()
        {
            Players[0] = new Player();
            Players[1] = new Player();

            Print("Type in the names for the following:");
            Print("    Player - ", true);
            var name = Console.ReadLine() ?? "";
            Players[0].Name = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();

            Print("    Dealer - ", true);
            name = Console.ReadLine() ?? "";
            Players[1].Name = string.IsNullOrWhiteSpace(name) ? "Dealer" : name.Trim();
        }

        /// <summary>
        /// Displays current scores and game status
        /// </summary>
        static void ScoreBlock()
        {
            Console.Clear();
            Console.WriteLine($"{Players[0].Name}:  {Players[0].Balance:C2}\t\t{Players[1].Name}:  {Players[1].Balance:C2}");
            Print($"Hand #{Round}\t\t\t\t", true);

            var diff = Math.Abs(Players[0].Balance - Players[1].Balance);
            if (Players[0].Balance > Players[1].Balance)
                Print($"{Players[0].Name} up by {diff:C2}");
            else if (Players[1].Balance > Players[0].Balance)
                Print($"{Players[1].Name} up by {diff:C2}");
            else
                Print("Tie Game!");
        }

        /// <summary>
        /// Gets bet amount from player with validation
        /// </summary>
        static decimal GetBet()
        {
            decimal bet;
            while (true)
            {
                bet = GetDecimal($"\n\nHow much is {Players[0].Name} betting?  $", 0m, MAX_BET);

                if (bet < MIN_BET)
                {
                    Print($"\nMinimum bet is {MIN_BET:C2}. Please enter a valid bet.");
                    continue;
                }

                if (bet <= Players[0].Balance)
                    return bet;

                Print($"\n\nUnfortunately, your bet is larger than your bankroll.");
                Print($"The highest possible bet is {Players[0].Balance:C2}");
            }
        }

        /// <summary>
        /// Handles split or double down logic
        /// </summary>
        static decimal HandleSplitOrDouble(decimal bet)
        {
            if (bet * 2m > Players[0].Balance)
            {
                Print("You are unable to double down or split during this hand.");
                return bet;
            }

            Print($"Did {Players[0].Name} split or double down this hand?  (Y or N)");
            var line = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (line.StartsWith(YES_PREFIX))
            {
                bet *= 2m;
                Print($"Your bet has been doubled to {bet:C2}");
            }
            else
            {
                Print("Your bet has not been changed.");
            }

            return bet;
        }

        /// <summary>
        /// Processes the hand result and updates balances
        /// </summary>
        static void ProcessHandResult(decimal bet)
        {
            Print($"\nDid {Players[0].Name} Win, Lose, or Push?  (W, L or P)", true);
            var result = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

            if (string.IsNullOrEmpty(result))
                return;

            var key = result[0];
            switch (key)
            {
                case WIN_KEY:
                    Print($"{Players[0].Name} has won {bet:C2}  Press a key for the next hand.");
                    Players[0].Balance += bet;
                    Players[1].Balance -= bet;
                    Console.ReadKey();
                    break;
                case LOSE_KEY:
                    Print($"{Players[0].Name} has lost {bet:C2}  Press a key for the next hand.");
                    Players[0].Balance -= bet;
                    Players[1].Balance += bet;
                    Console.ReadKey();
                    break;
                case PUSH_KEY:
                    Print("It's a tie, no money changes hands.  Press a key for the next hand.");
                    Console.ReadKey();
                    break;
                default:
                    Print("Input not recognized. No changes made this hand.");
                    Console.ReadKey();
                    break;
            }
        }

        /// <summary>
        /// Checks if either player is bankrupt and displays end game message
        /// </summary>
        static bool CheckBankruptcy()
        {
            if (Players[0].Balance <= 0m)
            {
                Print($"\n\n{Players[0].Name} (the player) has gone broke. Better luck next time.");
                Print($"{Players[1].Name} won a total of {Players[1].Balance:C2}  Press a key...");
                Console.ReadKey();
                return true;
            }

            if (Players[1].Balance <= 0m)
            {
                Print($"\n\n{Players[1].Name} (the dealer) has gone broke.");
                Print($"{Players[0].Name} won a total of {Players[0].Balance:C2}  Press a key...");
                Console.ReadKey();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Main betting loop
        /// </summary>
        static void BetLoop()
        {
            while (true)
            {
                ScoreBlock();

                decimal bet = GetBet();
                bet = HandleSplitOrDouble(bet);
                ProcessHandResult(bet);

                if (CheckBankruptcy())
                    break;

                Round++;
            }
        }
    }
}
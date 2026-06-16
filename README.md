# BlackJack Stacks

A simple console-based chip tracking application for BlackJack games. This program helps track virtual chips between a player and dealer during BlackJack sessions, eliminating the need for physical chip counting.

## 🎲 Features

- **Two-Player Chip Tracking**: Track balances for one player and one dealer
- **Flexible Betting**: Support for bets from $0.01 to $1,000,000
- **Split & Double Down Support**: Automatically handle doubled bets for these scenarios
- **Real-time Score Display**: See current balances and who's ahead at a glance
- **Hand Result Processing**: Record wins, losses, and pushes
- **Bankruptcy Detection**: Game automatically ends when either player runs out of chips
- **Currency Formatting**: Proper display of monetary values with culture-aware formatting
- **Input Validation**: Robust error handling for all user inputs

## 🚀 Getting Started

### Prerequisites

- .NET Framework 4.8.1 or higher
- Windows operating system (uses `System.Media.SystemSounds`)

### Installation

1. Clone the repository:
```bash
git clone https://github.com/HiTechCharles/BlackJack-Stacks.git
```

2. Open the solution in Visual Studio 2022 or later

3. Build and run the application

### Running the Application

Simply run the executable. The application will guide you through:

1. **Setup Phase**:
   - Enter player name (or press Enter for default "Player")
   - Enter dealer name (or press Enter for default "Dealer")
   - Set starting chip amount for both players

2. **Game Loop**:
   - View current balances and who's ahead
   - Enter the bet amount
   - Indicate if split or double down occurred
   - Record the hand result (W = Win, L = Lose, P = Push)
   - Continue until one player goes broke

## 📖 Usage Example

```
Type in the names for the following:
	Player - Alice
	Dealer - Bob

How much money does each player start with? $1000

Alice:  $1,000.00		Bob:  $1,000.00
Hand #1				Tie Game!

How much is Alice betting?  $50

Did Alice split or double down this hand?  (Y or N)
N
Your bet has not been changed.

Did Alice Win, Lose, or Push?  (W, L or P) W
Alice has won $50.00  Press a key for the next hand.
```

## 🎮 Game Controls

- **Betting**: Enter any amount between minimum bet ($0.01) and your current balance
- **Split/Double Down**: Type 'Y' to double the bet, 'N' to keep original bet
- **Hand Result**: 
  - Type 'W' for Win
  - Type 'L' for Lose
  - Type 'P' for Push (tie)

## 🛠️ Technical Details

- **Language**: C#
- **Framework**: .NET Framework 4.8.1
- **Architecture**: Single-file console application
- **Currency Handling**: Uses `decimal` type for precise monetary calculations
- **Input Processing**: Culture-aware number parsing with currency symbol support

## 📝 Code Structure

The application uses a clean, method-driven structure:

- `InitializePlayers()` - Sets up player names and starting balances
- `BetLoop()` - Main game loop
- `GetBet()` - Validates and captures bet amounts
- `HandleSplitOrDouble()` - Processes double-down scenarios
- `ProcessHandResult()` - Updates balances based on hand outcome
- `CheckBankruptcy()` - Detects game-ending conditions
- `ScoreBlock()` - Displays current game state

## 🎯 Future Enhancement Ideas

- Support for more than 2 players
- Blackjack payout (1.5x) support
- Hand history logging
- Save/load game sessions
- Statistics tracking (win rate, average bet, etc.)
- Insurance bet support
- Configurable house rules

## 👨‍💻 Author

**Charles Martin**

## 📄 License

This project is open source and available for personal and educational use.

## 🤝 Contributing

Contributions, issues, and feature requests are welcome! Feel free to check the issues page.

---

*Enjoy your BlackJack sessions with easy chip tracking!* 🃏💰

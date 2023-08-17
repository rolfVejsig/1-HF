class Program
{
    // Define a list of all possible Yatzy categories
    private static List<string> categories = new List<string>
    {
        "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes",
        "Three of a Kind", "Four of a Kind", "Full House",
        "Small Straight", "Large Straight", "Yatzy", "Chance", "Bonus"
    };

    static void Main(string[] args)
    {
        // Define game constants
        int numDice = 5;
        int numRolls = 3;
        int numRounds = 13;

        // Prompt user for number of players
        Console.Write("Enter the number of players: ");
        int numPlayers = int.Parse(Console.ReadLine());

        // Initialize a list of players based on input names
        List<Player> players = new List<Player>();
        for (int i = 0; i < numPlayers; i++)
        {
            Console.Write("Enter player's name: ");
            string playerName = Console.ReadLine();
            players.Add(new Player(playerName));
        }

        // Loop through each round of the game
        for (int roundNum = 1; roundNum <= numRounds; roundNum++)
        {
            Console.WriteLine($"\nRound {roundNum}:");
            foreach (var player in players)
            {
                Console.WriteLine($"\n{player.Name}'s turn:");
                var dice = RollDice(numDice);
                Console.Write("Your dice: ");
                PrintDice(dice);

                // Allow players to re-roll dice
                for (int j = 0; j < numRolls - 1; j++)
                {
                    List<int> diceToReroll = GetDiceToReRoll();
                    if (!diceToReroll.Any())
                        break;
                    foreach (var idx in diceToReroll)
                    {
                        dice[idx] = RollDice(1).First();
                    }
                    Console.Write("Your dice after re-roll: ");
                    PrintDice(dice);
                }

                // Determine which categories the player can select from
                var availableCategories = EligibleCategories(dice).Where(cat => !player.Scorecard.ContainsKey(cat)).ToList();
                if (!availableCategories.Any())
                {
                    Console.WriteLine("No eligible categories for this dice combination.");
                    continue;
                }
                string categoryChoice = availableCategories[GetCategoryChoice(availableCategories)];
                int score = CalculateScore(dice, categoryChoice);
                player.Scorecard[categoryChoice] = score;
                Console.WriteLine($"Your score for {categoryChoice}: {score}");
            }
        }

        // Display final scores for all players
        Console.WriteLine("\nFinal Scores:");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.Name}:");
            int bonusScore = CheckForBonus(player.Scorecard);
            player.Scorecard["Bonus"] = bonusScore;

            int totalScore = player.Scorecard.Values.Sum();
            foreach (var entry in player.Scorecard)
            {
                Console.WriteLine($"    {entry.Key}: {entry.Value}");
            }
            Console.WriteLine($"    Total Score: {totalScore}");
        }
    }

    // Simulate rolling dice
    static List<int> RollDice(int numDice)
    {
        Random rand = new Random();
        return Enumerable.Range(0, numDice).Select(_ => rand.Next(1, 7)).ToList();
    }

    // Print the current state of the dice
    static void PrintDice(List<int> dice)
    {
        Console.WriteLine(string.Join(" ", dice));
    }

    // Prompt user for dice indices they wish to re-roll
    static List<int> GetDiceToReRoll()
    {
        while (true)
        {
            Console.Write("Enter dice indices to re-roll (space-separated), or 'none': ");
            string reroll = Console.ReadLine();
            if (reroll.ToLower() == "none")
                return new List<int>();
            try
            {
                var indices = reroll.Split().Select(int.Parse).ToList();
                if (indices.All(idx => 1 <= idx && idx <= 5))
                    return indices.Select(idx => idx - 1).ToList();
            }
            catch
            {
                Console.WriteLine("Invalid input. Please enter indices between 1 and 5, or 'none'.");
            }
        }
    }

    // Determine which categories can be selected for the current dice roll
    static List<string> EligibleCategories(List<int> dice)
    {
        return categories.Where(category => CalculateScore(dice, category) > 0).ToList();
    }

    // Prompt user to select a category for scoring
    static int GetCategoryChoice(List<string> availableCategories)
    {
        while (true)
        {
            Console.WriteLine("\nAvailable categories:");
            for (int i = 0; i < availableCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {availableCategories[i]}");
            }
            try
            {
                int choice = int.Parse(Console.ReadLine());
                if (1 <= choice && choice <= availableCategories.Count)
                    return choice - 1;
            }
            catch
            {
                Console.WriteLine("Invalid choice. Please select a valid category number.");
            }
        }
    }

    // Calculate the score for a given dice roll and category choice
    static int CalculateScore(List<int> dice, string category)
    {
        var diceCounts = Enumerable.Range(1, 6).Select(i => dice.Count(d => d == i)).ToList();

        switch (category)
        {
            case "Ones":
                return diceCounts[0];
            case "Twos":
                return diceCounts[1] * 2;
            case "Threes":
                return diceCounts[2] * 3;
            case "Fours":
                return diceCounts[3] * 4;
            case "Fives":
                return diceCounts[4] * 5;
            case "Sixes":
                return diceCounts[5] * 6;
            case "Three of a Kind":
                for (int i = 0; i < 6; i++)
                {
                    if (diceCounts[i] >= 3)
                        return (i + 1) * 3;
                }
                break;
            case "Four of a Kind":
                for (int i = 0; i < 6; i++)
                {
                    if (diceCounts[i] >= 4)
                        return (i + 1) * 4;
                }
                break;
            case "Full House":
                if (diceCounts.Contains(3) && diceCounts.Contains(2))
                    return dice.Sum();
                break;
            case "Small Straight":
                if (new[] { 1, 2, 3, 4 }.All(x => dice.Contains(x)) ||
                    new[] { 2, 3, 4, 5 }.All(x => dice.Contains(x)) ||
                    new[] { 3, 4, 5, 6 }.All(x => dice.Contains(x)))
                    return 15;
                break;
            case "Large Straight":
                if (new[] { 1, 2, 3, 4, 5 }.All(x => dice.Contains(x)) ||
                    new[] { 2, 3, 4, 5, 6 }.All(x => dice.Contains(x)))
                    return 20;
                break;
            case "Yatzy":
                if (diceCounts.Contains(5))
                    return 50;
                break;
            case "Chance":
                return dice.Sum();
            case "Bonus":
                break;
        }
        return 0;
    }

    // Determine if a player's scorecard qualifies for a bonus
    static int CheckForBonus(Dictionary<string, int> scorecard)
    {
        var upperScores = new List<string> { "Ones", "Twos", "Threes", "Fours", "Fives", "Sixes" };
        if (upperScores.All(scorecard.ContainsKey) && upperScores.Sum(cat => scorecard[cat]) >= 63)
            return 50;
        return 0;
    }
}

// Player class to manage each player's details and scorecard
class Player
{
    public string Name { get; }
    public Dictionary<string, int> Scorecard { get; }

    public Player(string name)
    {
        Name = name;
        Scorecard = new Dictionary<string, int>();
    }
}

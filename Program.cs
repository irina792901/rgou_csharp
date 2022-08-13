//begin
Console.Clear();
Console.CursorVisible = false;

//board
Console.WriteLine("0 0");
Console.WriteLine();
Console.WriteLine("■■■■  ■■");
Console.WriteLine("■■■■■■■■");
Console.WriteLine("■■■■  ■■");
Console.WriteLine();
Console.WriteLine("0 0");

//path
int[,] compPath = {{4,2}, {3,2}, {2,2}, {1,2}, {0,2},
                   {0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3},
                   {7,2}, {6,2}};

int[,] userPath = {{4,4}, {3,4}, {2,4}, {1,4}, {0,4},
                   {0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3},
                   {7,4}, {6,4}};


//play
int Dice()
{
    return new Random().Next(1, 5);
}

var key = Console.ReadKey().Key;

if (key == ConsoleKey.Enter)
{
    int die1 = Dice();
    int die2 = Dice();
    int dice = die1 + die2;
    int x = compPath[dice, 0];
    int y = compPath[dice, 1];

    string compDice = $"{die1} {die2}";
    Console.SetCursorPosition(0, 0);
    Console.Write(compDice);
    
    Console.SetCursorPosition(x, y);
    Console.Write("+");
}

//end
Console.SetCursorPosition(0, 8);
Console.CursorVisible = true;
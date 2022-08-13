//begin
Console.Clear();
Console.CursorVisible = false;

//board
Console.WriteLine("0 0 -");
Console.WriteLine();
Console.WriteLine("■■■■  ■■");
Console.WriteLine("■■■■■■■■");
Console.WriteLine("■■■■  ■■");
Console.WriteLine();
Console.WriteLine("0 0 +");

string square = "■";
string programPiece = "-";
string playerPiece = "+";

//path
//int[] programInitialPosition = {4,0};
int programCurrentPositionIndex = 0;
int[,] programPath = {{4,0},
{3,2}, {2,2}, {1,2}, {0,2},
{0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3},
{7,2}, {6,2}};

//int[] playerInitialPosition = {4, 6};
int playerCurrentPositionIndex = 0;
int[,] playerPath = {{4,6},
{3,4}, {2,4}, {1,4}, {0,4},
{0,3}, {1,3}, {2,3}, {3,3}, {4,3}, {5,3}, {6,3}, {7,3},
{7,4}, {6,4}};

//rules
bool gameOver = false;
bool playerWon = false;
bool playerTurn = true;

int[] dice = {0, 0};
int moveLength;

int R()
{
    return new Random().Next(1, 3);
}

void CastDice(bool playerTurn)
{
    dice[0] = R();
    dice[1] = R();
    moveLength = dice[0] + dice[1];

    if(playerTurn)
    {
        Console.SetCursorPosition(0, 6);
    }
    else
    {
        Console.SetCursorPosition(0, 0);
    }
    
    Console.WriteLine($"{dice[0]} {dice[1]}");
}

bool CheckGameOver(bool playerTurn)
{
    if(playerTurn)
    {
        if (playerCurrentPositionIndex + moveLength >= playerPath.GetLength(0)) return true;
        else return false;
    }
    else
    {
        if (programCurrentPositionIndex + moveLength >= programPath.GetLength(0)) return true;
        else return false;
    }
}

void Move(bool playerTurn)
{
    if (playerTurn)
    {
        Console.SetCursorPosition(playerPath[playerCurrentPositionIndex, 0], playerPath[playerCurrentPositionIndex, 1]);
        Console.Write(square);

        playerCurrentPositionIndex += moveLength;

        Console.SetCursorPosition(playerPath[playerCurrentPositionIndex, 0], playerPath[playerCurrentPositionIndex, 1]);
        Console.Write(playerPiece);
    }
    else
    {
        Console.SetCursorPosition(programPath[programCurrentPositionIndex, 0], programPath[programCurrentPositionIndex, 1]);
        Console.Write(square);

        programCurrentPositionIndex += moveLength;

        Console.SetCursorPosition(programPath[programCurrentPositionIndex, 0], programPath[programCurrentPositionIndex, 1]);
        Console.Write(programPiece);
    }
}

while (!gameOver)
{
    var key = Console.ReadKey().Key;

    if (key == ConsoleKey.Enter)
    {
        if (playerTurn)
        {
            CastDice(true);
            gameOver = CheckGameOver(true);
            if (gameOver)
            {
                playerWon = true;
                break;
            }
            else
            {
                Move(true);
            }
        }
        else
        {
            CastDice(false);
            gameOver = CheckGameOver(false);
            if (gameOver)
            {
                playerWon = false;
                break;
            }
            else
            {
                Move(false);
            }
        }

        playerTurn = !playerTurn;
    }
}

if (playerWon)
{
    Console.SetCursorPosition(playerPath[playerCurrentPositionIndex, 0], playerPath[playerCurrentPositionIndex, 1]);
    Console.Write(square);
    Console.SetCursorPosition(0, 8);
    Console.WriteLine("ВЫ ВЫИГРАЛИ!");
}
else
{
    Console.SetCursorPosition(programPath[programCurrentPositionIndex, 0], programPath[programCurrentPositionIndex, 1]);
    Console.Write(square);
    Console.SetCursorPosition(0, 8);
    Console.WriteLine("Вы проиграли.");
}

//end
Console.CursorVisible = true;
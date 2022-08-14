//board
Console.Clear();

string s = "■"; // square
string programPiece = "o";
string playerPiece = "x";

Console.WriteLine($"0 0 {programPiece}");
Console.WriteLine();
Console.WriteLine($"{s}{s}{s}{s}" + "  " + $"{s}{s}");
Console.WriteLine($"{s}{s}{s}{s}" + $"{s}{s}" + $"{s}{s}");
Console.WriteLine($"{s}{s}{s}{s}" + "  " + $"{s}{s}");
Console.WriteLine();
Console.WriteLine($"0 0 {playerPiece}");

//path
int axis = 3;

int[,] programPath =
{
    {4,0},
    {3, axis - 1}, {2, axis - 1}, {1, axis - 1}, {0, axis - 1},
    {0, axis}, {1, axis}, {2, axis}, {3, axis}, {4, axis}, {5, axis},{6, axis}, {7, axis},
    {7, axis - 1}, {6, axis - 1}
};
int programIndex = 0;

int[,] playerPath =
{
    {4,6},
    {3, axis + 1}, {2, axis + 1}, {1, axis + 1},{0, axis + 1},
    {0, axis}, {1, axis}, {2, axis}, {3, axis}, {4, axis}, {5, axis},{6, axis}, {7, axis},
    {7, axis + 1}, {6, axis + 1}
};
int playerIndex = 0;

//utility
void I(int x, int y, string text) // Insert
{
    Console.SetCursorPosition(x, y);
    Console.WriteLine(text);
}

void IPlayer(string text)
{
    I(playerPath[playerIndex, 0], playerPath[playerIndex, 1], text);
}

void IProgram(string text)
{
    I(programPath[programIndex, 0], programPath[programIndex, 1], text);
}

//rules
bool gameOver = false;
bool playerTurn = true;

int[] dice = {0, 0};
int moveLength;

void CastDice(bool isPlayer)
{
    dice[0] = new Random().Next(1, 3);
    dice[1] = new Random().Next(1, 3);
    moveLength = dice[0] + dice[1];

    if(isPlayer)
    {
        I(0, 6, $"{dice[0]} {dice[1]}");
    }
    else
    {
        I(0, 0, $"{dice[0]} {dice[1]}");
    }
}

void CheckGameOver(bool isPlayer)
{
    if(isPlayer)
    {
        if (playerIndex + moveLength >= playerPath.GetLength(0)) gameOver = true;
    }
    else
    {
        if (programIndex + moveLength >= programPath.GetLength(0)) gameOver = true;
    }
}

void Move(bool isPlayer)
{
    if (isPlayer)
    {
        if (playerIndex != 0) IPlayer(s);
        else IPlayer(" ");

        playerIndex += moveLength;
        IPlayer(playerPiece);
    }
    else
    {
        if (programIndex != 0) IProgram(s);
        else IProgram(" ");

        programIndex += moveLength;
        IProgram(programPiece);
    }
}

void SendBack(bool isPlayer)
{
    if (isPlayer)
    {
        if (programIndex >= 5 && programIndex <= 12)
        {
            if (playerIndex + moveLength == programIndex)
            {
                IProgram(s);
                programIndex = 0;
                IProgram(programPiece);
            }
        }
    }
    else
    {
        if (playerIndex >= 5 && playerIndex <= 12)
        {
            if (programIndex + moveLength == playerIndex)
            {
                IPlayer(s);
                playerIndex = 0;
                IPlayer(playerPiece);
            }
        }
    }
}

void GameOver(bool isPlayer)
{
    if (isPlayer)
    {
        IPlayer(s);
        I(0, 8, "ВЫ ВЫИГРАЛИ!");
    }
    else
    {
        IProgram(s);
        I(0, 8, "Вы проиграли.");
    }
}

//go!
while (!gameOver)
{
    Console.SetCursorPosition(0, 8);

    var key = Console.ReadKey().Key;

    if (key == ConsoleKey.Enter)
    {
        if (playerTurn)
        {
            CastDice(true);
            CheckGameOver(true);
            SendBack(true);

            if (!gameOver) Move(true);
            else GameOver(true);
        }
        else
        {
            CastDice(false);
            CheckGameOver(false);
            SendBack(false);

            if (!gameOver) Move(false);
            else GameOver(false);
        }

        playerTurn = !playerTurn;
    }
}
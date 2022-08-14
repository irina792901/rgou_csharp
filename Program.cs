//board
Console.Clear();

string square = "■";
string programPiece = "o";
string playerPiece = "x";

Console.WriteLine($"0 0 0 0 {programPiece}");
Console.WriteLine();
Console.WriteLine($"{square}{square}{square}{square}  {square}{square}");
Console.WriteLine($"{square}{square}{square}{square}{square}{square}{square}{square}");
Console.WriteLine($"{square}{square}{square}{square}  {square}{square}");
Console.WriteLine();
Console.WriteLine($"0 0 0 0 {playerPiece}");

//path
int axis = 3;

int[,] programPath =
{
    {8,0},
    {3, axis - 1}, {2, axis - 1}, {1, axis - 1}, {0, axis - 1},
    {0, axis}, {1, axis}, {2, axis}, {3, axis}, {4, axis}, {5, axis},{6, axis}, {7, axis},
    {7, axis - 1}, {6, axis - 1}
};
int programIndex = 0;

int[,] playerPath =
{
    {8,6},
    {3, axis + 1}, {2, axis + 1}, {1, axis + 1},{0, axis + 1},
    {0, axis}, {1, axis}, {2, axis}, {3, axis}, {4, axis}, {5, axis},{6, axis}, {7, axis},
    {7, axis + 1}, {6, axis + 1}
};
int playerIndex = 0;

int[] extraRollSquare = {4, 8, 14};
int safeSquare = 8;

//utility
void Ins(int x, int y, string text)
{
    Console.SetCursorPosition(x, y);
    Console.WriteLine(text);
}

void InsPlayer(string text)
{
    Ins(playerPath[playerIndex, 0], playerPath[playerIndex, 1], text);
}

void InsProgram(string text)
{
    Ins(programPath[programIndex, 0], programPath[programIndex, 1], text);
}

void ClearLine(int y)
{
    Console.SetCursorPosition(0, y);
    Console.Write(new string(' ', Console.BufferWidth));
    Console.SetCursorPosition(0, y);
}

//rules
string msg = string.Empty;
bool gameOver = false;
bool playerTurn = true;

int[] dice = {0, 0, 0, 0};
int moveLength = 0;

void CastDice(bool isPlayer)
{
    dice[0] = new Random().Next(0, 4);
    dice[1] = new Random().Next(0, 4);
    dice[2] = new Random().Next(0, 4);
    dice[3] = new Random().Next(0, 4);

    moveLength = 0;
    for(int i = 0; i < dice.Length; i++)
    {
        if (dice[i] != 0)
        {
            dice[i] = 1;
            moveLength++;
        }
    }

    if(isPlayer)
    {
        Ins(0, 6, $"{dice[0]} {dice[1]} {dice[2]} {dice[3]}");
    }
    else
    {
        Ins(0, 0, $"{dice[0]} {dice[1]} {dice[2]} {dice[3]}");
    }
}

void CheckGameOver(bool isPlayer)
{
    if(isPlayer)
    {
        if (playerIndex + moveLength == playerPath.GetLength(0)) gameOver = true;
    }
    else
    {
        if (programIndex + moveLength == programPath.GetLength(0)) gameOver = true;
    }
}

bool SkipMove(bool isPlayer)
{
    if(isPlayer)
    {
        if (playerIndex + moveLength > playerPath.GetLength(0)) 
        {
            msg += "Вы пропускаете ход. ";

            return true;
        }
        else return false;
    }
    else
    {
        if (programIndex + moveLength > programPath.GetLength(0)) 
        {
            msg += "Программа пропускает ход. ";

            return true;
        }
        else return false;
    }
}

void SendBack(bool isPlayer)
{
    if (isPlayer)
    {
        if (programIndex >= 5 && programIndex <= 12 && playerIndex + moveLength == programIndex)
        {
            if (programIndex == safeSquare)
            {
                msg += "Фишка противника на безопасной клетке и не может быть убрана. Ваша фишка перепрыгнула фишку противника. ";
                moveLength++;
            }
            else
            {
                ClearLine(8);
                Ins(0, 8, "Убрать фишку противника или перепрыгнуть? Backspace - убрать, Enter - перепрыгнуть. ");

                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Backspace)
                {
                    InsProgram(square);
                    programIndex = 0;
                    InsProgram(programPiece);

                    msg += "Вы убрали фишку противника с доски. ";
                }
                else if (key == ConsoleKey.Enter)
                {
                    moveLength++;

                    msg += "Ваша фишка перепрыгнула фишку противника. ";
                }
                else ClearLine(9);
                
            }
        }
    }
    else
    {
        if (playerIndex >= 5 && playerIndex <= 12 && programIndex + moveLength == playerIndex)
        {
            if (playerIndex == safeSquare)
            {
                msg += "Ваша фишка на безопасной клетке и не может быть убрана. Фишка противника перепрыгнула вашу фишку. ";
            }
            else
            {
                if (new Random().Next(2) == 1)
                {
                    InsPlayer(square);
                    playerIndex = 0;
                    InsPlayer(playerPiece);
                    msg += "Противник убрал вашу фишку с доски. ";
                }
                else
                {
                    moveLength++;
                    msg += "Фишка противника перепрыгнула вашу фишку. ";
                }
                
            }
        }
    }
}

string Decline()
{
    if (moveLength >= 5) return "клеток";
    else if (moveLength == 1) return "клетку";
    else return "клетки";
}

void Move(bool isPlayer)
{
    if (isPlayer)
    {
        if (playerIndex != 0) InsPlayer(square);
        else InsPlayer(" ");

        playerIndex += moveLength;
        InsPlayer(playerPiece);

        msg += $"Вы продвинулись вперёд на {moveLength} {Decline()}. ";
    }
    else
    {
        if (programIndex != 0) InsProgram(square);
        else InsProgram(" ");

        programIndex += moveLength;
        InsProgram(programPiece);

        msg += $"Программа продвинулась вперёд на {moveLength} {Decline()}. ";
    }
}

void GameOver(bool isPlayer)
{
    if (isPlayer)
    {
        InsPlayer(square);
        msg += "ВЫ ВЫИГРАЛИ! ";
    }
    else
    {
        InsProgram(square);
        msg += "Вы проиграли. ";
    }
}

bool ExtraRoll(bool isPlayer)
{
    int currentIndex;
    if (isPlayer) currentIndex = playerIndex;
    else currentIndex = programIndex;

    for (int i = 0; i < extraRollSquare.Length; i++)
    {
        if (currentIndex == extraRollSquare[i])
        {
            if (isPlayer) msg += "У вас есть дополнительный ход. ";
            else msg += "У программы есть дополнительный ход. ";
            return true;
        }
    }

    return false;
}

//go!
while (!gameOver)
{
    msg = "";

    Console.SetCursorPosition(0,9);

    var key = Console.ReadKey().Key;
    if (key == ConsoleKey.Enter)
    {
        CastDice(playerTurn);
        CheckGameOver(playerTurn);
        SendBack(playerTurn);
        bool skip = SkipMove(playerTurn);

        if (!gameOver && !skip) Move(playerTurn);
        else if (gameOver) GameOver(playerTurn);

        bool extra;
        if (skip || gameOver) extra = false;
        else extra = ExtraRoll(playerTurn);
        if (!extra) playerTurn = !playerTurn;

        ClearLine(8);
        Ins(0, 8, msg);
    }
    else if (key == ConsoleKey.Escape) break;
}
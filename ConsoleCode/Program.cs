using System;
using System.Linq;
using static System.Formats.Asn1.AsnWriter;

Game Game = new Game();
class Game
{
    Field field;
    int maxScore;
    public Game()
    {
        maxScore = 0;
        while (true)
        {
            start();
        }
    }
    public void start()
    {
        field = new Field();
        Show();
        while (AnyPossible(field))
        {
            var k = Console.ReadKey().Key;
            field.ControlBlock(k);
            MaxScoreCheck();
            Show();
        }
        Console.WriteLine("Game over!");
    }
    private void Show()
    {
        Console.Clear();
        Console.WriteLine("Score: " + field._score);
        Console.WriteLine("Max Score: " + maxScore);
        field.Show();
    }
    private void MaxScoreCheck()
    {
        if(field._score>maxScore)
            maxScore = field._score;
    }

    public bool AnyPossible(Field f)
    {
        if (f.IsEmptyCell())
            return true;
        else
        {
            Field f4Check = f;
            f4Check._moveIsDoing = false;
            f4Check.Right();
            if(f4Check._moveIsDoing)
                    return true;
            f4Check = f;
            f4Check.Left();
            if (f4Check._moveIsDoing)
                return true;
            f4Check = f;
            f4Check.Up();
            if (f4Check._moveIsDoing)
                return true;
            f4Check = f;
            f4Check.Down();
            if (f4Check._moveIsDoing)
                return true;
            f4Check = f;
        }
        return false;
    }
}
class Field
{
    private const int SIZE = 4;
    private int[,] _field;

    private int _col;
    private int _row;
    public int _score;
    public bool _moveIsDoing;

    private Random rnd;

    public Field()
    {
        rnd = new Random();
        _field = new int[SIZE, SIZE];
        _score = 0;
        _moveIsDoing = false;
        GetCoordinate(); 
        Spawn(); 
    }
    public void ControlBlock(ConsoleKey key)
    {
        if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A)
            Left();
        else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D)
            Right();
        else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
            Up();
        else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
            Down();
        else
            return;
        if (IsEmptyCell()&& _moveIsDoing)
        {
            GetCoordinate();
            Spawn();
        }
        _moveIsDoing = false;
    }

    public void Right()
    {
        int startCoordiante = 2;
        for (int i = startCoordiante; i >= 0; i--)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (_field[j, i] == 0)
                    continue;
                int doubleI = i;
                while (doubleI < SIZE - 1)
                {
                    if (_field[j, doubleI] != _field[j, doubleI + 1] && _field[j, doubleI + 1] != 0)
                        break;
                    _moveIsDoing = true;

                    if (_field[j, doubleI] == _field[j, doubleI + 1])
                    {
                        _field[j, doubleI + 1] *= 2;
                        _field[j, doubleI] = 0;
                        _score += _field[j, doubleI + 1];
                        break;
                    }
                    else if (_field[j, doubleI + 1] == 0)
                    {
                        int val = _field[j, doubleI];
                        _field[j, doubleI] = _field[j, doubleI + 1];
                        _field[j, doubleI + 1] = val;
                        doubleI++;
                    }
                }
            }
        }
    }

    public void Left()
    {
        int startCoordiante = 1;

        for (int i = startCoordiante; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (_field[j, i] == 0)
                    continue;
                int doubleI = i;
                while (doubleI > 0)
                {
                    if (_field[j, doubleI] != _field[j, doubleI - 1] && _field[j, doubleI - 1] != 0)
                        break;
                    _moveIsDoing = true;
                    if (_field[j, doubleI] == _field[j, doubleI - 1])
                    {
                        _field[j, doubleI - 1] *= 2;
                        _field[j, doubleI] = 0;
                        _score += _field[j, doubleI - 1];
                        break;
                    }
                    else if (_field[j, doubleI - 1] == 0)
                    {
                        int val = _field[j, doubleI];
                        _field[j, doubleI] = _field[j, doubleI - 1];
                        _field[j, doubleI - 1] = val;
                        doubleI--;
                    }
                }
            }
        }
    }

    public void Up()
    {
        int startCoordiante = 1;

        for (int i = startCoordiante; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (_field[i, j] == 0)
                {
                    continue;
                }

                int doubleI = i;

                while (doubleI > 0)
                {
                    if (_field[doubleI, j] != _field[doubleI - 1, j] && _field[doubleI - 1, j] != 0)
                    {
                        break;
                    }

                    _moveIsDoing = true;

                    if (_field[doubleI, j] == _field[doubleI - 1, j])
                    {
                        _field[doubleI - 1, j] *= 2;
                        _field[doubleI, j] = 0;
                        _score += _field[doubleI - 1, j];
                        break;
                    }
                    else if (_field[doubleI - 1, j] == 0)
                    {
                        int val = _field[doubleI, j];
                        _field[doubleI, j] = _field[doubleI - 1, j];
                        _field[doubleI - 1, j] = val;
                        doubleI--;
                    }
                }
            }
        }
    }

    public void Down()
    {
        int startCoordiante = 2;

        for (int i = startCoordiante; i >= 0; i--)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (_field[i, j] == 0)
                {
                    continue;
                }

                int doubleI = i;

                while (doubleI < SIZE - 1)
                {
                    if (_field[doubleI, j] != _field[doubleI + 1, j] && _field[doubleI + 1, j] != 0)
                    {
                        break;
                    }

                    _moveIsDoing = true;

                    if (_field[doubleI, j] == _field[doubleI + 1, j])
                    {
                        _field[doubleI + 1, j] *= 2;
                        _field[doubleI, j] = 0;
                        _score += _field[doubleI + 1, j];
                        break;
                    }
                    else if (_field[doubleI + 1, j] == 0)
                    {
                        int val = _field[doubleI, j];
                        _field[doubleI, j] = _field[doubleI + 1, j];
                        _field[doubleI + 1, j] = val;
                        doubleI++;
                    }
                }
            }
        }
    }

    public bool IsEmptyCell()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {
                if (_field[i, j] == 0)
                    return true;
            }
        }
        return false;
    }

    private void GetCoordinate()
    {
        _col = rnd.Next(SIZE);
        _row = rnd.Next(SIZE);

        if (_field[_col, _row] == 0)
            return;
        for (int i = _col; i < SIZE; i++)
        {
            for (int j = _row; j < SIZE; j++)
            {
                if (_field[i, j] == 0)
                {
                    _col = i;
                    _row = j;
                    return;
                }
            }
        }
        for (int i = _col; i >= 0; i--)
        {
            for (int j = _row; j >= 0; j--)
            {
                if (_field[i, j] == 0)
                {
                    _col = i;
                    _row = j;
                    return;
                }
            }
        }
    }

    private void Spawn()
    {
        if (rnd.Next(100) > 10)
            _field[_col, _row] = 2;
        else
            _field[_col, _row] = 4;
    }

    public void Show()
    {
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = 0; j < SIZE; j++)
            {

                Console.BackgroundColor = valCol[_field[i, j]];
                Console.Write($"[ {_field[i, j]} ]");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.WriteLine("\n------------------");
        }
    }
    static Dictionary<int, ConsoleColor> valCol = new Dictionary<int, ConsoleColor>
    {
        { 2, ConsoleColor.Red },
        { 4, ConsoleColor.Yellow},
        { 8, ConsoleColor.Green },
        { 16, ConsoleColor.Gray },
        { 32, ConsoleColor.Magenta},
        { 64, ConsoleColor.DarkBlue},
        { 128, ConsoleColor.DarkYellow},
        { 256, ConsoleColor.Cyan },
        { 512, ConsoleColor.DarkMagenta },
        { 1024, ConsoleColor.Blue},
        { 2048, ConsoleColor.White},
        { 0, ConsoleColor.Black}
    };

}





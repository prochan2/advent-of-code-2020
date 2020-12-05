using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var ids = new List<int>();

foreach (var line in lines)
{
    ids.Add(GetSeatId(line));
}

WriteLine(ids.Max());

int GetSeatId(string code)
{
    int row = Decode(code[0..7], 'F', 'B');
    int column = Decode(code[7..10], 'L', 'R');
    int id = (row * 8) + column;

    WriteLine($"{code} {row} {column} {id}");

    return id;
}

int Decode(string code, char low, char high)
{
    int min = 0;
    int max = (int)Math.Pow(2, code.Length) - 1;

    foreach (char c in code)
    {
        int mid = (max - min) / 2;

        if (c == low)
            max -= mid + 1;
        else if (c == high)
            min += mid + 1;
        else
            throw new NotImplementedException();
    }

    if (min != max)
        throw new InvalidOperationException();

    return min;
}
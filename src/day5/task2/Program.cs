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

ids.Sort();

foreach (int id in ids)
{
    WriteLine(id);
}

int prev = ids.First();

foreach (int id in ids.Skip(1))
{
    if (id != prev + 1)
    {
        WriteLine(prev + 1);
        return;
    }

    prev = id;
}

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
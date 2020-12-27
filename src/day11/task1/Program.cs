using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput.txt";
string fileName = @"..\..\..\..\input.txt";

var seats = File.ReadAllLines(fileName).Select(l => l.Select(c => c switch
{
    'L' => 0,
    '#' => 1,
    '.' => -1,
    _ => throw new InvalidOperationException()
}).ToArray()).ToArray();

int xLength = seats.Length;
int yLength = seats[0].Length;

while (TryMakeSeats())
{
}

WriteLine(seats.Sum(row => row.Count(seat => seat == 1)));

bool TryMakeSeats()
{
    bool changed = false;
    var newSeats = seats.Select(row => row.Select(seat => seat).ToArray()).ToArray();

    for (int x = 0; x < xLength; x++)
    {
        for (int y = 0; y < yLength; y++)
        {
            if (seats[x][y] == -1)
                continue;

            int adjacentCount = CountAdjacent(x, y);

            if (seats[x][y] == 0 && adjacentCount == 0)
            {
                newSeats[x][y] = 1;
                changed = true;
            }
            else if (seats[x][y] == 1 && adjacentCount >= 4)
            {
                newSeats[x][y] = 0;
                changed = true;
            }
        }
    }

    seats = newSeats;
    return changed;
}

int CountAdjacent(int x, int y)
{
    int adjacentCount = 0;
    adjacentCount += IsOcupied(x - 1, y - 1);
    adjacentCount += IsOcupied(x + 1, y - 1);
    adjacentCount += IsOcupied(x - 1, y + 1);
    adjacentCount += IsOcupied(x + 1, y + 1);
    adjacentCount += IsOcupied(x, y - 1);
    adjacentCount += IsOcupied(x, y + 1);
    adjacentCount += IsOcupied(x - 1, y);
    adjacentCount += IsOcupied(x + 1, y);
    return adjacentCount;
}

int IsOcupied(int x, int y)
{
    if (x < 0 || x >= xLength || y < 0 || y >= yLength)
        return 0;

    return seats[x][y] == 1 ? 1 : 0;
}
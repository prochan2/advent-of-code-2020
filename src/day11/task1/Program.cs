using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

string fileName = @"..\..\..\..\sinput.txt";
//string fileName = @"..\..\..\..\input.txt";

const int adjacencyLength = 8;

var seats = File.ReadAllLines(fileName).Select(l => l.Select(c => c switch
{
    'L' => 0,
    '#' => 1,
    '.' => -1,
    _ => throw new InvalidOperationException()
}).ToArray()).ToArray();

int xLength = seats.Length;
int yLength = seats[0].Length;

var seatCounts = seats.Select(l => l.Select(s => 0).ToArray()).ToArray();

//WriteArray(seats);

TryMakeSeats();

WriteSeats(seats);

while (true)
{
    CountAll();
    bool done = !TryMakeSeats();

    WriteArray(seatCounts);
    WriteSeats(seats);
    ReadLine();
}

//WriteArray(seatCounts);

WriteLine(seats.Sum(s => s.Count(s => s == 1)));

void CountLine(int x, int y, int dx, int dy, bool reverse = false)
{
    int step = 1;
    int sum = 0;

    for (; x >= 0 && x < xLength && y >= 0 && y < yLength; x += dx, y += dy)
    {
        if (seats[x][y] == -1)
            continue;

        seatCounts[x][y] += sum;

        //WriteLine(sum);
        //WriteArray(seatCounts);
        //ReadLine();

        sum += seats[x][y];

        if (step++ <= adjacencyLength)
            continue;

        sum -= seats[x - (adjacencyLength * dx)][y - (adjacencyLength * dy)];
    }

    if (reverse)
        return;

    CountLine(x - dx, y - dy, -dx, -dy, true);
}

void CountAll()
{
    for (int x = 0; x < xLength; x++)
    {
        for (int y = 0; y < yLength; y++)
        {
            seatCounts[x][y] = 0;
        }
    }

    for (int x = 0; x < xLength; x++)
    {
        CountLine(x, 0, 0, 1);
    }

    for (int y = 0; y < yLength; y++)
    {
        CountLine(0, y, 1, 0);
    }

    for (int x = 0; x < xLength; x++)
    {
        CountLine(x, 0, 1, 1);
        CountLine(x, 0, -1, 1);
    }

    for (int y = 1; y < yLength; y++)
    {
        CountLine(0, y, 1, 1);
        CountLine(0, y, -1, 1);
    }
}

bool TryMakeSeats()
{
    bool changed = false;

    for (int x = 0; x < xLength; x++)
    {
        for (int y = 0; y < yLength; y++)
        {
            switch ((seats[x][y], seatCounts[x][y]))
            {
                case (0, 0):
                    seats[x][y] = 1;
                    changed = true;
                    break;
                case (1, >= 4):
                    seats[x][y] = 0;
                    changed = true;
                    break;
            }
        }
    }

    return changed;
}

void WriteArray(int[][] array)
{
    for (int x = 0; x < xLength; x++)
    {
        for (int y = 0; y < yLength; y++)
        {
            Write(array[x][y]);
            Write(" ");
        }
        WriteLine();
    }

    WriteLine();
}

void WriteSeats(int[][] array)
{
    for (int x = 0; x < xLength; x++)
    {
        for (int y = 0; y < yLength; y++)
        {
            Write(array[x][y] switch
            {
                0 => 'L',
                1 => '#',
                -1 => '.',
                _ => throw new InvalidOperationException()
            });
            Write(" ");
        }
        WriteLine();
    }

    WriteLine();
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput.txt";
string fileName = @"..\..\..\..\input.txt";

int xDir = 1;
int yDir = 0;
int x = 0;
int y = 0;

foreach (var line in File.ReadLines(fileName))
{
    char action = line[0];
    int argument = int.Parse(line[1..]);

    switch (action)
    {
        case 'N':
            y += argument;
            break;

        case 'S':
            y -= argument;
            break;

        case 'E':
            x += argument;
            break;

        case 'W':
            x -= argument;
            break;

        case 'L':
            for (int i = 0; i < argument; i += 90)
                TurnLeft();
            break;

        case 'R':
            for (int i = 0; i < argument; i += 90)
                TurnRight();
            break;

        case 'F':
            x += xDir * argument;
            y += yDir * argument;
            break;

        default:
            throw new InvalidOperationException();
    }

    //WriteLine($"{x} {y} | {xDir} {yDir}");
}

WriteLine(Math.Abs(x) + Math.Abs(y));

void TurnRight()
{
    (xDir, yDir) = (xDir, yDir) switch
    {
        (1, 0) => (0, -1),
        (0, -1) => (-1, 0),
        (-1, 0) => (0, 1),
        (0, 1) => (1, 0),
        _ => throw new InvalidOperationException()
    };
}

void TurnLeft()
{
    TurnRight();
    TurnRight();
    TurnRight();
}
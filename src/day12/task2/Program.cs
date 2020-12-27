using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput.txt";
string fileName = @"..\..\..\..\input.txt";

int x = 0;
int y = 0;
int wpX = 10;
int wpY = 1;

foreach (var line in File.ReadLines(fileName))
{
    char action = line[0];
    int argument = int.Parse(line[1..]);

    switch (action)
    {
        case 'N':
            wpY += argument;
            break;

        case 'S':
            wpY -= argument;
            break;

        case 'E':
            wpX += argument;
            break;

        case 'W':
            wpX -= argument;
            break;

        case 'L':
            for (int i = 0; i < argument; i += 90)
                (wpX, wpY) = (-wpY, wpX);
            break;

        case 'R':
            for (int i = 0; i < argument; i += 90)
                (wpX, wpY) = (wpY, -wpX);
            break;

        case 'F':
            x += wpX * argument;
            y += wpY * argument;
            break;

        default:
            throw new InvalidOperationException();
    }

    //WriteLine($"{line}: {x} {y} | {wpX} {wpY}");
}

WriteLine(Math.Abs(x) + Math.Abs(y));
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput.txt";
string fileName = @"..\..\..\..\input.txt";

var input = File.ReadLines(fileName);

var arrivalToAirport = long.Parse(input.First());

var lines = input.Skip(1).First().Split(',').Where(line => line != "x").Select(line => long.Parse(line));

long minWaitingTime = long.MaxValue;
long earliestLine = long.MaxValue;

foreach (var line in lines)
{
    var waitingTime = line - (arrivalToAirport % line);
    if (waitingTime < minWaitingTime)
    {
        minWaitingTime = waitingTime;
        earliestLine = line;
    }
}

WriteLine($"{earliestLine} {minWaitingTime} {earliestLine * minWaitingTime}");
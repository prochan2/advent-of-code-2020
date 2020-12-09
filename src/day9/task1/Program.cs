using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput.txt";
//var preambleLength = 5;

string fileName = @"..\..\..\..\input.txt";
var preambleLength = 25;

var lines = File.ReadAllLines(fileName);
var numbers = lines.Select(l => long.Parse(l)).ToArray();

long answer = long.MinValue;

for (int i = preambleLength; i < numbers.Length; i++)
{
    if (!HasSum(i))
    {
        answer = numbers[i];
        break;
    }
}

WriteLine(answer);

bool HasSum(int i)
{
    var n = numbers[i];

    for (int j = 0; j < preambleLength; j++)
    {
        for (int k = 0; k < preambleLength; k++)
        {
            if (j == k)
                continue;

            if (numbers[i - j - 1] + numbers[i - k - 1] == n)
                return true;
        }
    }

    return false;
}
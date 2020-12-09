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

long answer1 = long.MinValue;

for (int i = preambleLength; i < numbers.Length; i++)
{
    if (!HasSum(i))
    {
        answer1 = numbers[i];
        break;
    }
}

int first;
int last = int.MinValue;

for (first = 0; first < numbers.Length; first++)
{
    if (TryFindSequence(answer1, first, out last))
        break;
}

var sortedSequence = numbers[first..(last + 1)];
Array.Sort(sortedSequence);

var answer2 = sortedSequence[0];
var answer3 = sortedSequence[^1];
var answer4 = answer2 + answer3;

WriteLine(answer2);
WriteLine(answer3);
WriteLine(answer4);

bool HasSum(int i)
{
    var n = numbers[i];

    for (int j = 0; j < preambleLength; j++)
    {
        for (int k = j + 1; k < preambleLength; k++)
        {
            if (numbers[i - j - 1] + numbers[i - k - 1] == n)
                return true;
        }
    }

    return false;
}

bool TryFindSequence(long expectedSum, int first, out int last)
{
    int i = first;

    var n1 = numbers[i];
    long sum = n1;

    for (int j = i + 1; j < numbers.Length; j++)
    {
        var n2 = numbers[j];
        sum += n2;

        if (sum > expectedSum)
            break;

        if (sum == expectedSum)
        {
            last = j;
            return true;
        }
    }

    last = int.MinValue;
    return false;
}
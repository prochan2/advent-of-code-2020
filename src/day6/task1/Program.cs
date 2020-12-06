using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

HashSet<char> group = new HashSet<char>();
int sum = 0;

foreach (var line in lines)
{
    if (line == "")
    {
        sum += group.Count;
        group.Clear();
    }

    foreach (char c in line)
        group.Add(c);
}

sum += group.Count;

WriteLine(sum);
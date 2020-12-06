using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var group = new HashSet<char>();
int sum = 0;

foreach (var line in lines)
{
    if (line == "")
    {
        sum += group.Count;
        group.Clear();
        continue;
    }

    group.UnionWith(line.ToHashSet());
}

sum += group.Count;

WriteLine(sum);
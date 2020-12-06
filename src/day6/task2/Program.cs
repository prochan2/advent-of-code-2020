using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

// This is less efficient than the previous implementation, but I wanted to try this way.
var init = Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i).ToHashSet();
var group = init.ToHashSet();
int sum = 0;

foreach (var line in lines)
{
    WriteLine(line);

    if (line == "")
    {
        WriteLine(group.Count);

        sum += group.Count;
        group = init.ToHashSet();
        continue;
    }

    group.IntersectWith(line.ToHashSet());
}

sum += group.Count;

WriteLine(sum);
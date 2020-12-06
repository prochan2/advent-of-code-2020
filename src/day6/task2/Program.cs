using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

HashSet<char> group = new HashSet<char>();
int sum = 0;
bool @new = true;

foreach (var line in lines)
{
    WriteLine(line);

    if (line == "")
    {
        WriteLine(group.Count);

        sum += group.Count;
        group.Clear();
        @new = true;
        continue;
    }

    HashSet<char> person = line.ToHashSet();

    if (@new)
    {
        group = person;
        @new = false;
    }
    else
    {
        group.IntersectWith(person);
    }
}

sum += group.Count;

WriteLine(sum);
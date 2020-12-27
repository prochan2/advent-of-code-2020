using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput1.txt";
//string fileName = @"..\..\..\..\sinput2.txt";
string fileName = @"..\..\..\..\input.txt";

var adapters = File.ReadAllLines(fileName).Select(l => int.Parse(l)).ToList();
adapters.Add(0);
adapters.Sort();
adapters.Add(adapters.Last() + 3);

var counts = adapters.Select(a => 0L).ToArray();
counts[0] = 1L;

for (int i = 0; i < adapters.Count; i++)
{
    for (int j = i + 1; j < adapters.Count; j++)
    {
        if (adapters[j] - adapters[i] <= 3)
            counts[j] += counts[i];
        else
            break;
    }
}

WriteLine(counts.Last());
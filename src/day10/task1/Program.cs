using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput1.txt";
string fileName = @"..\..\..\..\input.txt";
var joltageDifferences = new HashSet<long> { 1, 2, 3 };

var lines = File.ReadAllLines(fileName);
var adapters = lines.Select(l => long.Parse(l)).ToList();

adapters.Sort();

for (int i = 0; i < adapters.Count; i++)
{
    var path = FindPath(i);

    if (path != null)
    {
        path.Reverse();

        var previousAdapter = 0L;
        var distributions = joltageDifferences.ToDictionary(d => d, d => 0L);

        WriteLine("Used adapters:");
        foreach (var usedAdapter in path)
        {
            WriteLine(usedAdapter);
            distributions[usedAdapter - previousAdapter]++;
            previousAdapter = usedAdapter;
        }

        distributions[3]++; // internal adapter

        WriteLine();
        WriteLine("Joltage distributions:");
        foreach (var kv in distributions)
        {
            WriteLine($"{kv.Key} {kv.Value}");
        }

        WriteLine();
        WriteLine("Answer:");
        WriteLine(distributions[1] * distributions[3]);

        return;
    }
}

WriteLine("Not found.");

List<long> FindPath(int currentIndex)
{
    var currentJoltage = adapters[currentIndex];

    if (currentIndex == adapters.Count - 1)
        return new List<long> { currentJoltage };

    for (int i = 1; i <= joltageDifferences.Count; i++)
    {
        var nextIndex = currentIndex + i;

        if (nextIndex == adapters.Count)
            return null;

        var nextJoltage = adapters[currentIndex + i];
        var diff = nextJoltage - currentJoltage;

        if (!joltageDifferences.Contains(diff))
            continue;

        var result = FindPath(nextIndex);

        if (result != null)
        {
            result.Add(currentJoltage);
            return result;
        }
    }

    return null;
}


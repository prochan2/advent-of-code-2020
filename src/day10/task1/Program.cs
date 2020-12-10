using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

//string fileName = @"..\..\..\..\sinput1.txt";
string fileName = @"..\..\..\..\input.txt";
var joltageDifferences = new long[] { 1, 2, 3 };

var lines = File.ReadAllLines(fileName);
var adapters = lines.Select(l => long.Parse(l)).ToHashSet();

if (adapters.Count != lines.Length)
    throw new NotImplementedException();

foreach (var adapter in adapters)
{
    var remainingAdapters = adapters.ToHashSet();
    remainingAdapters.Remove(adapter);    

    var usedAdapters = GetDistributions(adapter, remainingAdapters);

    if (usedAdapters != null)
    {
        usedAdapters.Add(adapter);
        usedAdapters.Reverse();

        var previousAdapter = 0L;
        var distributions = joltageDifferences.ToDictionary(d => d, d => 0L);

        WriteLine("Used adapters:");
        foreach (var usedAdapter in usedAdapters)
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

List<long> GetDistributions(long currentAdapter, HashSet<long> remainingAdapters)
{
    foreach (var joltageDifference in joltageDifferences)
    {
        var nextAdapter = currentAdapter + joltageDifference;

        if (!remainingAdapters.Contains(nextAdapter))
            continue;

        var nextRemainingAdapters = remainingAdapters.ToHashSet();
        nextRemainingAdapters.Remove(nextAdapter);


        if (nextRemainingAdapters.Count == 0)
        {
            return new List<long> { nextAdapter };
        }

        var result = GetDistributions(nextAdapter, nextRemainingAdapters);

        if (result != null)
        {
            result.Add(nextAdapter);
            return result;
        }
    }

    return null;
}


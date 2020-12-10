using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

string fileName = @"..\..\..\..\sinput1.txt";
//string fileName = @"..\..\..\..\input.txt";
var joltageDifferences = new long[] { 1, 2, 3 };

var lines = File.ReadAllLines(fileName);
var adapters = lines.Select(l => long.Parse(l)).ToHashSet();

if (adapters.Count != lines.Length)
    throw new NotImplementedException();

var emptyDistributions = joltageDifferences.ToDictionary(d => d, d => 0L);

foreach (var adapter in adapters)
{
    var remainingAdapters = adapters.ToHashSet();
    remainingAdapters.Remove(adapter);    

    var answer = GetDistributions(adapter, remainingAdapters, emptyDistributions);

    if (answer != (null, null))
    {
        answer.Item1.Add(adapter);
        answer.Item2[3]++; // internal adapter

        answer.Item1.Reverse();

        WriteLine("Used adapters:");
        foreach (var usedAdapter in answer.Item1)
        {
            WriteLine(usedAdapter);
        }

        WriteLine();
        WriteLine("Joltage distributions:");
        foreach (var kv in answer.Item2)
        {
            WriteLine($"{kv.Key} {kv.Value}");
        }
        return;
    }
}

WriteLine("Not found.");

(List<long>, Dictionary<long, long>) GetDistributions(long currentAdapter, HashSet<long> remainingAdapters, Dictionary<long, long> currentDistributions)
{
    foreach (var joltageDifference in joltageDifferences)
    {
        var nextAdapter = currentAdapter + joltageDifference;

        if (!remainingAdapters.Contains(nextAdapter))
            continue;

        var nextRemainingAdapters = remainingAdapters.ToHashSet();
        nextRemainingAdapters.Remove(nextAdapter);

        var nextCurrentDistributions = currentDistributions.ToDictionary(kv => kv.Key, kv => kv.Value);
        nextCurrentDistributions[joltageDifference]++;

        if (nextRemainingAdapters.Count == 0)
        {
            return (new List<long> { nextAdapter }, nextCurrentDistributions);
        }

        var result = GetDistributions(nextAdapter, nextRemainingAdapters, nextCurrentDistributions);

        if (result != (null, null))
        {
            result.Item1.Add(nextAdapter);
            return result;
        }
    }

    return (null, null);
}


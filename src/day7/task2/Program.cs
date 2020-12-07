using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var bags = new Dictionary<string, HashSet<AllowedBags>>();

foreach (var line in lines)
{
    var trimmedLine = line.TrimEnd('.');
    var kv = trimmedLine.Split("contain", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    if (kv[1] == "no other bags")
        continue;
    
    var keyTokens = kv[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var key = $"{keyTokens[0]} {keyTokens[1]}";

    var values = kv[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var allowedContent = new HashSet<AllowedBags>();

    foreach (var value in values)
    {
        var valueTokens = value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var amount = int.Parse(valueTokens[0]);
        var contentKey = $"{valueTokens[1]} {valueTokens[2]}";

        allowedContent.Add(new AllowedBags(contentKey, amount));
    }

    bags.Add(key, allowedContent);
}

WriteLine("Input:");

foreach (var bag in bags)
{
    Write(bag.Key);
    Write(": ");

    foreach (var allowedBag in bag.Value)
    {
        Write(allowedBag);
        Write(" ");
    }

    WriteLine();
}

WriteLine("Answer:");

long Count(string keyToCount)
{
    long result = 1;

    if (bags.TryGetValue(keyToCount, out var allowedContent))
    {
        foreach (var nextToCount in allowedContent)
        {
            long nextCount = Count(nextToCount.Id);
            result += nextToCount.Amount * nextCount;

            WriteLine($"{keyToCount}: {nextToCount} * {nextCount}");
        }
    }

    return result;
}

WriteLine(Count("shiny gold") - 1);

class AllowedBags
{
    public string Id { get; }
    public int Amount { get; }

    public AllowedBags(string id, int amount)
    {
        Id = id;
        Amount = amount;
    }

    public override bool Equals(object obj)
    {
        return obj is AllowedBags bags &&
               Id == bags.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id);
    }

    public override string ToString()
    {
        return $"{Amount} {Id}";
    }
}
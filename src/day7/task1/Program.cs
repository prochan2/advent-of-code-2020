using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var bags = new Dictionary<string, HashSet<string>>();

foreach (var line in lines)
{
    var trimmedLine = line.TrimEnd('.');
    var kv = trimmedLine.Split("contain", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    if (kv[1] == "no other bags")
        continue;
    
    var keyTokens = kv[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var key = $"{keyTokens[0]} {keyTokens[1]}";

    var values = kv[1].Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

    var allowedContent = new HashSet<string>();

    foreach (var value in values)
    {
        var valueTokens = value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var amount = int.Parse(valueTokens[0]);
        var contentKey = $"{valueTokens[1]} {valueTokens[2]}";

        allowedContent.Add(contentKey);
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

var answers = new HashSet<string>();

foreach (var bag in bags)
{
    answers.UnionWith(Search("shiny gold", new HashSet<string> { bag.Key }, bag.Value));
}

HashSet<string> Search(string keyToFind, HashSet<string> previous, IEnumerable<string> options)
{
    var result = new HashSet<string>();

    foreach (var option in options)
    {
        if (option == keyToFind)
        {
            result.UnionWith(previous);
        }
        else if (bags.TryGetValue(option, out var nextOptions))
        {
            HashSet<string> current = new HashSet<string>(previous);
            current.Add(option);
            result.UnionWith(Search(keyToFind, current, nextOptions));
        }
    }

    return result;
}

foreach (var answer in answers)
    WriteLine(answer);

WriteLine(answers.Count);
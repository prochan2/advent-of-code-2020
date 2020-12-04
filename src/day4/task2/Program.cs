using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

long items = 0;
long valid = 0;

foreach (string line in lines)
{
    if (line == "")
    {
        WriteLine();

        if (items == 7)
        {
            valid++;
            WriteLine("valid");
        }
        else
        {
            WriteLine("invalid");
        }

        items = 0;

        continue;
    }

    var pairs = line.Split(' ');

    Regex hclRegex = new Regex("^#[0-9a-f]{6}$");
    Regex pidRegex = new Regex("^[0-9]{9}$");

    var ecls = new HashSet<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

    foreach (string pair in pairs)
    {
        Write(pair);
        Write(" ");

        var kv = pair.Split(':', 2);
        bool isNumber = long.TryParse(kv[1], out long value);

        switch (kv[0])
        {
            case "byr":
                if (!isNumber || value < 1920 || value > 2002)
                    continue;
                break;

            case "iyr":
                if (!isNumber || value < 2010 || value > 2020)
                    continue;
                break;

            case "eyr":
                if (!isNumber || value < 2020 || value > 2030)
                    continue;
                break;

            case "hgt":
                string x = kv[1].Substring(0, kv[1].Length - 2);
                if (!long.TryParse(x, out value))
                    continue;

                string unit = kv[1].Substring(kv[1].Length - 2);

                switch (unit)
                {
                    case "cm":
                        if (value < 150 || value > 193)
                            continue;
                        break;

                    case "in":
                        if (value < 59 || value > 76)
                            continue;
                        break;

                    default:
                        continue;
                }

                break;

            case "hcl":
                if (!hclRegex.IsMatch(kv[1]))
                    continue;
                break;

            case "ecl":
                if (!ecls.Contains(kv[1]))
                    continue;
                break;

            case "pid":
                if (!pidRegex.IsMatch(kv[1]))
                    continue;
                break;

            case "cid":
                continue;

            default:
                throw new NotImplementedException();
        }

        Write("OK");
        Write(" ");

        items++;
    }
}

WriteLine();

if (items == 7)
{
    valid++;
    WriteLine("valid");
}
else
{
    WriteLine("invalid");
}

WriteLine(valid);
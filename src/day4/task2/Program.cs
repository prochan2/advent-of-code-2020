using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

long items = 0;
long valid = 0;

for (int lineNumber = 0; lineNumber < lines.Length; lineNumber++)
{
    try
    {
        var pairs = lines[lineNumber].Split(' ');

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
                    string x = kv[1][0..^2];
                    if (!long.TryParse(x, out value))
                        continue;

                    string unit = kv[1][^2..];

                    switch ((value, unit))
                    {
                        case (>= 150 and <= 193, "cm"):
                            break;

                        case (>= 59 and <= 76, "in"):
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
    finally
    {
        if (lineNumber == lines.Length - 1 || lines[lineNumber + 1] == "")
        {
            lineNumber++;

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
        }
    }
}

WriteLine(valid);
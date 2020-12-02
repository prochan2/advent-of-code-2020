using System.IO;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var regex = new Regex(@"^(?<first>\d+)-(?<second>\d+) (?<character>[a-zA-Z]): (?<password>[a-zA-Z]+)$");

int validCount = 0;

foreach (string line in lines)
{
    var match = regex.Match(line);
    int first = int.Parse(match.Groups["first"].Value) - 1;
    int second = int.Parse(match.Groups["second"].Value) - 1;
    char character = match.Groups["character"].Value[0];
    string password = match.Groups["password"].Value;

    int sum = password[first] == character ? 1 : 0;
    sum += password[second] == character ? 1 : 0;

    if (sum == 1)
        validCount++;
}

WriteLine(validCount);
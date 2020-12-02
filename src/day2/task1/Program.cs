using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

var regex = new Regex(@"^(?<min>\d+)-(?<max>\d+) (?<character>[a-zA-Z]): (?<password>[a-zA-Z]+)$");

int validCount = 0;

foreach (string line in lines)
{
    var match = regex.Match(line);
    int min = int.Parse(match.Groups["min"].Value);
    int max = int.Parse(match.Groups["max"].Value);
    char character = match.Groups["character"].Value[0];
    string password = match.Groups["password"].Value;

    int sum = password.Sum(c => c == character ? 1 : 0);

    if (sum >= min && sum <= max)
        validCount++;
}

WriteLine(validCount);
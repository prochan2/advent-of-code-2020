using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");
var ints = lines.Select(line => int.Parse(line)).ToArray();

for (int i = 0; i < ints.Length; i++)
{
    for (int j = i + 1; j < ints.Length; j++)
    {
        if (ints[i] + ints[j] == 2020)
        {
            WriteLine(ints[i] * ints[j]);
        }
    }
}
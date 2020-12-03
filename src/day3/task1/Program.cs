using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

int treesCount = 0;
int x = 0;

foreach (string line in lines.Skip(1))
{
    x += 3;

    if (x >= line.Length)
        x -= line.Length;

    if (line[x] == '#')
        treesCount++;
}

WriteLine(treesCount);
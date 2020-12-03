using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt").Where(line => !string.IsNullOrEmpty(line)).ToArray();

long product = 1;

product *= Count(1, 1);
product *= Count(3, 1);
product *= Count(5, 1);
product *= Count(7, 1);
product *= Count(1, 2);

WriteLine(product);

long Count(int xStep, int yStep)
{
    long treesCount = 0;
    int x = 0;
    int y = 0;

    do
    {
        y += yStep;

        if (y >= lines.Length)
            y = lines.Length - 1;

        x += xStep;

        if (x >= lines[y].Length)
            x -= lines[y].Length;

        if (lines[y][x] == '#')
            treesCount++;
    }
    while (y != lines.Length - 1);

    WriteLine(treesCount);

    return treesCount;
}
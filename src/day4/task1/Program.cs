using System.IO;
using System.Linq;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt");

long items = 0;
long valid = 0;

foreach (string line in lines)
{
    if (line == "")
    {
        if (items == 7)
            valid++;

        items = 0;

        continue;
    }

    var pairs = line.Split(' ');

    foreach (string pair in pairs)
    {
        var kv = pair.Split(':');

        if (kv[0] != "cid")
            items++;
    }
}

if (items == 7)
    valid++;

WriteLine(valid);
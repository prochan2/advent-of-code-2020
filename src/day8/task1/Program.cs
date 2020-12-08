using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt").ToArray();

long ip = 0;
long acc = 0;

var visitedIps = new HashSet<long>();

while (!visitedIps.Contains(ip))
{
    visitedIps.Add(ip);
    var instruction = lines[ip].Split(' ');

    string opcode = instruction[0];
    int arg = int.Parse(instruction[1]);

    switch (opcode)
    {
        case "nop":
            ip++;
            break;
        case "acc":
            acc += arg;
            ip++;
            break;
        case "jmp":
            ip += arg;
            break;
        default:
            throw new NotImplementedException();
    }

    WriteLine($"{opcode} {arg} | {acc}");
}

WriteLine(acc);
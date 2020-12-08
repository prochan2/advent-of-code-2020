using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Console;

var lines = File.ReadAllLines(@"..\..\..\..\input.txt").ToArray();

for (int i = 0; i < lines.Length; ++i)
{
    switch (lines[i].Split(' ')[0])
    {
        case "nop":
        case "jmp":
            if (Validate(i, out long answer))
            {
                WriteLine(answer);
                return;
            }
            break;
    }
}

WriteLine("Not found.");

bool Validate(int ipToSwitch, out long acc)
{
    long ip = 0;
    acc = 0;

    var visitedIps = new HashSet<long>();

    while (!visitedIps.Contains(ip))
    {
        visitedIps.Add(ip);
        var instruction = lines[ip].Split(' ');

        string opcode = instruction[0];
        int arg = int.Parse(instruction[1]);

        if (ip == ipToSwitch)
        {
            switch (opcode)
            {
                case "nop":
                    opcode = "jmp";
                    break;
                case "jmp":
                    opcode = "nop";
                    break;
                case "acc":
                default:
                    throw new NotImplementedException();
            }
        }

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

        if (ip < 0 || ip > lines.Length)
            return false;

        if (ip == lines.Length)
            return true;

        //WriteLine($"{opcode} {arg} | {acc}");
    }

    return false;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class consoleLog 
{
    public static void write(string args)
    {
        string docPath = "D:/";

        using (StreamWriter outputFile = File.AppendText(Path.Combine(docPath, args)))
        {
            outputFile.WriteLine(args + "\n");
        }
    }
}

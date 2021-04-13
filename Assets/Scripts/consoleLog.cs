using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public static class consoleLog 
{
    public static void write(string args)
    {
        string docPath = "Y:/";

        using (StreamWriter outputFile = File.AppendText(Path.Combine(docPath, "WriteLines.txt")))
        {
            outputFile.WriteLine(args + "\n");
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;

namespace MSH
{
    class Program
    {
        static void Main(string[] args)
        {
            Directory.SetCurrentDirectory(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
            int status;
            do 
            {
                Console.ResetColor();
                DateTime date = DateTime.Now;
                Console.Write("[{0:g}] " + Directory.GetCurrentDirectory() + "\\", date);
                Console.Write("> ");
                Console.ResetColor();
                string[] input = Console.ReadLine().Split(' ',2);
                status = exec(input);
                if (status != 0 && status != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X ");
                } 
                else if (status != -1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("V ");
                }
                else
                {
                    Console.ResetColor();
                }
            } while (status != -1);
          
        }
        static int exec(string[] input)
        {
            if (input[0] == "exit")
            {
                return -1;
            }
            else
            {
                Process process;
                if (input.Length >= 2)
                {
                    try
                    {
                        process = Process.Start(input[0],input[1]);
                        process.WaitForExit();
                        return process.ExitCode;
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        Console.WriteLine("msh: " + input[0] + ": command not found.");
                        return 1;
                    }
                }
                else
                {
                    try
                    {
                        process = Process.Start(input[0]);
                        process.WaitForExit();
                        return process.ExitCode;
                    }
                    catch (System.ComponentModel.Win32Exception)
                    {
                        Console.WriteLine("msh: " + input[0] + ": command not found.");
                        return 1;
                    } 
                }
            }
        }
    }
}

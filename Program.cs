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
                Console.Write("[{0:g}] ", date);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Environment.UserName + "@" + Environment.MachineName);
                Console.ResetColor();
                Console.Write(":");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(Directory.GetCurrentDirectory());
                Console.ResetColor();
                Console.Write("> ");
                Console.ResetColor();
                string[] input = Console.ReadLine().Split(' ',2);
                status = Exec(input);
                if (status != 0 && status != -1 && status != -2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("X ");
                } 
                else if (status == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("V ");
                }
                else if (status == -2)
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.ResetColor();
                }
            } while (status != -1);
          
        }
        static int Exec(string[] input)
        {
            if (input[0] == "connect")
            {
                Console.WriteLine("This feature is still being worked on.");
                return 0;
            } else if (input[0] == "exit")
            {
                return -1;
            } else if (input[0] == "clear")
            {
                Console.Clear();
                return -2;
            } else if (input[0] == "cd")
            {
                if (!(input.Length >= 2)) {Console.WriteLine("Please specify a path"); Console.Beep(); return 1;}
                try
                {
                    Directory.SetCurrentDirectory(input[1]);
                    return 0;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("msh: cd: Directory not found");
                    return 1;
                }
            } else if (input[0] == "ls")
            {
                string[] files = Directory.GetFileSystemEntries(Directory.GetCurrentDirectory());
                foreach (string file in files)
                {
                    Console.WriteLine(file);
                }
                return 0;
            } else
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
                    } catch (InvalidOperationException)
                    {
                        return -2;
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
                    } catch (InvalidOperationException)
                    {
                        return -2;
                    }
                }
            }
        }
    }
}

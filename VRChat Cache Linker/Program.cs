using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;

class Program {
    public static bool IsAdministrator() {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent()) {
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }


    static void Main() {
        // Admin Check 
        if (!IsAdministrator()) {
            Console.WriteLine("This program requires administrator privileges. Please run it as an administrator.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        // Folder paths
        string vrChatMainFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "..", "LocalLow", "VRChat");
        string vrChatData = Path.Combine(vrChatMainFolder, "VRChat");

        // Checks
        if (!Directory.Exists(vrChatMainFolder)) {
            Console.WriteLine($"VRChat main folder not found at. Please ensure VRChat is installed.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        if (!Directory.Exists(vrChatData)) {
            //Console.WriteLine($"VRChat data folder not found. Please ensure VRChat is installed.");
            //Console.WriteLine("Press any key to exit...");
            //Console.ReadKey();
            //return; uh idk this is a workaround for the case when the folder does not exist

            Directory.CreateDirectory(vrChatData);
        }

        // New path input
        Console.WriteLine("Enter the new path for the VRChat cache folder:");
        string pathToLink = Console.ReadLine()?.Trim();

        // Check
        if (string.IsNullOrWhiteSpace(pathToLink) || !Directory.Exists(pathToLink)) {
            Console.WriteLine("The specified path for the VRChat cache does not exist or is invalid.");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        // path to the new cache folder
        string newCachePath = Path.Combine(pathToLink, "VRChatCache");
        // exists check
        bool IsChacheExist = false;
        if (Directory.Exists(newCachePath)) {
            IsChacheExist = true;
        }
        try {
            if (!IsChacheExist){
                Console.WriteLine($"Moving folder...");
                Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(vrChatData, newCachePath);
                Console.WriteLine($"DONE!");
            }else{
                Console.WriteLine($"The folder already exists at {newCachePath}. Skipping move operation.");
                Console.WriteLine($"ATTENTION, YOUR ORIGINAL FOLDER WILL BE DELETED! WRITE \"yes\" IF YOU WANT TO CONTINUE");
                if (Console.ReadLine()?.ToLower() == "yes") {
                    Console.WriteLine($"Deleting original folder...");
                    Directory.Delete(vrChatData, true);
                    Console.WriteLine($"DONE!");
                } else {
                    Console.WriteLine("Operation cancelled");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }   

            }

            ProcessStartInfo processInfo = new ProcessStartInfo {
                    FileName = "cmd.exe",
                    Arguments = $"/C mklink /D \"{vrChatData}\" \"{newCachePath}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

            using (Process process = new Process()) {
                process.StartInfo = processInfo;
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode != 0) {
                    Console.WriteLine($"Failed to create symbolic link: {error}");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine(output);
                Console.WriteLine($"VRChat cache successfully moved and linked to {newCachePath}.");
            }
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }


}
using System.Diagnostics;
using System.Security.Cryptography;

namespace FilesAutoOrganizer;

internal abstract class Program
{
    private static List<string> filesmd5list = new List<string>();
    public static void Main(string[] args)
    {
        var downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads";
        Array.Resize(ref args, args.Length + 3);
        args[0] = "--calchash";
        args[1] = "--organize";
        args[2] = @"--path=C:\Users\d3sync\Documents\Downloads";
        var gbf = new GroupByFiles();
        if (args.Length < 1)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Usage: {Process.GetCurrentProcess().MainModule!.ModuleName} --organize --calchash [--path=C:\\Temp]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"If --path is not configured default path is: {downloadsPath}");
            Environment.Exit(-1);
        }
        gbf.SerializeAndWriteToFile();
        gbf.ClearList();
        gbf.ReadFromFileAndDeserialize();


        var argIndex = -1; // Initialize to -1, which means the argument wasn't found
        for (var i = 0; i < args.Length; i++)
        {
            if (args[i].StartsWith("--path"))
            {
                argIndex = i;
                break;
            }
        }
        var pathing = args[argIndex];
        var split = pathing.Split('=');
        Console.WriteLine($"Path was set to {split[1]}");
        if (!Directory.Exists(split[1]))
        {
            Console.WriteLine($"Invalid Path! {split[1]} doesn't exist!");
            Environment.Exit(404);
        }
        else
        {

            downloadsPath = split[1];
        }

        if (Directory.Exists(downloadsPath))
        {
            Console.WriteLine($"Searching for files in {downloadsPath}");
            var files = Directory.GetFiles(downloadsPath).ToList();
            if (files.Count > 0)
            {
                try
                {
                    foreach (var file in files)
                    {
                        var item = new FileInfo(file);
                        if (args.Contains("--calchash"))
                        {
                            CalculateFileHash(file);
                        }

                        if (args.Contains("--organize"))
                        {
                            var ext = item.Extension.Replace(".", "").ToUpper();
                            var folderPath = Path.Combine(downloadsPath, gbf.GetExtFolder(ext));
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                                Console.WriteLine($"Creating folder: {folderPath}");
                            }

                            try
                            {
                                File.Move(file, folderPath + "\\" + item.Name);
                                Console.WriteLine($"Moving {file} to {folderPath}\\{item.Name}");
                            }
                            catch (System.IO.IOException ioe)
                            {
                                Console.WriteLine(ioe.Message);
                                Console.WriteLine(ioe.Data);
                                File.Move(file, folderPath + $"\\({DateTime.Now:HHmm})-" + item.Name);
                            }
                        }
                    }

                    WriteToFileHashes();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        else
        {
            Console.WriteLine($"No {downloadsPath} folder available!");
        }
    }

    private static void CalculateFileHash(string filePath)
    {
        using var md5 = MD5.Create();
        using var stream = File.OpenRead(filePath);
        var hash = md5.ComputeHash(stream);
        var hashString = BitConverter.ToString(hash).Replace("-", "").ToLower();
        filesmd5list.Add($"MD5 hash of {filePath}: {hashString}");
    }

    private static void WriteToFileHashes()
    {
        var folderPath = global::System.IO.Directory.GetCurrentDirectory();
        var filePath = Path.Combine(folderPath, $"Organized-FilesMovedHashes-{DateTime.Now:ddMMyy-HHmm}.txt");
        foreach (var item in filesmd5list)
        {
            File.AppendAllText(filePath, item + "\r\n");
        }
        Console.WriteLine($"Wrote All Md5 Hashes to {filePath}");
    }
}
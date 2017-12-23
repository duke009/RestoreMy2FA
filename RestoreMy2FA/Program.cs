using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoogleAuthCruncher;
using RestoreMy2FA.Resources;

namespace RestoreMy2FA
{
    class Program
    {
        static void Main(string[] args)
        {
            var cruncher = new Cruncher();

            if (!args.Any())
            {
                ProcessCommandLine(cruncher);
                Console.WriteLine(Strings.PressAnyKey);
                Console.ReadKey();
                return;
            }

            if (args.Length != 1)
            {
                SaveCrunched(cruncher.CrunchTitaniumZip(args[0]));
                Console.WriteLine(Strings.PressAnyKey);
                Console.ReadKey();
                return;
            }

            if (args.Length != 2)
            {
                Console.WriteLine(Strings.WrongCountOfArguments);
                Console.WriteLine(Strings.Help);
                Console.WriteLine(Strings.PressAnyKey);
                Console.ReadKey();
                return;
            }

            if (!File.Exists(args[1]))
            {
                Console.WriteLine(Strings.FileDoesNotExist, args[1]);
                Console.WriteLine(Strings.PressAnyKey);
                Console.ReadKey();
                return;
            }

            switch (args[0])
            {
                case "archive":
                    SaveCrunched(cruncher.CrunchTitaniumZip(args[1]));
                    break;
                case "db":
                    SaveCrunched(cruncher.CrunchDbFile(args[1]));
                    break;
                default:
                    Console.WriteLine(Strings.UnexpectedArgument);
                    break;
            }

            Console.WriteLine(Strings.PressAnyKey);
            Console.ReadKey();
        }


        private static void ProcessCommandLine(Cruncher cruncher)
        {
            Console.WriteLine(Strings.WhatDoYouHave);
            Console.WriteLine(Strings.TitaniumBackupArchive);
            Console.WriteLine(Strings.DatabaseFile);

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar)
                {
                    case '1':
                        ChosePath(filePath => SaveCrunched(cruncher.CrunchTitaniumZip(filePath)), "com.google.authenticator.tar.gz");
                        return;
                    case '2':
                        ChosePath(filePath => SaveCrunched(cruncher.CrunchDbFile(filePath)), "accounts");
                        return;
                }
            }
        }

        private static void ChosePath(Action<string> setPath, string defaultPath)
        {
            Console.WriteLine(Strings.DidYouCopyFileToTheProgramDirectory);

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.KeyChar.ToString().ToLower())
                {
                    case "y":

                        if (!File.Exists(defaultPath))
                        {
                            Console.WriteLine(Strings.FileDoesNotExist, defaultPath);
                            return;
                        }

                        setPath(defaultPath);
                        break;
                    case "n":
                        Console.WriteLine(Strings.PleaseInputFilePath);
                        var path = Console.ReadLine();
                        Console.WriteLine();

                        if (!File.Exists(path))
                        {
                            Console.WriteLine(Strings.FileDoesNotExist, path);
                            return;
                        }

                        setPath(defaultPath);

                        break;
                }
            }
        }


        private static void SaveCrunched(IEnumerable<BitmapModel> crunched)
        {
            // otpauth://totp/Google%3Amyemail%40gmail.com?secret=7gmdmzctmhpm7i6nrmbom6u5gny7o6la&issuer=Google
            var di = Directory.CreateDirectory("Export");
            foreach (var bitmapModel in crunched)
            {
                var fileName = Path.GetInvalidFileNameChars().Aggregate(bitmapModel.OriginalName, (current, badChar) => current.Replace(badChar.ToString(), "."));
                bitmapModel.Bitmap.Save(Path.Combine(di.Name, $"{fileName}.bmp"));
            }

            Console.WriteLine(Strings.NKeysExported, crunched.Count());
        }
    }
}

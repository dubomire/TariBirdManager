using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using TariBirdManager.Extensions;

namespace TariBirdManager
{
    static class Program
    {
        private static string GetTargetPath(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            if (args.Length > 0 &&
                !string.IsNullOrEmpty(args[0]) &&
                new DirectoryInfo(args[0]).Exists)
            {
                path = args[0];
            }

            return path;
        }

        /// <summary>
        /// Main method. Entry point.
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting iBatisAnalyzer");

            string path = GetTargetPath(args);

            new ServiceCollection()
                .ConfigureServices()
                .BuildServiceProvider()
                .StartAnalyzer(path)
                .WaitForStopSignal();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Ls.ListingFormatters;

namespace Ls
{
    public class LsController
    {
        private const string NEEDS_DIRECTORY_ERROR = "You must pass in a directory.";

        /// <summary>
        /// It all begins here.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args)
        {
            List<string> targetDirectories = new List<string>();
            targetDirectories.Add(".");

            List<string> argsList = args.ToList<string>();

            targetDirectories = UpdateTargetDirectoryFromCommandArguments(targetDirectories, argsList);

            List<char> fullFlagList = PullFlagsFromCommandLineArguments(argsList);
            
            bool firstIteration = true;
            foreach (string targetDirectory in targetDirectories)
            {
                if (File.Exists(targetDirectory))
                {
                    Console.WriteLine(NEEDS_DIRECTORY_ERROR);
                }
                if (!Directory.Exists(targetDirectory))
                {
                    continue;
                }

                int rootDirectoryNameLength = targetDirectory.Length;

                IEnumerable<string> directories = Directory.GetDirectories(targetDirectory).Select(x => x.Substring(rootDirectoryNameLength + 1)).OrderBy(x => x);
                IEnumerable<string> files = Directory.GetFiles(targetDirectory).Select(x => x.Substring(rootDirectoryNameLength + 1)).OrderBy(x => x);

                ConsoleColor originalConsoleColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                if (!firstIteration)
                {
                    Console.WriteLine();
                }
                Console.WriteLine("Listing for {0} >>\n", (Directory.GetCurrentDirectory().Contains(targetDirectory) || targetDirectory.Equals(".")) ? "./": targetDirectory);
                Console.ForegroundColor = originalConsoleColor;

                IListingFormatter lister = ListingFormatterFactory.getLister(fullFlagList, targetDirectory);
                lister.PrintListings();
                firstIteration = false;
            }
        }

        /// <summary>
        /// Parse flags from the command line arguments list.
        /// </summary>
        /// <param name="argsList">The command line arguments.</param>
        /// <returns>List of flags found behind "-" character.</returns>
        private static List<char> PullFlagsFromCommandLineArguments(List<string> argsList)
        {
            IEnumerable<char[]> flagsList = argsList.Where(x => x.StartsWith("-")).Select(x => x.Substring(1).ToCharArray());

            List<char> fullFlagList = new List<char>();
            foreach (char[] listElement in flagsList)
            {
                fullFlagList.AddRange(listElement);
            }

            fullFlagList = fullFlagList.Distinct().ToList<char>();
            
            return fullFlagList;
        }

        /// <summary>
        /// Reads the argument list and updates the target directories if there are any non flag inputs.
        /// </summary>
        /// <param name="targetDirectory">The current list of target directories.</param>
        /// <param name="argsList">The command line arguments.</param>
        /// <returns></returns>
        private static List<string> UpdateTargetDirectoryFromCommandArguments(List<string> targetDirectory, List<string> argsList)
        {
            IEnumerable<string> possibleNewTargets = argsList.Where(x => !x.StartsWith("-"));
            if (possibleNewTargets != null && possibleNewTargets.Any())
            {
                possibleNewTargets = possibleNewTargets.Select(x =>
                {
                    //A bit of data cleansing.
                    if (x.EndsWith(@"/") || x.EndsWith(@"\"))
                    {
                        return x.Remove(x.Length - 1);
                    }
                    else
                    {
                        return x;
                    }
                });

                targetDirectory = possibleNewTargets.ToList<string>();
            }

            return targetDirectory;
        }
    }
}

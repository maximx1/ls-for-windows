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

        public static void Main(string[] args)
        {
            string targetDirectory = Directory.GetCurrentDirectory();

            List<string> argsList = args.ToList<string>();

            targetDirectory = UpdateTargetDirectoryFromCommandArguments(targetDirectory, argsList);

            List<char> fullFlagList = PullFlagsFromCommandLineArguments(argsList);

            if (File.Exists(targetDirectory))
            {
                Console.WriteLine(NEEDS_DIRECTORY_ERROR);
            }


            int rootDirectoryNameLength = targetDirectory.Length;

            IEnumerable<string> directories = Directory.GetDirectories(targetDirectory).Select(x => x.Substring(rootDirectoryNameLength + 1)).OrderBy(x => x);
            IEnumerable<string> files = Directory.GetFiles(targetDirectory).Select(x => x.Substring(rootDirectoryNameLength + 1)).OrderBy(x => x);

            IListingFormatter lister = ListingFormatterFactory.getLister(fullFlagList);
            lister.PrintListings(directories, files);
        }

        /// <summary>
        /// Parse flags from the command line arguments list.
        /// </summary>
        /// <param name="argsList">The command line arguments.</param>
        /// <returns>List of flags found behind "-" character.</returns>
        private static List<char> PullFlagsFromCommandLineArguments(List<string> argsList)
        {
            IEnumerable<char[]> flagsList = argsList.Where(x => x.StartsWith("-")).Select(x => x.Substring(1).ToLower().ToCharArray());

            List<char> fullFlagList = new List<char>();
            foreach (char[] listElement in flagsList)
            {
                fullFlagList.AddRange(listElement);
            }

            fullFlagList = fullFlagList.Distinct().ToList<char>();
            
            return fullFlagList;
        }

        /// <summary>
        /// Updates the target directory to list based on a non flag input from the command line.
        /// </summary>
        /// <param name="targetDirectory">The current target directory.</param>
        /// <param name="argsList">The command line arguments.</param>
        /// <returns></returns>
        private static string UpdateTargetDirectoryFromCommandArguments(string targetDirectory, List<string> argsList)
        {
            string possibleNewTarget = argsList.Where(x => !x.StartsWith("-")).FirstOrDefault();
            if (possibleNewTarget != null)
            {
                targetDirectory = argsList.Where(x => !x.StartsWith("-")).FirstOrDefault();
            }

            //A bit of data cleansing.
            if (targetDirectory.EndsWith(@"\"))
            {
                targetDirectory = targetDirectory.Remove(targetDirectory.Length - 1);
            }

            return targetDirectory;
        }
    }
}
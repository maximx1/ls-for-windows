using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingFormatters
{
    class LongListingFormatter : IListingFormatter
    {
        private bool listingHidden;

        public LongListingFormatter(bool listingHidden)
        {
            this.listingHidden = listingHidden;
        }

        /// <summary>
        /// Generates the line by line listing for ls -l
        /// </summary>
        /// <param name="listing">The listing list to format.</param>
        /// <returns>Formatted listings.</returns>
        public string GenerateListing(IEnumerable<string> listing)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string item in listing)
            {
                builder.Append(item + "\n");
            }

            return builder.ToString();
        }

        /// <summary>
        /// Prints out the directory and file listings.
        /// </summary>
        /// <param name="directories">List of directories.</param>
        /// <param name="files">List of files.</param>
        public void PrintListings(IEnumerable<string> directories, IEnumerable<string> files)
        {
            if (!listingHidden)
            {
                directories = directories.Where(x => !x.StartsWith("."));
                files = files.Where(x => !x.StartsWith("."));
            }

            string formattedListing = GenerateListing(directories);
            ConsoleColor originalForegroundColor = Console.ForegroundColor;

            if (formattedListing.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(formattedListing);
            }

            formattedListing = GenerateListing(files);
            if (formattedListing.Length > 0)
            {
                Console.ForegroundColor = originalForegroundColor;
                Console.Write(formattedListing);
            }

            Console.WriteLine();
            Console.ForegroundColor = originalForegroundColor;
        }
    }
}

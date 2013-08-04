using Ls.ListingItemControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingFormatters
{
    class RegularListingFormatter : IListingFormatter
    {
        private int currentScreenWidth;
        private ListingItemControllerBase listingItemController;
        private string targetDirectory;

        /// <summary>
        /// Sets up the regular listing formatter.
        /// </summary>
        public RegularListingFormatter()
        {
            currentScreenWidth = Console.WindowWidth;
        }

        public RegularListingFormatter(ListingItemControllerBase listingItemController, string targetDirectory) : this()
        {
            this.listingItemController = listingItemController;
            this.targetDirectory = targetDirectory;
        }

        /// <summary>
        /// Prints out the listing in Regular format.
        /// </summary>
        public string GenerateListing(IEnumerable<string> listings)
        {
            StringBuilder builder = new StringBuilder();
            if (listings.Count() > 0)
            {
                int lengthOfLongestListingItem = listings.OrderByDescending(s => s.Length).First().Length;

                int currentLineWidth = 0;
                foreach (String listing in listings)
                {
                    if (lengthOfLongestListingItem < currentScreenWidth)
                    {
                        if (currentLineWidth + lengthOfLongestListingItem > currentScreenWidth)
                        {
                            builder.Remove(builder.Length - 4 - 1, 4).Append("\n");
                            currentLineWidth = 0;
                        }

                        builder.Append(String.Format("{0, -" + lengthOfLongestListingItem.ToString() + "}    ", listing));

                        currentLineWidth += lengthOfLongestListingItem + 4;
                    }
                    else
                    {
                        builder.Append(listing + "\n");
                    }

                }
            }

            return builder.ToString();
        }

        /// <summary>
        /// Prints the listings in order
        /// </summary>
        /// <param name="directories"></param>
        /// <param name="files"></param>
        public void PrintListings()
        {
            IEnumerable<string> directories = listingItemController.GetDirectories();
            IEnumerable<string> files = listingItemController.GetFiles();

            string formattedListing = GenerateListing(directories);
            ConsoleColor originalForegroundColor = Console.ForegroundColor;

            if (formattedListing.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("--Directories--");

                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(formattedListing);
            }

            formattedListing = GenerateListing(files);
            if (formattedListing.Length > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (directories.Any())
                {
                    Console.WriteLine();
                }
                Console.WriteLine("--Files--");

                Console.ForegroundColor = originalForegroundColor;
                Console.Write(formattedListing);
            }

            Console.WriteLine();
            Console.ForegroundColor = originalForegroundColor;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ls
{
    public interface IListingFormatter
    {
        /// <summary>
        /// Prints out the listing of files in the specified format.
        /// </summary>
        string GenerateListing(IEnumerable<string> listing);

        /// <summary>
        /// Prints the listings of files and directories in the specified format.
        /// </summary>
        void PrintListings();
    }
}
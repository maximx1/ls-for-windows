using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingFormatters
{
    public class ListingFormatterFactory
    {
        /// <summary>
        /// Determines which formatter to provide based on the format flag.
        /// </summary>
        /// <param name="flags">command line flags.</param>
        /// <returns></returns>
        public static IListingFormatter getLister(List<char> flags)
        {
            IListingFormatter lister = new RegularListingFormatter(flags.Contains('a'));

            if (flags.Contains('l'))
            {
                lister = new LongListingFormatter(flags.Contains('a'));
            }

            return lister;
        }
    }
}

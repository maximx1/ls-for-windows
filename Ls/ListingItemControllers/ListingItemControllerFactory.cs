using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingItemControllers
{
    public class ListingItemControllerFactory
    {
        public static ListingItemControllerBase getItemController(List<char> flags, string targetDirectory)
        {
            if (flags.Contains('a'))
            {
                return new AllListingItemController(targetDirectory);
            }
            else if (flags.Contains('A'))
            {
                return new AlmostAllListingItemController(targetDirectory);
            }
            else
            {
                return new RegularItemController(targetDirectory);
            }
        }
    }
}

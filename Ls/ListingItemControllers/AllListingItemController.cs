using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingItemControllers
{
    class AllListingItemController : ListingItemControllerBase
    {
        public AllListingItemController(string targetDirectory) : base(targetDirectory) { }

        /// <summary>
        /// Gets the directories listed in the root with "." and "..".
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <returns>List of directories.</returns>
        public override IEnumerable<string> GetDirectories()
        {
            return (new string[] { ".", ".." }).Concat(base.GetDirectories());
        }
    }
}

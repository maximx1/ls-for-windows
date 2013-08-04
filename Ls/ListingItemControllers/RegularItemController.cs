using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingItemControllers
{
    class RegularItemController : ListingItemControllerBase
    {
        public RegularItemController(string targetDirectory) : base(targetDirectory) { }

        /// <summary>
        /// Gets the directories listed in the root minus all the directories that start with "."
        /// </summary>
        /// <returns>List of directories.</returns>
        public override IEnumerable<string> GetDirectories()
        {
            return RemoveHiddenFiles(base.GetDirectories());
        }

        /// <summary>
        /// Gets the files listed in the root minus all the directories that start with "."
        /// </summary>
        /// <returns>List of directories.</returns>
        public override IEnumerable<string> GetFiles()
        {
            return RemoveHiddenFiles(base.GetFiles());
        }

        private IEnumerable<string> RemoveHiddenFiles(IEnumerable<string> items)
        {
            return items.Where(x => !x.StartsWith("."));
        }
    }
}

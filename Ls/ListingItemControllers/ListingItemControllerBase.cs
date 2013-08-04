using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Ls.ListingItemControllers
{
    public abstract class ListingItemControllerBase
    {
        protected string targetDirectory;

        /// <summary>
        /// Generates the controller with a target directory.
        /// </summary>
        /// <param name="targetDirectory"></param>
        protected ListingItemControllerBase(string targetDirectory)
        {
            this.targetDirectory = targetDirectory;
        }

        /// <summary>
        /// Gets the directories listed in the root.
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <returns>List of directories.</returns>
        public virtual IEnumerable<string> GetDirectories()
        {
            return Directory.GetDirectories(targetDirectory).Select(x => x.Substring(targetDirectory.Length + 1)).OrderBy(x => x);
        }

        /// <summary>
        /// Gets the directories listed in the root.
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <returns>List of directories.</returns>
        public virtual IEnumerable<string> GetFiles()
        {
            return Directory.GetFiles(targetDirectory).Select(x => x.Substring(targetDirectory.Length + 1)).OrderBy(x => x);
        }
    }
}

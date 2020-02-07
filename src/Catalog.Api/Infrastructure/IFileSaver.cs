using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure
{
    /// <summary>
    ///     A contract to implement for saving <see cref="IFormFile"/> on disk.
    /// </summary>
    public interface IFileSaver
    {
        /// <summary>
        ///     Saves file asynchronously
        /// </summary>
        /// <param name="file">
        ///     The file to save
        /// </param>
        /// <param name="directoryPath">
        ///     The directory path to save file
        /// </param>
        /// <returns>
        ///     Task contains file name
        /// </returns>
        /// <exception cref="DirectoryNotFoundException">1</exception>
        Task<string> SaveAsync(IFormFile file, string directoryPath);
    }
}
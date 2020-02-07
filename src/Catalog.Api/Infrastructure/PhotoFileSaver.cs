using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure
{
    public class PhotoFileSaver : IFileSaver
    {
        public async Task<string> SaveAsync(IFormFile file, string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Path {directoryPath} does not exist.");

            var fileName = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last().Trim('\\')}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);

            await using (var outputFile = File.OpenWrite(path))
            {
                await file.OpenReadStream().CopyToAsync(outputFile);
                outputFile.Close();
            }

            return fileName;
        }
    }
}
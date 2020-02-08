using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Infrastructure
{
    public class PhotoFileSaver : IFileSaver
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IConfiguration _configuration;

        public PhotoFileSaver(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public async Task<string> SaveAsync(IFormFile file, string directory)
        {
            var directoryPath = Path.Combine(_hostEnvironment.WebRootPath, directory);
            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Path {directoryPath} does not exist.");

            var fileName = $"{Guid.NewGuid()}.{file.FileName.Split('.').Last().Trim('\\')}";
            //var path = Path.Combine(Directory.GetCurrentDirectory(), directoryPath, fileName);
            var path2 = Path.Combine(directoryPath, fileName);

            await using (var outputFile = File.OpenWrite(path2))
            {
                await file.OpenReadStream().CopyToAsync(outputFile);
                outputFile.Close();
            }

            return fileName;
        }
    }
}
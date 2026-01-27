using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastucture.Repositories.Service
{
    internal class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider fileProvider;
        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var savedFilePaths = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);
            if (!Directory.Exists(ImageDirectory))
                Directory.CreateDirectory(ImageDirectory);

            foreach (var item in files)
            {
                if (item.Length > 0)
                {
                    var imageName = item.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";
                    var root = Path.Combine(ImageDirectory, "wwwroot");
                    using (var stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    savedFilePaths.Add(imageSrc);
                }
            }
            return savedFilePaths;
        }

        public void RemoveImageAsync(string src)
        {
            var info = fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            if (!File.Exists(root))
                return;

            File.Delete(root);
        }
    }
}
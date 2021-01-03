using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Application.Catalog.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "image";
        public FileStorageService(IWebHostEnvironment env)
        {
            _userContentFolder = Path.Combine(env.WebRootPath, USER_CONTENT_FOLDER_NAME);
        }
        public async Task DeleteFileAsync(string fileName)
        {
            string fileFolder = Path.Combine(_userContentFolder, fileName);
            if(File.Exists(fileFolder) == true)
            {
                await Task.Run(() => File.Delete(fileFolder));
            }
        }
        public string GetFileUrl(string fileName)
        {
            return $"{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }
        public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            string filePath = Path.Combine(_userContentFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await mediaBinaryStream.CopyToAsync(stream);
            }
        }
    }
}

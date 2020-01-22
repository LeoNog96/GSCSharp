using System;
using System.IO;
using System.Threading.Tasks;
using storage.Client;
using uploadFile.Core.models;
using uploadFile.Core.Services.Interfaces;

namespace uploadFile.Core.Services
{
    public class StorageService : IStorageService
    {
        private readonly Storage _storage;

        public StorageService(GoogleStorage storage)
        {
            _storage = storage;
        }

        public async Task<string> UploadFiles(GedisFile file)
        {
            if (file.File.Length == 0)
            {
                throw new Exception("Arquivo Vazio");
            }

            var name = file.Name;

            var contentType = file.File.ContentType;

            using var fileStream = new FileStream(file.Name, FileMode.Create);

            await file.File.CopyToAsync(fileStream);

            return await _storage.UploadFile(fileStream, name, contentType);
        }
    }
}
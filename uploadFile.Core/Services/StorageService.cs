using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using storage.Client;
using uploadFile.Core.models;
using uploadFile.Core.Services.Interfaces;
using Util;

namespace uploadFile.Core.Services
{
    public class StorageService : IStorageService
    {
        private readonly Storage _storage;
        private readonly string _bucketName;

        public StorageService(GoogleStorage storage, IConfiguration config)
        {
            _storage = storage;
            _bucketName = config["bucketName"];
        }

        public async Task<string> UploadFiles(GedisFile file)
        {
            if (file.File.Length == 0)
            {
                throw new Exception("Arquivo Vazio");
            }

            var name = file.Name +'.'+ file.File.FileName.Split('.').Last();

            var contentType = file.File.ContentType;

            var path = Utils.ExecutionDirectoryPathName() + "UploadTemp/" +file.File.FileName;
            try
            {
                using var fileStream = new FileStream(path, FileMode.Create);

                await file.File.CopyToAsync(fileStream);

                var url = await _storage.UploadFile(fileStream, _bucketName, name, contentType);

                return url;
            }catch(Exception)
            {
                return null;
            }
        }

        public async Task<string> DownloadFiles(string name)
        {
            var path = Utils.ExecutionDirectoryPathName() + "DownloadTemp/" + name;
            try
            {
                await _storage.DownloadFile(_bucketName, name, path);

                return path;
            }catch (Exception)
            {
                return null;
            }
        }
    }
}
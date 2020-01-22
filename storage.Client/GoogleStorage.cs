using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Util;

namespace storage.Client
{
    public class GoogleStorage : Storage
    {
        private readonly StorageClient _storageClient;

        public GoogleStorage()
        {
            _storageClient = StorageClient.Create(ConfigAuth());
        }

        public GoogleCredential ConfigAuth()
        {
            return GoogleCredential.FromJson(GetKeyToJson());
        }

        public string GetKeyToJson()
        {
            var path = (Utils.ExecutionDirectoryPathName()+ "/Keys/GoogleKey.json") ;

            using StreamReader r = new StreamReader(path);

            return r.ReadToEnd();
        }

        public override async Task DownloadFile(string bucketName, string name, string path)
        {
            using var fileStream = new FileStream(path, FileMode.Create);

            await _storageClient.DownloadObjectAsync(bucketName, name, fileStream);
        }

        public override async Task<string> UploadFile(FileStream file, string bucketName, string name, string contentType)
        {
            var fileAcl = PredefinedObjectAcl.Private;

            var fileObject = await _storageClient.UploadObjectAsync(
                bucket: bucketName,
                objectName: name,
                contentType: contentType,
                source: file,
                options: new UploadObjectOptions { PredefinedAcl = fileAcl }
            );

            return fileObject.MediaLink;
        }
    }
}
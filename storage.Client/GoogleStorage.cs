using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

namespace storage.Client
{
    public class GoogleStorage : Storage
    {
        private readonly StorageClient _storageClient;
        private string _bucketName;

        public GoogleStorage(IConfiguration config = null)
        {
            _storageClient = StorageClient.Create(ConfigAuth());

            _bucketName = config["bucketName"];
        }

        public GoogleCredential ConfigAuth()
        {
            return GoogleCredential.FromJson(GetKeyToJson());
        }

        public override void ConfigBucketName(string bucketName)
        {
            _bucketName = bucketName;
        }

        public string GetKeyToJson()
        {
            var path = (ExecutionDirectoryPathName().Replace("\\","")+ "/Keys/GoogleKey.json") ;

            using StreamReader r = new StreamReader(path);

            return r.ReadToEnd();
        }

        public override Task<FileStream> DownloadFile()
        {
            throw new System.NotImplementedException();
        }

        public override async Task<string> UploadFile(FileStream file, string name, string contentType)
        {
            var fileAcl = PredefinedObjectAcl.PublicRead;

            var fileObject = await _storageClient.UploadObjectAsync(
                bucket: _bucketName,
                objectName: name,
                contentType: contentType,
                source: file,
                options: new UploadObjectOptions { PredefinedAcl = fileAcl }
            );

            return fileObject.MediaLink;
        }

        public string ExecutionDirectoryPathName()
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);
            return dirPath + @"\";
        }
    }
}
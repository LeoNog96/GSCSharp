using System.IO;
using System.Threading.Tasks;

namespace storage.Client
{
    public abstract class Storage
    {
        public abstract Task<string> UploadFile(FileStream image, string name, string contentType);
        public abstract Task<FileStream> DownloadFile();
        public abstract void ConfigBucketName(string bucketName);
    }
}
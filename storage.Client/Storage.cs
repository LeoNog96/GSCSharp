using System.IO;
using System.Threading.Tasks;

namespace storage.Client
{
    public abstract class Storage
    {
        public abstract Task<string> UploadFile(FileStream image, string bucketName, string name, string contentType);
        public abstract Task DownloadFile(string bucketName, string name, string path);
    }
}
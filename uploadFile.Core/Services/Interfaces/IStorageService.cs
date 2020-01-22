using System.IO;
using System.Threading.Tasks;
using uploadFile.Core.models;

namespace uploadFile.Core.Services.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFiles(GedisFile file);
        Task<string> DownloadFiles(string name);
    }
}
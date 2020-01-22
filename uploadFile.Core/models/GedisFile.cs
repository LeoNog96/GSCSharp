using Microsoft.AspNetCore.Http;

namespace uploadFile.Core.models
{
    public class GedisFile
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
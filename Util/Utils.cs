using System.IO;
using System.Reflection;

namespace Util
{
    public static class Utils
    {
        public static string ExecutionDirectoryPathName()
        {
            var dirPath = Assembly.GetExecutingAssembly().Location;
            dirPath = Path.GetDirectoryName(dirPath);
            return dirPath + '/';
        }
    }
}
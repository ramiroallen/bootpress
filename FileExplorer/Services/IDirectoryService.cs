using FileExplorer.DTOs;
using System.IO;

namespace FileExplorer.Services
{
    public interface IDirectoryService
    {
        Node GetNodes(string path, int deep);
        void Watch(string directory, FileSystemEventHandler OnChanged, RenamedEventHandler OnRenamed, ErrorEventHandler OnError);
    }
}

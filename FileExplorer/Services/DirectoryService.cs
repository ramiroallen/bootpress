using FileExplorer.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FileExplorer.Services
{
    public class DirectoryService: IDirectoryService
    {
        FileSystemWatcher watcher;
        public DirectoryService()
        {
            watcher = new FileSystemWatcher();
        }
        public void Watch(string directory, FileSystemEventHandler OnChanged, RenamedEventHandler OnRenamed, ErrorEventHandler OnError)
        {
            watcher.Path = directory;
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.Attributes;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Error += new ErrorEventHandler(OnError);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.IncludeSubdirectories = true;
            watcher.EnableRaisingEvents = true;
        }

        public Node GetNodes(string path, int deep)
        {
            Node root = new Node();
            if (File.Exists(path))
            {
                root.Name = Path.GetFileName(path);
                root.FullPath = path;
                root.NodeType = NodeType.FileName;
                return root;
            }
            root.Name = new DirectoryInfo(path).Name;
            root.FullPath = path;
            root.NodeType = NodeType.Directory;
            root.Children = new List<Node>();
            foreach (var subDirectoryName in Directory.EnumerateDirectories(path))
            {
                if (deep > 0)
                {
                    root.Children.Add(GetNodes(subDirectoryName, deep - 1));
                }
                else
                {
                    root.Children.Add(new Node
                    {
                        Name = new DirectoryInfo(subDirectoryName).Name,
                        FullPath = subDirectoryName,
                        NodeType = NodeType.Directory
                    });
                }
            }
            foreach (var fileName in Directory.EnumerateFiles(path))
            {
                root.Children.Add(new Node
                {
                    Name = Path.GetFileName(fileName),
                    FullPath = fileName,
                    NodeType = NodeType.FileName
                });
            }
            return root;
        }
    }
}

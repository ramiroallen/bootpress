using System.Collections.Generic;

namespace FileExplorer.DTOs
{
    public class Node
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public List<Node> Children { get; set; }

        public NodeType NodeType { get; set; }
    }
}

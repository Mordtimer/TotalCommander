using System;
using System.Collections.Generic;
using System.Text;

namespace TC.Model {
    interface IPanel {
        string Path { get; set; }
        List<string> Folder { get; set; }
        string[] DriveList { get; }
        public bool getDirContent(string path);
        public bool openFile(string file);
    }
}

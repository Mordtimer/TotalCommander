using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;

namespace TC.Model {
    class TCPanel : IPanel {
        public TCPanel() { }

        public string Path { get; set; }
        public List<string> Folder { get; set; }
        public string[] DriveList
            => Directory.GetLogicalDrives();

        public bool getDirContent(string path) {
            List<string> Folders;
            List<string> Files;

            try {
                Folders = Directory.GetDirectories(path).ToList();
                Files = Directory.GetFiles(path).ToList();
            }
            catch {
                return false;
            }

            Folder = new List<string>();

            if(!DriveList.Contains(System.IO.Path.GetFullPath(path)))
                Folder.Add("..");

            Folder.AddRange(Folders.Select(x => "<D>" + System.IO.Path.GetFileName(x)));
            Folder.AddRange(Files.Select(x => System.IO.Path.GetFileName(x)));

            Path = System.IO.Path.GetFullPath(path);
            return true;
        }

        public bool openFile(string file) {
            if(file == null)
                return true;

            if(file.Contains("<D>")) {
                var path = Path + @"\" + file.Substring(3);
                return getDirContent(path);
            }
            if(file == "..") {
                var path = Path + @"\" + file;
                return getDirContent(path);
            }
            return true;
        }
    }
}

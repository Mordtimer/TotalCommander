using System;
using System.IO;
using System.Windows;
using resource = TC.Properties.Resources;

namespace TC.Model {
    class FilesIO {
        public static void Copy(string name, string Path_source, string Path_target) {
            if(name == null)
                return;

            if(name.Contains("[D]")) {
                name = name.Substring(3);

                var source = new DirectoryInfo(Path_source + name);
                var target = new DirectoryInfo(Path_target + @"\" + name);

                if(Path_target.Contains(source.ToString())) {
                    MessageBox.Show(resource.MsgCopy,
                                resource.MsgError,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                    return;
                }

                try {
                    CopyDirectory(source, target);
                }
                catch {
                    throw new Exception("Unable to Copy Folder to itself");
                }
            }
            else {
                var source = Path.Combine(Path_source, name);
                var target = Path.Combine(Path_target, name);
                try {
                    File.Copy(source, target, true);
                }
                catch { }
            }
        }

        private static void CopyDirectory(DirectoryInfo source, DirectoryInfo target) {
            Directory.CreateDirectory(target.FullName);

            foreach(FileInfo fi in source.GetFiles()) {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach(DirectoryInfo diSourceSubDir in source.GetDirectories()) {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyDirectory(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}

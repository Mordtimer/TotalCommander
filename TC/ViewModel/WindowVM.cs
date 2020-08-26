using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TC.Model;
using resource = TC.Properties.Resources;

namespace TC.ViewModel {
    class WindowVM : BaseVM {

        private TCPanel leftPanel;
        private TCPanel rightPanel;
        private TCPanel focusedPanel;
        public WindowVM() {
            leftPanel = new TCPanel();
            rightPanel = new TCPanel();
            focusedPanel = null;
        }

        #region properties
        public string Path_Left {
            get { return leftPanel.Path; }
        }

        public string Path_Right {
            get { return rightPanel.Path; }
        }

        public string SelectedDrive_Left {
            set {
                if(value != null) {
                    leftPanel.getDirContent(value);
                    onPropertyChanged(nameof(SelectedDrive_Left), nameof(Directory_Left), nameof(Path_Left));
                }
            }
        }

        public string SelectedDrive_Right {
            set {
                if(value != null) {
                    rightPanel.getDirContent(value);
                    onPropertyChanged(nameof(SelectedDrive_Right), nameof(Directory_Right), nameof(Path_Right));
                }
            }
        }

        private string selectedFile_Left;
        public string SelectedFile_Left {
            set { selectedFile_Left = value; }
        }

        private string selectedFile_Right;
        public string SelectedFile_Right {
            set { selectedFile_Right = value; }
        }

        public string[] DriveListVM {
            get { return leftPanel.DriveList; }
        }

        public List<string> Directory_Left {
            get { return leftPanel.Folder; }
        }

        public List<string> Directory_Right {
            get { return rightPanel.Folder; }
        }
        #endregion
        #region commands
        private ICommand enterSelectedFolder_Left = null;
        public ICommand EnterSelectedFolder_Left {
            get {
                if(enterSelectedFolder_Left == null) {
                    enterSelectedFolder_Left = new RelayCommand(
                        x => {
                            if(Directory_Left == null)
                                return;

                            if(!leftPanel.openFile(selectedFile_Left)) {
                                MessageBox.Show(resource.MsgError,
                                resource.MsgError,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            }
                            else
                                onPropertyChanged(nameof(Directory_Left), nameof(Path_Left));
                        },
                        x => true
                        );
                }
                return enterSelectedFolder_Left;
            }
        }

        private ICommand enterSelectedFolder_Right = null;
        public ICommand EnterSelectedFolder_Right {
            get {
                if(enterSelectedFolder_Right == null) {
                    enterSelectedFolder_Right = new RelayCommand(
                        x => {
                            if(!rightPanel.openFile(selectedFile_Right)) {
                                MessageBox.Show(resource.MsgError,
                                resource.MsgError,
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            }
                            else
                                onPropertyChanged(nameof(Directory_Right), nameof(Path_Right));
                        },
                        x => true
                        );
                }
                return enterSelectedFolder_Right;
            }
        }

        private ICommand focusedPanel_Left = null;
        public ICommand FocusedPanel_Left {
            get {
                if(focusedPanel == null)
                    focusedPanel_Left = new RelayCommand(
                        x => focusedPanel = leftPanel,
                        x => true);
                return focusedPanel_Left;
            }
        }

        private ICommand focusedPanel_Right = null;
        public ICommand FocusedPanel_Right {
            get {
                if(focusedPanel == null)
                    focusedPanel_Right = new RelayCommand(
                        x => focusedPanel = rightPanel,
                        x => true);
                return focusedPanel_Right;
            }
        }

        private ICommand copy = null;
        public ICommand Copy {
            get {
                if(copy == null)
                    copy = new RelayCommand(
                        x => CopyFileOrDir(),
                        x => true);

                return copy;
            }
        }

        private void CopyFileOrDir() {
            if(focusedPanel == leftPanel) {
                FilesIO.Copy(selectedFile_Left, leftPanel.Path, rightPanel.Path);

                rightPanel.getDirContent(rightPanel.Path);
                onPropertyChanged(nameof(Directory_Right));
            }
            if(focusedPanel == rightPanel) {
                FilesIO.Copy(selectedFile_Right, rightPanel.Path, leftPanel.Path);

                leftPanel.getDirContent(leftPanel.Path);
                onPropertyChanged(nameof(Directory_Left));
            }
        }
        #endregion




    }
}

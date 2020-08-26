﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TC.View
{
    /// <summary>
    /// Logic interaction for PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC() {
            InitializeComponent();
        }

        #region Register DependencyProperty

        //Drive stuff
        public static readonly DependencyProperty DriveListDP = DependencyProperty.Register(
           nameof(DriveListPanel), typeof(string[]), typeof(PanelTC), new FrameworkPropertyMetadata(null));

        public string[] DriveListPanel
        {
            get { return (string[])GetValue(DriveListDP); }
            set { SetValue(DriveListDP, value); }
        }

        public static readonly DependencyProperty SelectedDriveDP = DependencyProperty.Register(
           nameof(SelectedDrive), typeof(string), typeof(PanelTC), new FrameworkPropertyMetadata(null));

        public string SelectedDrive
        {
            get { return (string)GetValue(SelectedDriveDP); }
            set { SetValue(SelectedDriveDP, value); }
        }

        //File List
        public static readonly DependencyProperty FileListDP = DependencyProperty.Register(
           nameof(FileList), typeof(List<string>), typeof(PanelTC), new FrameworkPropertyMetadata(null));

        public string FileList
        {
            get { return (string)GetValue(FileListDP); }
            set { SetValue(FileListDP, value); }
        }

        //Selected File
        public static readonly DependencyProperty SelectedFileDP = DependencyProperty.Register(
            nameof(SelectedFile), typeof(string), typeof(PanelTC), new FrameworkPropertyMetadata(null));

        public string SelectedFile
        {
            get { return (string)GetValue(SelectedFileDP); }
            set { SetValue(SelectedFileDP, value); }
        }

        //Path
        public static readonly DependencyProperty PathDP = DependencyProperty.Register(
            nameof(Path), typeof(string), typeof(PanelTC), new FrameworkPropertyMetadata(null));

        public string Path
        {
            get { return (string)GetValue(PathDP); }
            set { SetValue(PathDP, value); }
        }

        #endregion

        #region Register Events
        public static readonly RoutedEvent ChangeDriveEvent =
        EventManager.RegisterRoutedEvent(nameof(ChangeDrive),
             RoutingStrategy.Bubble, typeof(RoutedEventHandler),
             typeof(PanelTC));

        public event RoutedEventHandler ChangeDrive
        {
            add { AddHandler(ChangeDriveEvent, value); }
            remove { RemoveHandler(ChangeDriveEvent, value); }
        }

        public static readonly RoutedEvent ChangeSelectedFileEvent =
        EventManager.RegisterRoutedEvent(nameof(ChangeSelectedFile),
             RoutingStrategy.Bubble, typeof(RoutedEventHandler),
             typeof(PanelTC));

        public event RoutedEventHandler ChangeSelectedFile
        {
            add { AddHandler(ChangeSelectedFileEvent, value); }
            remove { RemoveHandler(ChangeSelectedFileEvent, value); }
        }

        public static readonly RoutedEvent MouseDoubleClickEvent =
        EventManager.RegisterRoutedEvent(nameof(MouseDoubleClick),
                 RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                 typeof(PanelTC));

        public event RoutedEventHandler MouseDoubleClick
        {
            add { AddHandler(MouseDoubleClickEvent, value); }
            remove { RemoveHandler(MouseDoubleClickEvent, value); }
        }

        //Focused Panel Event
        public static readonly RoutedEvent FocusedPanelEvent =
        EventManager.RegisterRoutedEvent(nameof(FocusedPanel),
                 RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                 typeof(PanelTC));

        public event RoutedEventHandler FocusedPanel
        {
            add { AddHandler(FocusedPanelEvent, value); }
            remove { RemoveHandler(FocusedPanelEvent, value); }
        }

        #endregion



        private void FileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => RaiseEvent(new RoutedEventArgs(ChangeSelectedFileEvent));

        private void Drive_SelectionChanged(object sender, SelectionChangedEventArgs e)
            => RaiseEvent(new RoutedEventArgs(ChangeDriveEvent));

        private void FileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            => RaiseEvent(new RoutedEventArgs(MouseDoubleClickEvent));

        private void FileList_GotFocus(object sender, RoutedEventArgs e)
            => RaiseEvent(new RoutedEventArgs(FocusedPanelEvent));
    }
}

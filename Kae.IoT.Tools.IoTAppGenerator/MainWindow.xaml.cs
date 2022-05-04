﻿using Kae.IoT.PnP.Generator;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kae.IoT.Tools.IoTAppGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> dtInterfaces = new ObservableCollection<string>();
        IDictionary<string, IDictionary<string, string>> appKindsForLang = new Dictionary<string, IDictionary<string, string>>()
        {
            {"C#", new Dictionary<string,string>{{"Device App","deviceapp"},{ "Service", "service" },{ "IoT Edge Module", "edge" } } },
            {"C/C++", new Dictionary<string,string>{{"Device SDK", "device" }, { "Embedded SDK", "embedded"} } },
            {"Python", new Dictionary<string,string>{ } }
        };

        ObservableCollection<string> kindList = new ObservableCollection<string>();

        private IoTPnPCodeGenerater generator;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            lbInterfaces.ItemsSource = dtInterfaces;
            cbGenKind.ItemsSource = kindList;
            tvGenerated.ItemsSource = generatedViewerItems;
        }

        private void buttonSelectDTDLFile_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "DTDL (.json)|*.json";
            dialog.DefaultExt = ".json";
            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                tbDTDLFileName.Text = dialog.FileName;
                LoadIoTPnPDTDL();
                buttonParse.IsEnabled = true;
            }
        }

        private void LoadIoTPnPDTDL()
        {
            using (var reader = new StreamReader(tbDTDLFileName.Text))
            {
                tbIoTPnPDTDL.Text = reader.ReadToEnd();
            }
        }

        private async void buttonParse_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIoTPnPDTDL.Text))
            {
                generator = new IoTPnPCodeGenerater();
                await generator.LoadModel(tbIoTPnPDTDL.Text);
                var result = await generator.Parse();

                dtInterfaces.Clear();
                foreach (var item in generator.DTInterfaces)
                {
                    dtInterfaces.Add(item.Key);
                }
            }
        }

        private void lbInterfaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonSelectGenFolder.IsEnabled = true;
        }

        private void buttonSelectGenFolder_Click(object sender, RoutedEventArgs e)
        {
            using (var folderDialog = new CommonOpenFileDialog()
            { IsFolderPicker = true })
            {
                if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    tbGenFolderPath.Text = folderDialog.FileName;
                    cbLanguage.IsEnabled = true;
                    buttonRefleshGeneratedViewer.IsEnabled = true;
                    RefleshGeneratedViewer();
                }
            }

        }

        private async void buttonGenerate_Click(object sender, RoutedEventArgs e)
        {
            var selectedLanguage = cbLanguage.SelectedItem.ToString();
            var selectedInterface = lbInterfaces.SelectedItem.ToString();
            var exeType = appKindsForLang[selectedLanguage][cbGenKind.SelectedItem.ToString()];
            await generator.GenerateProject(selectedInterface, tbGenFolderPath.Text, tbAppName.Text, tbNamespace.Text, exeType);

            RefleshGeneratedViewer();
        }

        private void cbLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedLanguage = ((ComboBoxItem)cbLanguage.SelectedItem).Content.ToString();

            if (selectedLanguage=="C/C++" || selectedLanguage== "Python")
            {
                MessageBox.Show("Under construction. Coming soon may be.");
                return;
            }
            kindList.Clear();
            foreach (var kindKey in appKindsForLang[selectedLanguage].Keys)
            {
                kindList.Add((string)kindKey);
            }
            if (kindList.Count > 0)
            {
                cbGenKind.IsEnabled = true;
            }
        }

        private void cbGenKind_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buttonGenerate.IsEnabled = true;
        }

        private void buttonRefleshGeneratedViewer_Click(object sender, RoutedEventArgs e)
        {
            RefleshGeneratedViewer();
        }

        ObservableCollection<TreeViewData> generatedViewerItems = new ObservableCollection<TreeViewData>();
        private void RefleshGeneratedViewer()
        {
            generatedViewerItems.Clear();
            var di = new DirectoryInfo(tbGenFolderPath.Text);
            foreach(var child in di.GetDirectories())
            {
                generatedViewerItems.Add(CreateDirectoryTreeViewItem(child));
            }
            foreach(var child in di.GetFiles())
            {
                generatedViewerItems.Add(new TreeViewData() { Name = child.Name });
            }
        }

        private TreeViewData CreateDirectoryTreeViewItem(DirectoryInfo directoryInfo)
        {
            var children = new List<TreeViewData>();
            var tvData = new TreeViewData() { Name = directoryInfo.Name, Children = children };
            foreach(var child in directoryInfo.GetDirectories())
            {
                var childTVData = CreateDirectoryTreeViewItem(child);
                children.Add(childTVData);
            }
            foreach(var child in directoryInfo.GetFiles())
            {
                var childTVData = new TreeViewData() { Name = child.Name, FullPath = child.FullName };
                children.Add(childTVData);
            }
            return tvData;
        }

        private class TreeViewData
        {
            public string Name { get; set; }
            public IEnumerable<TreeViewData> Children { get; set; }
            public string FullPath { get; set; }
        }

        private void tvGenerated_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (!string.IsNullOrEmpty(((TreeViewData)e.NewValue).FullPath))
            {
                Process.Start("notepad", ((TreeViewData)e.NewValue).FullPath);
            }
        }
    }
}

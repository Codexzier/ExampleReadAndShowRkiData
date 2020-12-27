using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OverviewRkiData.Controls.FolderBrowser
{
    /// <summary>
    /// Interaction logic for FolderBrowserControl.xaml
    /// </summary>
    public partial class FolderBrowserControl : UserControl
    {
        public string SelectedDirectoryPath
        {
            get => this.GetValue(SelectedDirectoryPathProperty).ToString();
            set => this.SetValue(SelectedDirectoryPathProperty, value);
        }

        public static readonly DependencyProperty SelectedDirectoryPathProperty =
            DependencyProperty.RegisterAttached(
                nameof(SelectedDirectoryPath),
                typeof(string),
                typeof(FolderBrowserControl),
                new PropertyMetadata(string.Empty));

        public FolderBrowserControl() => this.InitializeComponent();

        public override void OnApplyTemplate() => this.LoadCurrentFolder(Environment.CurrentDirectory);

        private void LoadCurrentFolder(string currentDirectory)
        {
            var list = new List<FolderBrowserItem>
            {
                new FolderBrowserItem(currentDirectory, true)
            };

            foreach (var item in Directory.GetDirectories(currentDirectory))
            {
                list.Add(new FolderBrowserItem(item));
            }

            this.ListBoxFolder.ItemsSource = list;
        }

        private void ListBoxFolder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxFolder.SelectedItem is FolderBrowserItem item)
            {
                if (item.ReturnFolderItem)
                {
                    var di = new DirectoryInfo(item.CompletePath);
                    if (di.Parent == null)
                    {
                        return;
                    }

                    this.LoadCurrentFolder(di.Parent.FullName);
                    return;
                }

                this.LoadCurrentFolder(item.CompletePath);
            }
        }

        private void ListBoxFolder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxFolder.SelectedItem is FolderBrowserItem item)
            {
                this.textBoxCompleteFolderPath.Text = item.CompletePath;
                this.SelectedDirectoryPath = item.CompletePath;
            }
        }
    }
}

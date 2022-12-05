﻿using System;
using System.Collections.Generic;
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
using System.ServiceModel.Syndication;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using Path = System.IO.Path;
using System.Security;

namespace RssReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ReadFromFile();
        }

        private void SaveLinkToFile(object sender, RoutedEventArgs e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki.txt");

            if (!File.Exists(dir))
            {
                using (StreamWriter sw = new StreamWriter(dir))
                {
                    sw.WriteLine(linkTextBox.Text);
                }
            }
            else
            {
                File.AppendAllText(dir, $"\n{linkTextBox.Text}");
            }

            ReadFromFile();
        }

        private void ReadFromFile()
        {
            ObservableCollection<string> links = new ObservableCollection<string>();

            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki.txt");
            //pobranie wartosci z pliku z zapisanymi linkami
            try
            {
                using (StreamReader sr = new StreamReader(dir))
                {
                    string line;
                    linkList.ItemsSource = links;
                    while ((line = sr.ReadLine()) != null)
                    {
                        links.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not find the file!");
            }
        }

        private void SearchLink(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    WriteTitle(Link.Text);

            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Could not find the website!");
            //}
        }

        private void LinkList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (LinkList.SelectedIndex >= 0)
            //    {
            //        //Take selected link from list of links as link
            //        dynamic link = LinkList.SelectedItem as dynamic;


            //        WriteTitle(link);

            //    }
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Could not find the website!");
            //}
        }

        private void WriteTitle(string link)
        {
            //XmlReader reader = XmlReader.Create(link); // Reads a link from TextBox

            //// Change of settings in order to fix DTD issue
            //XmlReaderSettings readerSettings = new XmlReaderSettings();
            //readerSettings.DtdProcessing = DtdProcessing.Parse;

            //SyndicationFeed feed = SyndicationFeed.Load(reader);

            ////Reads a Title 
            //foreach (SyndicationItem item in feed.Items)
            //{
            //    Content.Text = item.Title.Text.ToString();
            //}
        }

        private void ClearPlaceholder(object sender, RoutedEventArgs e)
        {
            linkTextBox.FontWeight = FontWeights.Normal;
            linkTextBox.FontStyle = FontStyles.Normal;
            linkTextBox.Text = string.Empty;
        }

        private void ButtonClicked(object sender, RoutedEventArgs e)
        {
            if (linkTextBox.Text != "Enter a link!")
                SaveLinkToFile(sender, e);
            linkTextBox.FontWeight = FontWeights.ExtraLight;
            linkTextBox.FontStyle = FontStyles.Italic;
            linkTextBox.Text = "Enter a link!";
        }

        private void AddFilterWorld(object sender, RoutedEventArgs e)
        {

        }

        private void AddFilterFood(object sender, RoutedEventArgs e)
        {

        }

        private void AddFilterSport(object sender, RoutedEventArgs e)
        {

        }

        private void AddFilterWeather(object sender, RoutedEventArgs e)
        {

        }

        private void AddFilterMusic(object sender, RoutedEventArgs e)
        {

        }

        private void ResetFilters(object sender, RoutedEventArgs e)
        {

        }
    }
}

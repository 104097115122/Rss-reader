using System;
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
            //pobranie wartosci z pliku z zapisanymi linkami
            try
            {
                using (StreamReader sr = new StreamReader(@"D:\ProjektWPF\Rss-reader\lista.txt")) 
                {
                    string line;
                    while ((line = sr.ReadLine()) != null) 
                    {
                        LinkList.Items.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Could not find the file!");
            }
        }

        private void AddLink_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddLink_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void AddLink1_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    XmlReader reader = XmlReader.Create(AddLink.Text); // Reads a link from TextBox

            //    // Change of settings in order to fix DTD issue
            //    XmlReaderSettings readerSettings = new XmlReaderSettings();
            //    readerSettings.DtdProcessing = DtdProcessing.Parse;

            //    SyndicationFeed feed = SyndicationFeed.Load(reader);

            //    //Reads a Title 
            //    foreach (SyndicationItem item in feed.Items)
            //    {
            //        Content.Text = item.Title.Text.ToString();
            //    }

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Search(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Link.Text);
        }

        private void SaveLinkToFile(object sender, RoutedEventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\ProjektWPF\Rss-reader\lista.txt")) 
            {
                sw.WriteLine(Link.Text); 
            }
        }
    }
}

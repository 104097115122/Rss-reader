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
using System.Collections.ObjectModel;
using Path = System.IO.Path;
using System.Security;
using System.Text.RegularExpressions;

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

        private IEnumerable<string> ReadLinksToList(string dir)
        {
            using (StreamReader reader = File.OpenText(dir))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        public static string DeleteHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
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


        private void ResetFilters(object sender, RoutedEventArgs e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki.txt");
            string contentHtml = "";
            var logFile = ReadLinksToList(dir);
            foreach (var s in logFile)
            {
                if (string.IsNullOrEmpty(s))
                {
                    contentHtml += "";
                }
                else
                {
                    XmlReader reader = XmlReader.Create(s); // Reads a link from TextBox

                    // Change of settings in order to fix DTD issue
                    XmlReaderSettings readerSettings = new XmlReaderSettings();
                    readerSettings.DtdProcessing = DtdProcessing.Parse;

                    SyndicationFeed feed = SyndicationFeed.Load(reader);


                    //Reads a Title 
                    foreach (SyndicationItem item in feed.Items)
                    {
                        if (item.Title != null)
                        {
                            contentHtml += item.Title.Text.ToString();
                            contentHtml += "\n";
                        }
                        if (item.PublishDate != null)
                        {
                            contentHtml += item.PublishDate.DateTime.ToString();
                            contentHtml += "\n";
                        }
                        if (item.Content != null)
                        {
                            TextSyndicationContent txt = (TextSyndicationContent)item.Content;
                            string myContent = txt.Text;
                            contentHtml += myContent.ToString();
                            contentHtml += "\n";
                        }
                        if (item.Summary != null)
                        {
                            contentHtml += item.Summary.Text.ToString();
                            contentHtml += "\n";
                        }

                        contentHtml += "\n";
                        contentHtml += "\n";

                        contentHtml = DeleteHtml(contentHtml);


                    }
                }





            }
            WindowAllLinks secondWindow = new WindowAllLinks(contentHtml);
            secondWindow.Show();
            this.Close();

        }



        private void DeleteFromList(object sender, RoutedEventArgs e)
        {
            string selectedLink = linkList.SelectedIndex.ToString();
            int lineToDelete = Int32.Parse(selectedLink);


            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki.txt");
            var dir2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki2.txt");
            string line = null;
            int lineNumber = 0;

            /*            using (StreamReader reader = new StreamReader(dir))
                        {
                            using (StreamWriter writer = new StreamWriter(dir2))
                            {
                                while ((line = reader.ReadLine()) != null)
                                {
                                    lineNumber++;

                                    if (lineNumber == lineToDelete)
                                        continue;

                                    writer.WriteLine(line);
                                }
                            }
                        }*/

            string[] readText = File.ReadAllLines(dir);

            File.WriteAllText(dir, String.Empty);
            using (StreamWriter writer = new StreamWriter(dir))
            {
                foreach (string s in readText)
                {
                    if (!s.Equals(lineToDelete))
                    {
                        writer.WriteLine(s);
                    }
                }
            }


        }

        private void RefreshLinks(object sender, RoutedEventArgs e)
        {
            var dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki.txt");
            var dir2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Linki2.txt");
            string line = null;


                using (StreamWriter writer = new StreamWriter(dir))
                {

                        writer.WriteLine(line);

                }
            
            ReadFromFile();
        }

        private void ReadSingleLink(object sender, RoutedEventArgs e)
        {
            string contentHtml = "";
            var s = linkList.SelectedItem.ToString();
            if (string.IsNullOrEmpty(s))
            {
                contentHtml += "";
            }
            else
            {
                XmlReader reader = XmlReader.Create(s); // Reads a link from TextBox

                // Change of settings in order to fix DTD issue
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.DtdProcessing = DtdProcessing.Parse;

                SyndicationFeed feed = SyndicationFeed.Load(reader);


                //Reads a Title 
                foreach (SyndicationItem item in feed.Items)
                {
                    if (item.Title != null)
                    {
                        contentHtml += item.Title.Text.ToString();
                        contentHtml += "\n";
                    }
                    if (item.PublishDate != null)
                    {
                        contentHtml += item.PublishDate.DateTime.ToString();
                        contentHtml += "\n";
                    }
                    if (item.Content != null)
                    {
                        TextSyndicationContent txt = (TextSyndicationContent)item.Content;
                        string myContent = txt.Text;
                        contentHtml += myContent.ToString();
                        contentHtml += "\n";
                    }
                    if (item.Summary != null)
                    {
                        contentHtml += item.Summary.Text.ToString();
                        contentHtml += "\n";
                    }

                    contentHtml += "\n";
                    contentHtml += "\n";

                    contentHtml = DeleteHtml(contentHtml);


                }
            }
            WindowAllLinks secondWindow = new WindowAllLinks(contentHtml);
            secondWindow.Show();
            this.Close();
        }
    }
}

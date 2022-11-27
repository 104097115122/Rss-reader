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
        }

        private void AddLink_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddLink_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void AddLink1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlReader reader = XmlReader.Create(AddLink.Text); // Reads a link from TextBox

                // Change of settings in order to fix DTD issue
                XmlReaderSettings readerSettings = new XmlReaderSettings();
                readerSettings.DtdProcessing = DtdProcessing.Parse;

                SyndicationFeed feed = SyndicationFeed.Load(reader);

                //Reads a Title 
                foreach (SyndicationItem item in feed.Items)
                {
                    Content.Text = item.Title.Text.ToString();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}

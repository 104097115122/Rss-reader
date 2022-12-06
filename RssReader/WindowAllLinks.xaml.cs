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
using System.Windows.Shapes;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;
using Path = System.IO.Path;
using System.Reflection.Emit;

namespace RssReader
{
    /// <summary>
    /// Logika interakcji dla klasy WindowAllLinks.xaml
    /// </summary>
    public partial class WindowAllLinks : Window
    {
        public WindowAllLinks()
        {
            InitializeComponent();
        }
        public static string DeleteHtml(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
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
        private void BackToMain(object sender, RoutedEventArgs e)
        {
            MainWindow secondWindow = new MainWindow();
            secondWindow.Show();
            this.Close();
        }
        public WindowAllLinks(string text) : this()
        {
            contentBox.Text = text;

        }

 

    }
}




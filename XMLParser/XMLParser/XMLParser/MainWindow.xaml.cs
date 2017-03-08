using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Parser;
using BDOperational;
namespace XMLParser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        

        }

        private void Menu1_Click(object sender, RoutedEventArgs e)
        {
            ConnectionSettings con;
            con.Data_Source = "PIRKO-VV\\PIRKO";
            con.Initial_Catalog = "Learning";
            con.user_name = "rg";
            NorthwindDataSet n = new NorthwindDataSet();

            DataTable data;
            data = new XmlParser("doc.xml").Parse().Tables[0];
            n.insert(con, data);
        }

        private void Menu2_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = new XmlParser("doc.xml").Parse().Tables[0].DefaultView;
        }
    }
}

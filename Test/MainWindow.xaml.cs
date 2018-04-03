using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        JournalDBEntities db;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new JournalDBEntities();
            g1.ItemsSource = db.Data.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddElement().Show();
        }

        //private void binddatagrid()
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = ConfigurationManager.ConnectionStrings["connData"].ConnectionString;
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "select * from[Data]";
        //    cmd.Connection = con;
        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataTable dt = new DataTable("Data");
        //    da.Fill(dt);

        //    g1.ItemsSource = dt.DefaultView;
        //}
    }
}

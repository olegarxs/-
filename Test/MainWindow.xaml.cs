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

        JournalDBEntities1 db = new JournalDBEntities1();
        

        public MainWindow()
        {
            InitializeComponent();
            //this.WindowState = WindowState.Maximized;  // для водителей
            //this.WindowStyle = System.Windows.WindowStyle.None;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            g1.ItemsSource = db.Data.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddElement().Show();
        }

        public void UpdataTable()
        {
            g1.ItemsSource = null;
            g1.ItemsSource = db.Data.ToList();
        }

        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            g1.ItemsSource = null;
            g1.ItemsSource = db.Data.ToList();
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

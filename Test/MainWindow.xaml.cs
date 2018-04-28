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
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

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
                UpdataTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddElement().Show();
        }

        public async Task UpdataTable()
        {
            while (true)
            {
                g1.ItemsSource = null;
                
                g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse();
                
                await Task.Delay(5000);
            }
        }

        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            g1.ItemsSource = null;
            g1.ItemsSource = db.Data.ToList();
        }

        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void g1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            Data row = (Data)gd.SelectedItem;
            epID.Text = row.id.ToString();
            epName.Text = row.name.ToString();
            epApplicationDateAndTime.Text = row.applicationDateAndTime.ToString();
            epDateAndTimeOfCarProvision.Text = row.dateAndTimeOfCarProvision.ToString();
            epPurposesOfUsingAuto.Text = row.purposesOfUsingAuto.ToString();
            epRoute.Text = row.route.ToString();
            epNameDocument.Text = row.route.ToString();
            epLastName.Text = row.lastName.ToString();
            epCargo.Text = row.cargo.ToString();
            epApplicationStatus.Text = row.applicationStatus.ToString();
            
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

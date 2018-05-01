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

        JournalDBEntities db = new JournalDBEntities();

        public MainWindow()
        {
            InitializeComponent();
            settingForSimplePiople();
            //vod.SelectedValuePath;
            //vod.ItemsSource = db.Driver.Select(x => x.name).ToList();

            //this.WindowState = WindowState.Maximized;  // для водителей
            //this.WindowStyle = System.Windows.WindowStyle.None;
        }

        public List<Driver> drivers { get; set; }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            drivers = db.Driver.ToList();
            DataContext = drivers;
            epDriver.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddElement().Show();
        }

        public async Task UpdataTable()
        {
            
                var dataGrid = db.Data.ToList().AsEnumerable().Reverse();
                g1.ItemsSource = dataGrid;
                await Task.Delay(5000);
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
            epNameDocument.Text = row.nameDocument.ToString();
            epLastName.Text = row.Employees.fullName.ToString() ;
            epCargo.Text = row.cargo.ToString();
            epDriver.SelectedValue = row.id_driver.Value.ToString();
            epApplicationStatus.Text = row.applicationStatus.ToString(); 
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            UpdataTable();
        }

        private void falseReadEditPanel()
        {
            foreach (var item in editPanel.Children)
            {
                foreach (var i in ((WrapPanel)item).Children)
                {
                    if(i is TextBox) { 
                        ((TextBox)i).IsReadOnly = true;
                    }else if(i is ComboBox)
                    {
                        ((ComboBox)i).IsEnabled = false;
                    }
                }
            }
        }
        private void settingForSimplePiople()
        {
            falseReadEditPanel();
            addButton.Visibility = Visibility.Hidden;
            acceptOrderButton.Visibility = Visibility.Hidden;
        }

        //private void vod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    ID.Text = ((ComboBox)sender).SelectedValue.ToString();
        //}

        private void acceptOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int cell = Convert.ToInt32(epID.Text);
            var cellEdit = db.Data.Where(w => w.id == cell).FirstOrDefault();
            cellEdit.name = epName.Text;
            cellEdit.applicationDateAndTime = epApplicationDateAndTime.Text;
            cellEdit.dateAndTimeOfCarProvision = epDateAndTimeOfCarProvision.Text;
            cellEdit.purposesOfUsingAuto = epPurposesOfUsingAuto.Text;
            cellEdit.route = epRoute.Text;
            cellEdit.nameDocument = epNameDocument.Text;
            cellEdit.id_employe = int.Parse(epLastName.Text);
            cellEdit.cargo = epCargo.Text;
            cellEdit.id_driver = int.Parse(epDriver.SelectedValue.ToString());
            cellEdit.applicationStatus = "Выполняется";
            db.SaveChanges();
            g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse();
        }

        private void settingForDriver()
        {
            falseReadEditPanel();
            addButton.Visibility = Visibility.Hidden;
            epDriver.IsEnabled = true;
        }

        private void settingForChief()
        {
            falseReadEditPanel();
            addButton.Visibility = Visibility.Visible;
        }
        private void settingForBoss()
        {

        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int cell = Convert.ToInt32(epID.Text);
            var cellEdit = db.Data.Where(w => w.id == cell).FirstOrDefault();
            cellEdit.name = epName.Text;
            cellEdit.applicationDateAndTime = epApplicationDateAndTime.Text;
            cellEdit.dateAndTimeOfCarProvision = epDateAndTimeOfCarProvision.Text;
            cellEdit.purposesOfUsingAuto = epPurposesOfUsingAuto.Text;
            cellEdit.route = epRoute.Text;
            cellEdit.nameDocument = epNameDocument.Text;
            cellEdit.id_employe = int.Parse(epLastName.Text);
            cellEdit.cargo = epCargo.Text;
            cellEdit.id_driver = int.Parse(epDriver.SelectedValue.ToString());
            cellEdit.applicationStatus = epApplicationStatus.Text;
            db.SaveChanges();
            g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse();
        }
    }
}

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
            checkProperty();

            //ID.Text = Properties.Settings.Default.rights.ToString();
            //Properties.Settings.Default.rights = 12;
            //Properties.Settings.Default.Save(); // для сохранение данных


            //this.WindowState = WindowState.Maximized;  // для водителей
            //this.WindowStyle = System.Windows.WindowStyle.None;
        }

        private void checkProperty()
        {
            
            
            ID.Text = Properties.Settings.Default.rights.ToString();
            switch (Properties.Settings.Default.rights)
            {
                case (byte)Rights.People:
                    settingForSimplePeople();
                    break;
                case (byte)Rights.Driver:
                    settingForDriver();
                    break;
                case (byte)Rights.Chief:
                    settingForChief();
                    break;
                case (byte)Rights.Boss:
                    settingForBoss();
                    break;
            }
        }

        enum Rights
        {
            People,
            Driver,
            Chief,
            Boss
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
                var dataGrid = db.Data.ToList().AsEnumerable().Reverse().Take(5);
                g1.ItemsSource = dataGrid;
                await Task.Delay(5000);
        }

        private void btnUPDATE_Click(object sender, RoutedEventArgs e)
        {
            g1.ItemsSource = null;
            g1.ItemsSource = db.Data.ToList();
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
            epDriver.SelectedValue =(row.id_driver is null) ? "0" : row.id_driver.Value.ToString();
            epApplicationStatus.Text = row.applicationStatus.ToString(); 
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            UpdataTable();
        }

        private void ReadEditPanel(bool boolVar)
        {
            foreach (var item in editPanel.Children)
            {
                foreach (var i in ((WrapPanel)item).Children)
                {
                    if(i is TextBox) { 
                        (i as TextBox).IsReadOnly = boolVar;
                    }else if(i is ComboBox)
                    {
                        (i as ComboBox).IsEnabled = !boolVar;
                    }
                }
            }
        }
        private void settingForSimplePeople()
        {
            setting(true, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, false);
        }

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
            setting(true, Visibility.Hidden, Visibility.Hidden, Visibility.Visible, true);
        }

        private void settingForChief()
        {
            setting(true,Visibility.Visible,Visibility.Hidden, Visibility.Hidden,false);
        }

        private void setting(bool onlyReadEditPanel,Visibility visibilityAddButton,
            Visibility visibilityEditButton, Visibility visibilityAcceptOrderButton, bool onlyReadCbDriver)
        {
            ReadEditPanel(onlyReadEditPanel);
            addButton.Visibility = visibilityAddButton;
            editButton.Visibility = visibilityEditButton;
            acceptOrderButton.Visibility = visibilityAcceptOrderButton;
            epDriver.IsEnabled = onlyReadCbDriver;
        }

        private void settingForBoss()
        {
            setting(false, Visibility.Visible, Visibility.Visible, Visibility.Hidden, true);
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

        private void btBoss_Click(object sender, RoutedEventArgs e)
        {
            new Entrance().Show();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            settingForDriver();
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            settingForChief();
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            settingForBoss();
        }
    }
}

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
        public int id_employe;

        public MainWindow()
        {
            InitializeComponent();
            UpdataTable();
            //Properties.Settings.Default.rights = 1;
            //Properties.Settings.Default.Save();
            checkProperty(Properties.Settings.Default.rights);
            //ID.Text = Properties.Settings.Default.rights.ToString();
            // для сохранение данных
            
            
            //this.WindowState = WindowState.Maximized;  // для водителей
            //this.WindowStyle = System.Windows.WindowStyle.None;
        }

        
        
        
                                             

      
        private void checkProperty(int number)
        {
            
            
            //ID.Text = Properties.Settings.Default.rights.ToString();

            switch (number)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            epDriver.DataContext = db.Driver.ToList();
            
            epDriver.SelectedIndex = 0;

            epLastName.DataContext = db.Employees.ToList();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new AddElement().Show();
        }

        public async void UpdataTable()
        {
            while (true) {
                                       
                    g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse().Take(10);
                    await Task.Delay(15000);
                
            }
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
            epName.Text = row.Employees.department.ToString();
            epApplicationDateAndTime.Text = row.applicationDateAndTime.ToString();
            epDateAndTimeOfCarProvision.Text = row.dateAndTimeOfCarProvision.ToString();
            epPurposesOfUsingAuto.Text = row.purposesOfUsingAuto.ToString();
            epRoute.Text = row.route.ToString();
            epNameDocument.Text = row.nameDocument.ToString();
            epLastName.Text = row.Employees.fullName.ToString();
            epCargo.Text = row.cargo.ToString();
            epDriver.SelectedValue =(row.id_driver is null) ? "0" : row.id_driver.Value.ToString();
            epApplicationStatus.Text = row.applicationStatus.ToString(); 
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            //UpdataTable();
        }

        private void ReadEditPanel(bool boolVar)
        {
            foreach (var item in editPanel.Children)
            {
                if(item is WrapPanel)
                {
                    
                }
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
            this.Title = "Добро пожаловать";
            setting(true, Visibility.Hidden, Visibility.Hidden, Visibility.Hidden, false);
        }

        private void acceptOrderButton_Click(object sender, RoutedEventArgs e)
        {
            int cell = Convert.ToInt32(epID.Text);
            var cellEdit = db.Data.Where(w => w.id == cell).FirstOrDefault();  
            
            try
            {
                cellEdit.id_driver = int.Parse(epDriver.SelectedValue.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Выберите водителя");
            }
            
            cellEdit.applicationStatus = (sender as Button).Tag.ToString();
            db.SaveChanges();
            g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse();
        }

        private void settingForDriver()
        {
            this.Title = "Добро пожаловать";
            setting(true, Visibility.Collapsed, Visibility.Collapsed, Visibility.Visible, true);
        }

        private void settingForChief()
        {
            id_employe = Properties.Settings.Default.idUser;
            this.Title = "Добро пожаловать " + db.Employees.ToList().Where(x => x.id == id_employe).Select(x => x.fullName).First().ToString();
            setting(true,Visibility.Visible,Visibility.Collapsed, Visibility.Collapsed,false);
            
        }

        private void setting(bool onlyReadEditPanel,Visibility visibilityAddButton,
            Visibility visibilityEditButton, Visibility visibilityAcceptOrderButton, bool onlyReadCbDriver)
        {
            ReadEditPanel(onlyReadEditPanel);
            addButton.Visibility = visibilityAddButton;
            editButton.Visibility = visibilityEditButton;
            btBoxDriver.Visibility = visibilityAcceptOrderButton;
            epDriver.IsEnabled = onlyReadCbDriver;
        }

        private void settingForBoss()
        {
            id_employe = Properties.Settings.Default.idUser;
            this.Title = "Добро пожаловать " + db.Employees.ToList().Where(x => x.id == id_employe).Select(x => x.fullName).First().ToString();
            setting(false, Visibility.Visible, Visibility.Visible, Visibility.Collapsed, true);
            btManageUser.Visibility = Visibility.Visible;
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            int cell = Convert.ToInt32(epID.Text);
            var cellEdit = db.Data.Where(w => w.id == cell).FirstOrDefault();
            cellEdit.applicationDateAndTime = epApplicationDateAndTime.Text;
            cellEdit.dateAndTimeOfCarProvision = epDateAndTimeOfCarProvision.Text;
            cellEdit.purposesOfUsingAuto = epPurposesOfUsingAuto.Text;
            cellEdit.route = epRoute.Text;
            cellEdit.nameDocument = epNameDocument.Text;
            cellEdit.id_employe = db.Employees.Where(x =>x.fullName == epLastName.Text).First().id;
            cellEdit.cargo = epCargo.Text;
            cellEdit.id_driver = epDriver.SelectedValue != null ? int.Parse(epDriver.SelectedValue.ToString()) : (int?)null ;
            cellEdit.applicationStatus = epApplicationStatus.Text;
            db.SaveChanges();
            g1.ItemsSource = db.Data.ToList().AsEnumerable().Reverse();
        }

        private void btBoss_Click(object sender, RoutedEventArgs e)
        {

            new Entrance().Show();
            this.Close();
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

        private void btManageUser_Click(object sender, RoutedEventArgs e)
        {
            new ManageUsers().Show();
        }

        private void btDriver_Click(object sender, RoutedEventArgs e)
        {
            Entrance entrance = new Entrance();
            entrance.Show();
            entrance.loginBox.Visibility = Visibility.Collapsed;
            this.Close();
        }
    }
}

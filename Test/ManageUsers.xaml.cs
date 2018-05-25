using System;
using System.Collections.Generic;
using System.Data;
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

namespace Test
{
    /// <summary>
    /// Логика взаимодействия для ManageUsers.xaml
    /// </summary>
    public partial class ManageUsers : Window
    {
        JournalDBEntities context = new JournalDBEntities();
        public ManageUsers()
        {
            
            InitializeComponent();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            userData.Columns.Clear();
            fillingTableDriver();
            userData.ItemsSource = context.Driver.ToList();
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            userData.Columns.Clear();
            fillingTableEmployees();
            userData.ItemsSource = context.Employees.ToList();
        }

        private void fillingTableDriver()
        {
            AddColumn(userData, "id_driver", "ID");
            AddColumn(userData, "name", "ФИО");
            AddColumn(userData, "mobilePhone", "Телефон");
        }

        private void AddColumn(DataGrid dataGrid,string nameColumn, string headerColumn)
        {
            Binding binding = new Binding(nameColumn);
            DataGridTextColumn column= new DataGridTextColumn()
            {
                Binding = binding,
                Header = headerColumn
            };

            dataGrid.Columns.Add(column);
        }

        private void fillingTableEmployees()
        {
            AddColumn(userData, "id", "ID");
            AddColumn(userData, "fullName", "ФИО");
            AddColumn(userData, "login", "Логин");
            AddColumn(userData, "accessRights", "Доступ");
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            addUser.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addUser.Visibility = Visibility.Visible;
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            if(selectedUsers.SelectedIndex == 0)
            {
                int user_id = (userData.SelectedItem as Driver).id_driver;
                var driver = context.Driver.Where(x => x.id_driver == user_id).First();
                context.Entry(driver).State = EntityState.Deleted;
                //context.Driver.Remove(driver);
                context.SaveChanges();
                userData.ItemsSource = context.Driver.ToList();
            }
            else
            {
                int user_id = (userData.SelectedItem as Employees).id;
                Employees employees = context.Employees.Where(x => x.id== user_id).First();
                //context.Employees.Remove(employees);
                context.Entry(employees).State = EntityState.Deleted;
                context.SaveChanges();
                userData.ItemsSource = context.Employees.ToList();
            }
            //int user_id = userData.SelectedItem is Employees ?
            //    (userData.SelectedItem as Employees).id :
            //    (userData.SelectedItem as Driver).id_driver;

        }

        private void editUser_Click(object sender, RoutedEventArgs e)
        {
            //var el = 
        }

        private void editDriver()
        {
            var data = (userData.SelectedItem as Driver).
        }
    }
}

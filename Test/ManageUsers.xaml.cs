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

        private void AddColumn(DataGrid dataGrid, string nameColumn, string headerColumn)
        {
            Binding binding = new Binding(nameColumn);
            DataGridTextColumn column = new DataGridTextColumn()
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
            AddColumn(userData, "department", "Отдел");
            AddColumn(userData, "login", "Логин");
            AddColumn(userData, "accessRights", "Доступ");
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (selectedUsers.SelectedIndex == 0)
                ComboBoxItem_Selected(null, null);
            else
                ComboBoxItem_Selected_1(null, null);

            defaultPropertyAddUser();
            addUser.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            addUser.Visibility = Visibility.Visible;
        }

        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUsers.SelectedIndex == 0)
            {
                int user_id = (userData.SelectedItem as Driver).id_driver;
                var driver = context.Driver.Where(x => x.id_driver == user_id).First();
                context.Entry(driver).State = EntityState.Deleted;
                context.SaveChanges();
                userData.ItemsSource = context.Driver.ToList();
            }
            else
            {
                int user_id = (userData.SelectedItem as Employees).id;
                Employees employees = context.Employees.Where(x => x.id == user_id).First();
                context.Entry(employees).State = EntityState.Deleted;
                context.SaveChanges();
                userData.ItemsSource = context.Employees.ToList();
            }
        }


        private string[] titleDriver = { "Добавить водителя", "Редактировать водителя" };
        private string[] titleEmployee = { "Добавить сотрудника", "Редактировать сотрудника" };


        private void editUser_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUsers.SelectedIndex == 0)
            {
                addUser.Visibility = Visibility.Visible;
                panelEmployee.Visibility = Visibility.Collapsed;
                panelDriver.SetCurrentValue(Grid.ColumnSpanProperty, 2);
                panelDriver.SetCurrentValue(Grid.ColumnProperty, 0);
                tbTitleDriver.Text = titleDriver[1];
                btAddDriver.Content = titleDriver[1].Split(' ')[0].ToString();
                

                int user_id = (userData.SelectedItem as Driver).id_driver;
                var driver = context.Driver.Where(x => x.id_driver == user_id).First();
                tbFullNameDriver.Text = driver.name;
                tbMobilePhone.Text = driver.mobilePhone;
                editUser.Tag = driver.id_driver;
            }
            else
            {
                addUser.Visibility = Visibility.Visible;
                panelDriver.Visibility = Visibility.Collapsed;
                panelEmployee.SetCurrentValue(Grid.ColumnSpanProperty, 2);
                panelEmployee.SetCurrentValue(Grid.ColumnProperty, 0);
                tbTitleEmployee.Text = titleEmployee[1];
                btAddEmployee.Content = titleEmployee[1].Split(' ')[0].ToString();

                int user_id = (userData.SelectedItem as Employees).id;
                var employee = context.Employees.Where(x => x.id == user_id).First();
                tbFullNameEmployee.Text = employee.fullName;
                tbDepartmentEmployee.Text = employee.department;
                tbLoginEmployee.Text = employee.login;
                tbPassEmployee.Text = employee.password;
                cbAcсessEmployee.Text = (employee.accessRights == true) ? "Полный" : "Не полный";
                editUser.Tag = employee.id;
            }
        }

        private void defaultPropertyAddUser()
        {
            tbTitleDriver.Text = titleDriver[0];
            btAddDriver.Content = titleDriver[0].Split(' ')[0];

            tbTitleEmployee.Text = titleEmployee[0];
            btAddEmployee.Content = titleEmployee[0].Split(' ')[0];


            panelEmployee.Visibility = Visibility.Visible;
            panelEmployee.SetCurrentValue(Grid.ColumnSpanProperty, 1);
            panelEmployee.SetCurrentValue(Grid.ColumnProperty, 0);
            panelDriver.Visibility = Visibility.Visible;
            panelDriver.SetCurrentValue(Grid.ColumnSpanProperty, 1);
            panelDriver.SetCurrentValue(Grid.ColumnProperty, 1);
            clearData();
        }

        private void clearData()
        {
            foreach (var item in panelDriver.Children)
            {
                if (item is WrapPanel)
                {
                    foreach (var i in (item as WrapPanel).Children)
                    {
                        if (i is TextBox)
                        {
                            (i as TextBox).Text = string.Empty;
                        }
                    }
                }
            }
            foreach (var item in panelEmployee.Children)
            {
                if(item is WrapPanel)
                foreach (var i in (item as WrapPanel).Children)
                {
                    if (i is TextBox)
                    {
                        (i as TextBox).Text = string.Empty;
                    }
                }
            }
        }

        private void editDriver()
        {
            var data = (userData.SelectedItem as Driver);
        }

        private void btAddDriver_Click(object sender, RoutedEventArgs e)
        {
            if (btAddDriver.Content.ToString() == "Добавить")
            {
                Driver d = new Driver
                {
                    name = tbFullNameDriver.Text,
                    mobilePhone = tbMobilePhone.Text,
                    status = false
                };
                context.Driver.Add(d);
                context.SaveChanges();
                MessageBox.Show("Запись добавлена");
            }
            else
            {
                var driver = context.Driver.Where(x => x.id_driver == (int)editUser.Tag).First();
                driver.name = tbFullNameDriver.Text;
                driver.mobilePhone = tbMobilePhone.Text;
                Canvas_MouseLeftButtonDown(null, null);
                
            }
        }

        private void btAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (btAddDriver.Content.ToString() == "Добавить") { 
                Employees employee = new Employees()
                {
                    fullName = tbFullNameEmployee.Text,
                    department = tbDepartmentEmployee.Text,
                    login = tbLoginEmployee.Text,
                    password = tbPassEmployee.Text,
                    accessRights = (cbAcсessEmployee.Text == "Полный") ? true : false
                };
            context.Employees.Add(employee);
            context.SaveChanges();
            MessageBox.Show("Запись добавлена");
            }
            else
            {
                var employee = context.Employees.Where(x => x.id == (int)editUser.Tag).First();
                employee.fullName = tbFullNameEmployee.Text;
                employee.department = tbDepartmentEmployee.Text;
                employee.login = tbLoginEmployee.Text;
                employee.password = tbPassEmployee.Text;
                employee.accessRights = (cbAcсessEmployee.Text == "Полный") ? true : false;
                Canvas_MouseLeftButtonDown(null, null);

                //Дописать изменение записи при нажатии на кнопку
            }
        }
    }
}

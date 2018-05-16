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
            //fillingTableDriver();
            InitializeComponent();
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            userData.Columns.Clear();
            fillingTableDriver();
            context.Driver.ToList();
            DataContext = context.Driver.Local;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            userData.Columns.Clear();
            fillingTableEmployees();
            context.Employees.ToList();
            DataContext = context.Employees.Local;
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

    }
}

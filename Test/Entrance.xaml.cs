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
    /// Логика взаимодействия для Entrance.xaml
    /// </summary>
    public partial class Entrance : Window
    {
        public Entrance()
        {
            InitializeComponent();
        }

        public int id_employe;
        public bool rights;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            JournalDBEntities db = new JournalDBEntities();
            bool check = false;


            foreach (var item in db.Employees)
            {
                if(item.login == tbLogin.Text)
                {
                    if(item.password == tbPass.Password)
                    {
                        Properties.Settings.Default.idUser = item.id;

                        if (item.accessRights == true)
                        {
                            Properties.Settings.Default.rights = 3;
                            Properties.Settings.Default.Save();
                        }
                        else
                        {
                            Properties.Settings.Default.rights = 2;
                            Properties.Settings.Default.Save();
                        }
                        
                        new MainWindow().Show();
                        check = true;
                        this.Close();
                        break;
                        
                    }
                }
            }
            if (check == false)
            {
                MessageBox.Show("Вы ввели неверный логин или пароль");
            }
        }
    }
}

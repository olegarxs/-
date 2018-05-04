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
            Employees em = new Employees();

            for (int i = 0; i < em// кол-во элементов; i++)
            {
                if (tbLogin.Text == em.login[i].ToString())
                {
                    if (tbPass.Text == em.password[i].ToString())
                    {
                        id_employe = em.id;
                        rights = em.accessRights;
                        MessageBox.Show("Логин и пароль верный");
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Не верный логин");
                }

            }
        }
    }
}

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
            List<Employees> em = db.Employees.ToList();
            bool check = false;

            for (int i = 0; i <  db.Employees.Count(); i++)
            {
                string login = em.Select(s => s.login).ElementAt(i).ToString();
                if (tbLogin.Text == login)
                {
                    if (tbPass.Text == em.Select(s => s.password).ElementAt(i).ToString())
                    {
                        id_employe = int.Parse(em.Select(s => s.id).ElementAt(i).ToString());
                        rights = bool.Parse(em.Select(s => s.accessRights).ElementAt(i).ToString());
                        MessageBox.Show("Логин и пароль верный");
                        check = true;
                        break;
                    }                                                           
                }
            }
            if(check == false)
            {
                MessageBox.Show("Вы ввели неверный логин или пароль");
            }
        }
    }
}

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
using System.Windows.Shapes;


namespace Test
{
    /// <summary>
    /// Логика взаимодействия для AddElement.xaml
    /// </summary>
    public partial class AddElement : Window
    {
        public AddElement()
        {
            InitializeComponent();
        }

        JournalDBEntities db = new JournalDBEntities();

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            Data d = new Data();
            d.name = tbName.Text;
            string timeNow = DateTime.Now.ToString("dd.MM.yy hh:mm");
            d.applicationDateAndTime = timeNow + "," + tbCar.Text;
            d.dateAndTimeOfCarProvision = dtpick.Text + " " + tbHours.Text + ":" + tbMinuts.Text;
            d.purposesOfUsingAuto = cbPurposesOfUsingAuto.Text;
            d.cargo = tbCargo.Text;
            d.route = tbRoute.Text;
            d.nameDocument = tbNameDocument.Text;
            d.id_employe = Properties.Settings.Default.idUser;
            d.nameDocument = "Нет";
            d.applicationStatus = "Не выполненно";
            db.Data.Add(d);
            db.SaveChanges();
            this.Close();
        }

        private void tbCar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)cb.SelectedItem;
            if (selectedItem.Content.ToString() == "Грузовая")
            {
                hide.Visibility = Visibility.Visible;
                tbNameDocument.Text = "";
            }
            else {
                hide.Visibility = Visibility.Hidden;
                tbNameDocument.Text = "Нет";
            }
        }
    
    }
}

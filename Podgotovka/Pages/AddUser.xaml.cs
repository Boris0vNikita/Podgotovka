using Podgotovka.Infructucture;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Podgotovka.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Page
    {
        public AddUser()
        {
            InitializeComponent();
            cmbRole.ItemsSource = DbConn.db.Role.ToList();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/ShowUser.xaml", UriKind.RelativeOrAbsolute));
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (txbFirstName.Text != "" && txbLastName.Text != "" && txbLogin.Text != "" && txbPassword.Text != "" && txbPatronymic.Text != "" && txbPhone.Text != "")
            {
                try
                {
                   
                    if (txbPhone.Text.Length == 11)
                    {
                        int idPeople = DbConn.db.People.Max(p => p.IdPeople) + 1;
                        int idRole = (cmbRole.SelectedItem as Role).IdRole;
                        People people = new People();
                        people.IdPeople = idPeople;
                        people.FirstName = txbFirstName.Text;
                        people.LastName = txbLastName.Text;
                        people.Login = txbLogin.Text;
                        people.Password = txbPassword.Text;
                        people.Patronymic = txbPatronymic.Text;
                        people.Phone = txbPhone.Text;
                        people.RoleId = idRole;
                        DbConn.db.People.Add(people);
                        DbConn.db.SaveChanges();
                        MessageBox.Show("Пользователь успешно добавлен");
                    }
                    else
                    {
                        MessageBox.Show("Телефон состоит из 11 цифр");
                    }
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Не все поля заполненны!");
            }
        }

        private void txbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !(Char.IsDigit(e.Text, 0));
        }
    }
}

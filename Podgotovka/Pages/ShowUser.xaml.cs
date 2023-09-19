using Podgotovka.Infructucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
    /// Логика взаимодействия для ShowUser.xaml
    /// </summary>
    public partial class ShowUser : Page
    {
        public ShowUser()
        {
            InitializeComponent();
            dgrUser.ItemsSource = DbConn.db.People.ToList();
            cmbRole.ItemsSource = DbConn.db.Role.ToList();
            
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/AddUser.xaml", UriKind.RelativeOrAbsolute));
        }

        private void txbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SelectedUser();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedUser();
        }
        private void SelectedUser()
        {
            List<People> people = DbConn.db.People.ToList();

            if (cmbRole.SelectedItem == null && txbSearch.Text != "")
            {
                string text = txbSearch.Text;
                dgrUser.ItemsSource= people.Where(p => p.Fio.Contains(text));
            }
            if (cmbRole.SelectedItem != null && txbSearch.Text == "")
            {
                int idRole = (cmbRole.SelectedItem as Role).IdRole;
                dgrUser.ItemsSource = people.Where(p => p.RoleId == idRole);
            }
            if (cmbRole.SelectedItem == null && txbSearch.Text == "")
            {

                dgrUser.ItemsSource = people;
            }
            if (cmbRole.SelectedItem != null && txbSearch.Text != "")
            {
                int idRole = (cmbRole.SelectedItem as Role).IdRole;
                string text = txbSearch.Text;
                dgrUser.ItemsSource = people.Where(p => p.RoleId == idRole && p.Fio.Contains(text));
               
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            dgrUser.ItemsSource = DbConn.db.People.ToList();
            cmbRole.SelectedItem = null;
            txbSearch.Text = "";
        }

        private void btnSound_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
           
        }
    }
}

using Podgotovka.Infructucture;
using Podgotovka.Properties;
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
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Page
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txbLogin.Text != "" && psbPas.Password != "")
                {
                    People people = DbConn.db.People.Where(p => p.Login == txbLogin.Text && p.Password == psbPas.Password).FirstOrDefault();
                    if (people != null)
                    {
                        if (chSavePas.IsChecked == true)
                        {
                            Settings.Default["User"]=people.IdPeople;
                            Settings.Default.Save();
                        }
                        else
                        {
                            Settings.Default["User"] = 0;
                            Settings.Default.Save();
                        }
                        switch (people.RoleId)
                        {
                            case 1:
                                {
                                    App.Current.Resources["IdUser"] = people.IdPeople;
                                    NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
                                    break;
                                }
                            case 2:
                                {
                                    App.Current.Resources["IdUser"] = people.IdPeople;
                                    NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/UserPage.xaml", UriKind.RelativeOrAbsolute));
                                    break;
                                }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void chPas_Click(object sender, RoutedEventArgs e)
        {
            if (chPas.IsChecked == true)
            {
                psbPas.Visibility = Visibility.Hidden;
                txbPas.Visibility = Visibility.Visible;
                txbPas.Text = psbPas.Password;
            }
            else
            {
                psbPas.Visibility = Visibility.Visible;
                txbPas.Visibility = Visibility.Hidden;
                psbPas.Password = txbPas.Text;
            }
           
        }

        

        private void txbPas_TextChanged(object sender, TextChangedEventArgs e)
        {
            psbPas.Password = txbPas.Text;
        }
    }
}

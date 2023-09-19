using Podgotovka.Infructucture;
using Podgotovka.Pages;
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

namespace Podgotovka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            frmNavigate.Content = new Autorization();

            if ((int)Settings.Default["User"] != 0)
            {
                try
                {
                    People people = DbConn.db.People.Where(p => p.IdPeople == (int)Settings.Default["User"]).FirstOrDefault();
                    switch (people.RoleId)
                    {
                        case 1:
                            {
                                NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/AdminPage.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                        case 2:
                            {
                                NavigationService.GetNavigationService(this).Navigate(new Uri("/Pages/UserPage.xaml", UriKind.RelativeOrAbsolute));
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {

            }
        }
    }
}

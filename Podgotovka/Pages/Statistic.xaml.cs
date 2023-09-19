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
    /// Логика взаимодействия для Statistic.xaml
    /// </summary>
    public partial class Statistic : Page
    {
        List<Order> order = DbConn.db.Order.ToList();
        List<ProductOrder> productOrders = DbConn.db.ProductOrder.ToList();
        public Statistic()
        {
            InitializeComponent();
            grdUsers.ItemsSource = DbConn.db.People.ToList();
            List<Role> roles = DbConn.db.Role.ToList();
            List<string> role = new List<string>();
            role.Add("Все");
            foreach (var pr in roles)
            {
                role.Add(pr.NameRole);
            }
            cmbRole.ItemsSource = role;
           
            cmbMonth.Items.Add("Январь");
            cmbMonth.Items.Add("Февраль");
            cmbMonth.Items.Add("Март");
            cmbMonth.Items.Add("Апрель");
            cmbMonth.Items.Add("Май");
            cmbMonth.Items.Add("Июнь");
            cmbMonth.Items.Add("Июль");
            cmbMonth.Items.Add("Август");
            cmbMonth.Items.Add("Сентябрь");
            cmbMonth.Items.Add("Октябрь");
            cmbMonth.Items.Add("Ноябрь");
            cmbMonth.Items.Add("Декабрь");

            int quantityOrder = DbConn.db.Order.Count();
            lblQuantityOrders.Content = quantityOrder.ToString();

            int quantityProduct = 0;
            decimal Price = 0;
            decimal allPrice = 0;
            foreach (var productOrder in productOrders)
            {
                int quantity = productOrder.Quantity;
                quantityProduct += productOrder.Quantity;
                 Price = DbConn.db.Product.Where(p => p.IdProduct == productOrder.ProductId).FirstOrDefault().Price;
                 allPrice += quantity * Price;
            }
            lblAllProducts.Content = quantityOrder.ToString();
            lblExpenses.Content = allPrice.ToString();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                if (cmbRole.SelectedItem.ToString() == "Все")
                {
                    grdUsers.ItemsSource = DbConn.db.People.ToList();
                }
                if (cmbRole.SelectedItem.ToString() == "Админ")
                {
                    grdUsers.ItemsSource = DbConn.db.People.Where(p=>p.Role.NameRole=="Админ").ToList();
                }
                if (cmbRole.SelectedItem.ToString() == "Пользователь")
                {
                    grdUsers.ItemsSource = DbConn.db.People.Where(p => p.Role.NameRole == "Пользователь").ToList();
                }
            
        }

        private void cmbMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectMonth = cmbMonth.SelectedIndex + 1;
            int quantityOrders = 0;
            int quantityProducts = 0;
            decimal productPrice = 0;
            decimal price = 0;
            int quantity = 0;
            foreach(var orders in order)
            {
                if(selectMonth == Int32.Parse(orders.Date.ToString("MM")))
                {
                    quantityProducts++;
                    List<ProductOrder> productOrders = DbConn.db.ProductOrder.Where(p => p.OrderId == orders.IdOrder).ToList();
                    foreach (var ord in productOrders)
                    {
                        quantityProducts += ord.Quantity;
                        price = DbConn.db.Product.Where(s => s.IdProduct == ord.ProductId).FirstOrDefault().Price;
                        quantity = ord.Quantity;
                        productPrice += price * quantity;
                    }
                }
            }
            lblAllProducts.Content = quantityProducts;
            lblExpenses.Content = productPrice;
            lblQuantityOrders.Content = quantityProducts;
        }
    }
}

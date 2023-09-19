using Podgotovka.Infructucture;
using Podgotovka.Models;
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
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        List<ProductOut> productOuts = new List<ProductOut>();
        int id = 0;

        public AddOrder()
        {
            InitializeComponent();
            cmbProduct.ItemsSource = DbConn.db.Product.ToList();
            dgrOrder.ItemsSource = productOuts;
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (cmbProduct.SelectedItem != null)
            {
                int idProduct = (cmbProduct.SelectedItem as Product).IdProduct;
                string nameProduct= (cmbProduct.SelectedItem as Product).NameProduct;
                decimal price = (cmbProduct.SelectedItem as Product).Price;
                ProductOut product = productOuts.Where(s => s.NameProduct == nameProduct).FirstOrDefault();
                if(product != null)
                {
                    product.Quantity++;
                }
                else
                {
                    productOuts.Add(new ProductOut() { Id = idProduct, NameProduct = nameProduct, Price = price, Quantity = 1 });
                }
                dgrOrder.ItemsSource = null;
                dgrOrder.ItemsSource = productOuts;
            }
            else
            {
                MessageBox.Show("Товар не выбран");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (dgrOrder.SelectedItem != null)
            {
                ProductOut productOut = (dgrOrder.SelectedItem as ProductOut);
                productOut.Quantity++;
                dgrOrder.ItemsSource = null;
                dgrOrder.ItemsSource = productOuts;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (dgrOrder.SelectedItem != null)
            {
                ProductOut productOut = (dgrOrder.SelectedItem as ProductOut);
                if (productOut.Quantity > 1)
                {
                    productOut.Quantity--;
                }
                else
                {
                    productOuts.Remove(productOut);
                }
                dgrOrder.ItemsSource = null;
                dgrOrder.ItemsSource = productOuts;
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dgrOrder.SelectedItem != null)
            {
                ProductOut productOut=(dgrOrder.SelectedItem as ProductOut);
                productOuts.Remove(productOut);
                dgrOrder.ItemsSource = null;
                dgrOrder.ItemsSource = productOuts;
            }
            else
            {
                MessageBox.Show("Товар не выбран");
            }
        }

        private void btnAllCost_Click(object sender, RoutedEventArgs e)
        {
            if (dgrOrder.Items != null)
            {
                decimal allPrice = 0;
                foreach (ProductOut productOut in productOuts)
                {
                    decimal price = DbConn.db.Product.Where(p => p.NameProduct == productOut.NameProduct).FirstOrDefault().Price;
                    int quantity = productOut.Quantity;
                    allPrice += price * quantity;
                   
                }
                txbAllCost.Text = allPrice.ToString();
            }
            else
            {
                MessageBox.Show("Нет товаров для рассчета итоговой суммы заказа");
            }
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dgrOrder.Items != null)
            {
                int idOrder = DbConn.db.Order.Max(p => p.IdOrder) + 1;

                Order order = new Order();
                order.IdOrder = idOrder;
                order.PeopleId = (int)App.Current.Resources["IdUser"];
                order.StatusId = 1;
                order.Date = DateTime.Now;
                DbConn.db.Order.Add(order);
                DbConn.db.SaveChanges();
                foreach(ProductOut newproductOut in productOuts)
                {
                    decimal priceProduct = DbConn.db.Product.Where(p => p.NameProduct == newproductOut.NameProduct).FirstOrDefault().Price;

                    ProductOrder newproductOrder = new ProductOrder();
                    newproductOrder.IdProductOrder = DbConn.db.ProductOrder.Max(p=>p.IdProductOrder)+1;
                    newproductOrder.OrderId = order.IdOrder;
                    newproductOrder.Quantity = newproductOut.Quantity;
                    newproductOrder.ProductId = DbConn.db.Product.Where(p => p.NameProduct == newproductOut.NameProduct).FirstOrDefault().IdProduct;
                    try
                    {
                        DbConn.db.ProductOrder.Add(newproductOrder);
                        DbConn.db.SaveChanges();
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
                MessageBox.Show("заказ сформирован");
            }
            else
            {
                MessageBox.Show("Нет выбраных товаров для формирования заказа");
            }
        }
    }
}

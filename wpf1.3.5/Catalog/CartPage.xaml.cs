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
using System.Data;
using wpf1._3._5.Models;

namespace wpf1._3._5.Catalog
{
    /// <summary>
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Window
    {
        Cart Cart = new Cart();
        public CartPage()
        {
            InitializeComponent();

            DataSet Products = this.Cart.AllAlt();

            //CartTable.ItemsSource = Products.Tables["Cart"].AsEnumerable();
            CartTable.ItemsSource = Products.Tables[0].DefaultView;
        }
    }
}

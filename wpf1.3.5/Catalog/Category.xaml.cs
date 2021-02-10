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
using System.Windows.Media.Effects;
using wpf1._3._5.Models;
using System.Data;
using System.Windows.Controls.Primitives;

namespace wpf1._3._5.Catalog
{
    /// <summary>
    /// Логика взаимодействия для Category.xaml
    /// </summary>
    public partial class Category : Page
    {
        CarModel CarModel;
        public Category(int id)
        {
            InitializeComponent();
            this.CarModel = new CarModel();

            DataSet dillers = CarModel.GetAllByDillerId(id);
            foreach (DataRow model in dillers.Tables[0].Rows)
            {
                Card Card = new Card();
                Card.AddName((string)model["name"]);
                Card.AddPrice(Convert.ToString(model["price"]));
                Card.AddImage((string)model["image"]);
                Card.AddBtn(Convert.ToInt32(model["Id"]));

                Card.MakeCard(Root);
            }
        }

        public void OpenCart(object sender, RoutedEventArgs e)
        {
            CartPage modelsEditor = new CartPage();
            modelsEditor.Show();
        }
    }

    class Card
    {
        Image Image = new Image();
        Grid CardWrapper = new Grid();
        StackPanel ControlsWrapper = new StackPanel();
        TextBlock Name = new TextBlock();
        TextBlock Price = new TextBlock();
        Button BuyBtn = new Button();

        Cart Cart = new Cart();
        public Card()
        {
            this.CardWrapper.Margin = new Thickness(20, 20, 20, 20);//?
            this.CardWrapper.VerticalAlignment = VerticalAlignment.Top;
            this.CardWrapper.Background = Brushes.White;
            this.CardWrapper.Height = 250;
            this.CardWrapper.Width = 300;

            DropShadowEffect WrapperShadow = new DropShadowEffect();
            WrapperShadow.BlurRadius = 20;
            WrapperShadow.ShadowDepth = 1;

            this.CardWrapper.Effect = WrapperShadow;

            this.ControlsWrapper.Margin = new Thickness(30, 10, 0, 0);//?
            this.ControlsWrapper.HorizontalAlignment = HorizontalAlignment.Left;
            this.ControlsWrapper.Width = 265;


        }

        public Card AddName(string name)
        {
            Name.Text = name;
            Name.FontSize = 22;
            Name.TextWrapping = TextWrapping.Wrap;
            Name.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6A6A6A"));

            return this;
        }

        public Card AddPrice(string price)
        {
            Price.Text = $"$ {price}";
            Price.FontSize = 20;
            Price.Margin = new Thickness(180, 20, 0, 0);
            Price.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));

            return this;
        }

        public Card AddBtn(int id)
        {
            BuyBtn.FontSize = 22;
            BuyBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4572A0"));
            BuyBtn.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF4572A0"));
            BuyBtn.Foreground = Brushes.White;
            BuyBtn.Content = "Buy Now";
            BuyBtn.Margin = new Thickness(10, 110, 0, 0);
            BuyBtn.Tag = id;
            BuyBtn.Click += new RoutedEventHandler(this.Buy);

            return this;
        }

        public void Buy(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            bool result = this.Cart.Add(Convert.ToInt32(btn.Tag));

            Console.WriteLine("IsAdded: " + result);
        }

        public Card AddImage(string path)
        {
            this.Image.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            this.Image.HorizontalAlignment = HorizontalAlignment.Left;
            this.Image.Margin = new Thickness(10, 0, 0, 10);//?
            this.Image.Width = 200;
            this.Image.Height = 200;

            return this;
        }

        public void MakeCard(WrapPanel element)
        {
            ControlsWrapper.Children.Add(Name);
            ControlsWrapper.Children.Add(Price);
            ControlsWrapper.Children.Add(BuyBtn);

            CardWrapper.Children.Add(Image);
            CardWrapper.Children.Add(ControlsWrapper);

            element.Children.Add(CardWrapper);
        }
    }
}

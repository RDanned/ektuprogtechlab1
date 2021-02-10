using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
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
using wpf1._3._5.Admin;
using wpf1._3._5.Catalog;
using wpf1._3._5.Models;

namespace wpf1._3._5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            DataSet dillers = this.GetDillers();
            string test;

            int MarginLeft = 0;

            foreach (DataRow row in dillers.Tables[0].Rows)
            {
                MarginLeft += 100;
                Button btn = new Button();
                btn.Content = (string)row["name"];
                btn.Tag = row["Id"];
                //btn.Margin = new Thickness(MarginLeft, 20, 20, 20);
                btn.Width = 100;
                btn.Height = 30;
                btn.VerticalAlignment = VerticalAlignment.Top;
                btn.Click += new RoutedEventHandler(this.OpenCategory);
                Navigation.Children.Add(btn);
                test = (string)row["name"];
                Console.WriteLine(test);
            }

        }

        private DataSet GetDillers()
        {
            CarDiller diller = new CarDiller();

            DataSet dillers = diller.AllAlt();

            return dillers;
        }

        private void EditorBtn(object sender, RoutedEventArgs e)
        {
            ModelsEditor modelsEditor = new ModelsEditor();
            modelsEditor.Show();
        }

        private void OpenCategory(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            //TestFrame.NavigationService.RemoveBackEntry();
            Category Category = new Category(Convert.ToInt32(clickedButton.Tag));
            TestFrame.Content = Category;
        }

        private void OpenCategories(object sender, RoutedEventArgs e)
        {
            //Categories.Show();
            TestFrame.Content = null;
            Category category = new Category(1);
            //NavigationWindow win = (NavigationWindow)Window.GetWindow(this);
            /*NavigationWindow win = new NavigationWindow();
            win.Content = categories;*/
            TestFrame.Content = category;
            //win.Show();
            //this.Content = categories;
            //MainWindow.Navigate(categories);
        }
    }
}

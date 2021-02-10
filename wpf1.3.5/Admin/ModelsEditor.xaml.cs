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
using Microsoft.Win32;
using wpf1._3._5.Models;
using System.Runtime.InteropServices;

namespace wpf1._3._5.Admin
{
    /// <summary>
    /// Логика взаимодействия для ModelsEditor.xaml
    /// </summary>
    public partial class ModelsEditor : Window
    {
        CarModel CarModel;
        CarDiller CarDiller;
        string ModelName;
        string ModelImagePath;
        int ModelSelectedDillerId;

        /*[DllImport("Kernel32")]
        public static extern void AllocConsole();*/
        public ModelsEditor()
        {
            //AllocConsole();
            InitializeComponent();

            this.CarModel = new CarModel();
            this.CarDiller = new CarDiller();

            List<CarItem> AllCars = this.CarModel.All();
            List<CarDillerItem> AllDillers = this.CarDiller.All();

            foreach(CarDillerItem Diller in AllDillers)
            {
                ComboBoxItem item = new ComboBoxItem();

                item.Tag = Diller.Id;
                item.Content = Diller.Name;
                ModelDillerList.Items.Add(item);
            }

            Console.WriteLine("Id    Name    DillerId     DillerName");
            foreach (CarItem Car in AllCars)
            {
                Console.WriteLine($"{Car.Id}    {Car.Name}    {Car.DillerId}     {Car.DillerName}");
            }
        }

        private void UploadImage(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == true)
            {
                Image modelPreview = new Image();

                BitmapImage previewSource = new BitmapImage();
                previewSource.BeginInit();
                this.ModelImagePath = fileDialog.FileName;
                previewSource.UriSource = new Uri(this.ModelImagePath);
                previewSource.EndInit();

                ModelPreview.Source = previewSource;
            }
        }

        private void DillersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(ComboBoxItem diller in e.AddedItems)
            {
                this.ModelSelectedDillerId = Convert.ToInt32(diller.Tag);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = ModelNameInput.Text;
            string ImagePath = this.ModelImagePath;
            int DillerId = this.ModelSelectedDillerId;
            int Price = Convert.ToInt32(this.ModelPriceInput.Text);

            CarModel Model = new CarModel();

            Model.Name = Name;
            Model.ImagePath = ImagePath;
            Model.DillerId = DillerId;
            Model.Price = Price;

            bool isAdded = Model.Save();

            if (isAdded)
            {
                MessageBox.Show("Модель записана", "Редактор моделей");
                ModelNameInput.Text = "";
                ModelPriceInput.Text = "";
            } else
            {
                MessageBox.Show("Модель НЕ записана", "Редактор моделей");
            }

        }
    }
}

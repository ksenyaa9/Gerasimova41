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

namespace Gerasimova41
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {

        private int _totalPages; // Общее количество страниц
        private int _filteredPages; // Количество страниц после фильтрации
        public ProductPage()
        {
            InitializeComponent();
            var currentServices = Герасимова41Entities.GetContext().Product.ToList();
            ServiceListView.ItemsSource = currentServices;

            ComboType.SelectedIndex = 0;
            UpdateProduct();
        }

        private void UpdateProduct()
        {
            var currentProduct = Герасимова41Entities.GetContext().Product.ToList();

            // Общее количество страниц
            _totalPages = currentProduct.Count;
            CountPage.Text = $"количество {_totalPages} из {_totalPages}";

            if (ComboType.SelectedIndex == 0) {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 0 && p.ProductDiscountAmount < 10)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 10 && p.ProductDiscountAmount < 15)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProduct = currentProduct.Where(p => (p.ProductDiscountAmount >= 15 && p.ProductDiscountAmount <= 100)).ToList();
            }





            currentProduct = currentProduct.Where(p => p.ProductName.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            ServiceListView.ItemsSource = currentProduct.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentProduct.OrderByDescending(p => p.ProductCost).ToList();

            }
            if (RButtonUP.IsChecked.Value)
            {
                ServiceListView.ItemsSource = currentProduct.OrderBy(p => p.ProductCost).ToList();

            }

            _filteredPages = currentProduct.Count;
            CountPage.Text = $"количество {_filteredPages} из {_totalPages}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonUP_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateProduct();
        }
    }
}

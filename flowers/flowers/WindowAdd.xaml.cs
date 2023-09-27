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

namespace flowers
{
    /// <summary>
    /// Логика взаимодействия для WindowAdd.xaml
    /// </summary>
    public partial class WindowAdd : Window
    {
        private Product _product = new Product();
        public WindowAdd(Product selectedProduct)
        {
            InitializeComponent();
            var categ = fahrutdinova_impEntitiesFlowers.GetContext().Product.Select(p => p.ProductCategory).ToList();
            cbCategory.ItemsSource = categ.Distinct();
            var manuf = fahrutdinova_impEntitiesFlowers.GetContext().Product.Select(p => p.ProductManufacturer).ToList();
            cbManuf.ItemsSource = manuf.Distinct();
            var provider = fahrutdinova_impEntitiesFlowers.GetContext().Product.Select(p => p.ProductPrivider).ToList();
            cbProvider.ItemsSource = provider.Distinct();

            

            if(selectedProduct!=null)
            {
                _product = selectedProduct;
            }
            DataContext = _product;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //validation
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_product.ProductArticleNumber))
            {
                error.AppendLine("Укажите ариткул продукта");
            }
            if (!_product.ProductCost.ToString().All(char.IsDigit))
            {
                error.AppendLine("Цена должна быть числом");
            }
            if(error.Length>0)
            {
                MessageBox.Show(error.ToString());
                return;
            }


            if (!fahrutdinova_impEntitiesFlowers.GetContext().Product.Select(p => p.ProductArticleNumber).Contains(_product.ProductArticleNumber))
            {
                fahrutdinova_impEntitiesFlowers.GetContext().Product.Add(_product);
            }
            try
            {
                fahrutdinova_impEntitiesFlowers.GetContext().SaveChanges();
                MessageBox.Show("Updated");
            }
            catch
            {
                MessageBox.Show("Smth went wrong");
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }
    }
}

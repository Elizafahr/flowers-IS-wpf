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

namespace flowers
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Заполнение комбоБокс из базы
            var manuf = fahrutdinova_impEntitiesFlowers.GetContext().Product.Select(p => p.ProductManufacturer).ToList();
            manuf.Insert(0, "Все производители");

            cmbFilter.ItemsSource = manuf.Distinct();
        }

        public void Update() {
            var query = fahrutdinova_impEntitiesFlowers.GetContext().Product.ToList(); 
            //поиск в реальном времени
            query = query.Where(p => p.ProductName.ToLower().Contains(tbPoisk.Text.ToLower())
            || p.ProductArticleNumber.ToLower().Contains(tbPoisk.Text.ToLower())).ToList();

            //filtration
            if (cmbFilter.SelectedIndex > 0)
            {
                query = query.Where(p => p.ProductManufacturer.Contains(cmbFilter.SelectedItem.ToString())).ToList(); 
            }
            //Сортировка
            //switch(cmbSort.SelectedIndex)
            //{
            //    case 0:
            //        dgProduct.ItemsSource = query.OrderBy(p => p.ProductCost);
            //        break;
            //    case 1:
            //        dgProduct.ItemsSource = query.OrderByDescending(p => p.ProductCost);
            //        break;
            //    default:            
            //        dgProduct.ItemsSource = query;
            //        break;
            //}

            //Сортировка по radiobutton
            if (rbPrice.IsChecked == true)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        dgProduct.ItemsSource = query.OrderBy(p => p.ProductCost);break;
                    case 1:
                        dgProduct.ItemsSource = query.OrderByDescending(p => p.ProductCost);break;
                    default:
                        dgProduct.ItemsSource = query;break;
                }
            }
            else if (rbName.IsChecked == true)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        dgProduct.ItemsSource = query.OrderBy(p => p.ProductName);break;
                    case 1:
                        dgProduct.ItemsSource = query.OrderByDescending(p => p.ProductName);break;
                    default:
                        dgProduct.ItemsSource = query;break;
                }
            }
            else if(rbArticul.IsChecked == true)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        dgProduct.ItemsSource = query.OrderBy(p => p.ProductArticleNumber);break;
                    case 1:
                        dgProduct.ItemsSource = query.OrderByDescending(p => p.ProductArticleNumber);break;
                    default:
                        dgProduct.ItemsSource = query;break;
                }
            }
            else if (rbManufacturer.IsChecked == true)
            {
                switch (cmbSort.SelectedIndex)
                {
                    case 0:
                        dgProduct.ItemsSource = query.OrderBy(p => p.ProductManufacturer);break;
                    case 1:
                        dgProduct.ItemsSource = query.OrderByDescending(p => p.ProductManufacturer);break;
                    default:
                        dgProduct.ItemsSource = query;break;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void tbPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void cmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void rbPrice_Checked(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd win = new WindowAdd(null);
            win.ShowDialog();
        
            
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Update();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd win = new WindowAdd(dgProduct.SelectedItem as Product);
            win.ShowDialog();

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            //form the collection of active lines
            var productDelited = dgProduct.SelectedItems.Cast<Product>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалит следующие {productDelited.Count()} элементов?", "Внимание!!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
               try
                {
                    fahrutdinova_impEntitiesFlowers.GetContext().Product.RemoveRange(productDelited);
                    fahrutdinova_impEntitiesFlowers.GetContext().SaveChanges();
                    MessageBox.Show("Строчки удалены");
                }
                catch
                {
                    MessageBox.Show("Ошибка уаления данных");
                }
            }
        }

        private void btnAdd_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPoint_Click(object sender, RoutedEventArgs e)
        {
            Пункты_выдачи win = new Пункты_выдачи();
            win.ShowDialog();
        }
    }
}

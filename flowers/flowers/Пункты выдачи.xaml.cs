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
    /// Логика взаимодействия для Пункты_выдачи.xaml
    /// </summary>
    public partial class Пункты_выдачи : Window
    {
        public Пункты_выдачи()
        {
            InitializeComponent();
            Update();
        }
        public void Update()
        {
            var query = fahrutdinova_impEntitiesFlowers.GetContext().Point.ToList();

            dgPoint.ItemsSource = query;
        }

            private void dgProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PointAdd win = new PointAdd(null);
            win.ShowDialog();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            PointAdd win = new PointAdd(dgPoint.SelectedItem as Point);
            win.ShowDialog();
        }
    }
}

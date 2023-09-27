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
    /// Логика взаимодействия для PointAdd.xaml
    /// </summary>
    public partial class PointAdd : Window
    {
        private Point _point = new Point();
        public PointAdd(Point selectedpoint)
        {
            InitializeComponent();
            DataContext = _point;
            if (selectedpoint != null)
            {
                _point = selectedpoint;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //validation
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_point.adress))
            {
                error.AppendLine("Укажите адрес продукта");
            }
            
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }


            if (!fahrutdinova_impEntitiesFlowers.GetContext().Point.Select(p => p.adress).Contains(_point.adress))
            {
                fahrutdinova_impEntitiesFlowers.GetContext().Point.Add(_point);
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
    }
}

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

namespace Test_pay
{
    /// <summary>
    /// Логика взаимодействия для AcoountAdd.xaml
    /// </summary>
    public partial class AcoountAdd : Window
    {
        public AcoountAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            date_order.SelectedDate = DateTime.Now;
            Random rnd = new Random();
            price_order.Text = rnd.Next(10, 10000).ToString();
            payment_order.Text = price_order.Text;


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            bool price = decimal.TryParse(price_order.Text.Trim(), out decimal d);
            bool payment = decimal.TryParse(payment_order.Text.Trim(), out decimal p);
            if (price && payment && d <= p)
            {
                Test_pay.test_payDataSet test_payDataSet = ((Test_pay.test_payDataSet)(this.FindResource("test_payDataSet")));
                Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter test_payDataSetAccountMoneyTableAdapter = new Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter();
                test_payDataSetAccountMoneyTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
                test_payDataSetAccountMoneyTableAdapter.Insert(date_order.SelectedDate.Value, d, p);
                this.Close();
            }
            else
            {
                if (!price)
                {
                    MessageBox.Show("Не коректное число: " + price_order.Text);
                }
                else if (!payment)
                {
                    MessageBox.Show("Не коректное число: " + payment_order.Text);
                }
                else
                {
                    MessageBox.Show("Остаток не может быть больше суммы");
                }
            }
        }
    }
}

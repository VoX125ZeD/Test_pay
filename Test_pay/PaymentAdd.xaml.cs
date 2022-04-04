using System;
using System.Data;
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
    /// Логика взаимодействия для PaymentAdd.xaml
    /// </summary>
    public partial class PaymentAdd : Window
    {
        public PaymentAdd()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Test_pay.test_payDataSet test_payDataSet = ((Test_pay.test_payDataSet)(this.FindResource("test_payDataSet")));          
            Test_pay.test_payDataSetTableAdapters.OrderTableAdapter test_payDataSetOrderTableAdapter = new Test_pay.test_payDataSetTableAdapters.OrderTableAdapter();
            test_payDataSetOrderTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
            test_payDataSetOrderTableAdapter.FillBy(test_payDataSet.Order);
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter test_payDataSetAccountMoneyTableAdapter = new Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter();
            test_payDataSetAccountMoneyTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
            test_payDataSetAccountMoneyTableAdapter.FillBy(test_payDataSet.AccountMoney);
            System.Windows.Data.CollectionViewSource accountMoneyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accountMoneyViewSource")));


            // Загрузить данные в таблицу Payment. Можно изменить этот код как требуется.
           
        }
        int id_order=-1;
        Decimal payment=-1;
        Decimal remainder = -1;
        int id_acc = -1;
        
        private void orderDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rows = e.AddedItems[0] as DataRowView;
            id_order = (int)rows.Row.ItemArray[0];
            payment = (decimal)rows.Row.ItemArray[2]-(decimal)rows.Row.ItemArray[3];
            if (payment != -1 && remainder != -1)
            {
                if (payment - remainder >= 0)
                {
                    Payment_label.Content = remainder.ToString();
                    Remainder_label.Content = 0 ;
                }
                else
                {
                    Remainder_label.Content = Math.Abs(payment - remainder);
                    Payment_label.Content = payment;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void accountMoneyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView rows = e.AddedItems[0] as DataRowView;
            id_acc = (int)rows.Row.ItemArray[0];
            remainder = (decimal)rows.Row.ItemArray[3];
            if (payment != -1 && remainder != -1)
            {
                if (payment - remainder >= 0)
                {
                    Payment_label.Content = remainder.ToString();
                    Remainder_label.Content = 0;
                }
                else
                {
                    Remainder_label.Content = Math.Abs(payment - remainder);
                    Payment_label.Content = payment;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (id_order != -1 && id_acc != -1)
            {
                Test_pay.test_payDataSet test_payDataSet = ((Test_pay.test_payDataSet)(this.FindResource("test_payDataSet")));
                Test_pay.test_payDataSetTableAdapters.PaymentTableAdapter test_payDataSetPaymentTableAdapter = new Test_pay.test_payDataSetTableAdapters.PaymentTableAdapter();
                test_payDataSetPaymentTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
                test_payDataSetPaymentTableAdapter.Insert(id_order, id_acc, 0);
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите строку из таблицы");
            }
        }
    }
}

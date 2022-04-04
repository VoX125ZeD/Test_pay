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
using System.Data.SqlClient;

namespace Test_pay
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            type_server.Items.Add("Проверка подлинности Windows");
            type_server.Items.Add("Проверка подлинности SQL Server");
            type_server.SelectedIndex = 0;
            autho.Visibility = Visibility.Visible;
            menu.Visibility = Visibility.Hidden;
        }
      

        private void combo_box1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type_server.SelectedIndex == 1)
            {
                GridLength length = new GridLength(8, GridUnitType.Star);             
                row1.Height = length;
                row2.Height = length;
            }
            else
            {
                GridLength length = new GridLength(0, GridUnitType.Pixel);
                row1.Height = length;
                row2.Height = length;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool name = name_server.Text.Trim().Length != 0;
            bool login = login_server.Text.Trim().Length != 0;
            bool password = Password_server.Text.Trim().Length != 0;
            if (type_server.SelectedIndex == 0&& name)
            {
                try
                {
                    SqlConnectionStringBuilder connect = new SqlConnectionStringBuilder();
                    connect.DataSource = name_server.Text.Trim();
                    connect.InitialCatalog = @"test_pay";
                    connect.IntegratedSecurity = true;
                    sql.sqlconnect = new SqlConnection(connect.ConnectionString);
                    sql.CreateCommand("USE test_pay", connect.ConnectionString);
                    autho.Visibility = Visibility.Hidden;
                    menu.Visibility = Visibility.Visible;
                }
                catch(Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
            else if(name&& login&& password)
            {
                try
                {
                    SqlConnectionStringBuilder connect = new SqlConnectionStringBuilder();
                    connect.DataSource = name_server.Text.Trim();
                    connect.InitialCatalog = @"test_pay";
                    connect.IntegratedSecurity = true;
                    connect.Password = Password_server.Text.Trim();
                    connect.UserID = login_server.Text.Trim();
                    sql.sqlconnect = new SqlConnection(connect.ConnectionString);
                    sql.CreateCommand("USE test_pay", connect.ConnectionString);
                    autho.Visibility = Visibility.Hidden;
                    menu.Visibility = Visibility.Visible;
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window OrderAdd = new OrderAdd();
            OrderAdd.ShowDialog();
            menu.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }

        private void menu_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (menu.Visibility == Visibility.Visible)
            {
                Test_pay.test_payDataSet test_payDataSet = ((Test_pay.test_payDataSet)(this.FindResource("test_payDataSet")));
                Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter test_payDataSetAccountMoneyTableAdapter = new Test_pay.test_payDataSetTableAdapters.AccountMoneyTableAdapter();
                test_payDataSetAccountMoneyTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
                test_payDataSetAccountMoneyTableAdapter.Fill(test_payDataSet.AccountMoney);
                System.Windows.Data.CollectionViewSource accountMoneyViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("accountMoneyViewSource")));             
                Test_pay.test_payDataSetTableAdapters.OrderTableAdapter test_payDataSetOrderTableAdapter = new Test_pay.test_payDataSetTableAdapters.OrderTableAdapter();
                test_payDataSetOrderTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
                test_payDataSetOrderTableAdapter.Fill(test_payDataSet.Order);
                System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
                Test_pay.test_payDataSetTableAdapters.PaymentTableAdapter test_payDataSetPaymentTableAdapter = new Test_pay.test_payDataSetTableAdapters.PaymentTableAdapter();
                test_payDataSetPaymentTableAdapter.Connection.ConnectionString = sql.sqlconnect.ConnectionString;
                test_payDataSetPaymentTableAdapter.Fill(test_payDataSet.Payment);
                System.Windows.Data.CollectionViewSource paymentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("paymentViewSource")));
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window AccountAdd = new AcoountAdd();
            AccountAdd.ShowDialog();
            menu.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window PaymentAdd = new PaymentAdd();
            PaymentAdd.ShowDialog();
            menu.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }
    }
}

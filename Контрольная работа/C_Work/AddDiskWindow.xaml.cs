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

namespace C_Work
{
    public partial class AddDiskWindow : Window
    {
        public MainWindow mw;

        public AddDiskWindow()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            mw.addDisk(nameTB.Text, Convert.ToDouble(priceTB.Text), codeTB.Text, typeTB.Text, incTB.Text);
            clearFields();
            this.Close();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            clearFields();
            this.Close();
        }

        void clearFields()
        {
            nameTB.Clear();
            priceTB.Clear();
            codeTB.Clear();
            typeTB.Clear();
            incTB.Clear();
        }
               
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clearFields();
        }

        private void priceTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)                               ///Ввод только цифр
                || (e.Text == "," && !priceTB.Text.Contains(","))))      ///Запятая
                e.Handled = true;
        }

        private void priceTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

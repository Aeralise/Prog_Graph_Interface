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
    public partial class AddBookWindow : Window
    {
        public MainWindow mw;

        public AddBookWindow()
        {
            InitializeComponent();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Вызов метода в классе MainWindow с переданными данными
            mw.addBook(nameTB.Text, Convert.ToDouble(priceTB.Text), codeTB.Text, autorTB.Text, Convert.ToInt32(pagesCountTB.Text));
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
            autorTB.Clear();
            pagesCountTB.Clear();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            clearFields();
        }

        
        private void pagesCountTB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))                               ///Только цифры
                e.Handled = true;
        }

        private void pagesCountTB_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
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

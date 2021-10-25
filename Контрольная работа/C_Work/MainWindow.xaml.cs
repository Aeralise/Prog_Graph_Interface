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
using System.Data.SQLite;
using Microsoft.Win32;

namespace C_Work
{   
    public partial class MainWindow : Window
    {
        SQLiteConnection m_dbConnection;
        ///Листы товаров
        public List<Item> productCollection = new List<Item>();
        public List<Disk> diskCollection = new List<Disk>();
        public List<Book> bookCollection = new List<Book>();
        ///Окна добавления
        AddBookWindow addBookWindow;
        AddDiskWindow addDiskWindow;  

        public MainWindow()
        {
            InitializeComponent();
            toggleUI(false);    ///Заблокировать кнопки (когда нет БД)      
        }

        private void openDBBtn_Click(object sender, RoutedEventArgs e)
        {          
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Файл базы данных (*.db)|*.db";
            dlg.ShowDialog();
            if (dlg.FileName != "") ///Когда выбрана БД
            {
                productCollection.Clear();
                diskCollection.Clear();
                bookCollection.Clear();
                /// Содеинение с БД
                string db_name = dlg.FileName;
                m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");                
                m_dbConnection.Open();
                ///Считывание БД
                string sql = "SELECT * FROM Item";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    ///Товар в целом
                    Item item = new Item();
                    item.id = id;
                    item.name = reader.GetString(1);
                    string priceS = reader.GetString(2);    
                    priceS.Replace(",", ".");               ///Преобразование формата double
                    item.price = Convert.ToDouble(priceS);  
                    item.code = reader.GetString(3);

                    ///Книга или диск (по id товара)
                    sql = $"SELECT autor, pages FROM Book WHERE id={id}";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader reader2 = command.ExecuteReader();
                    if (reader2.Read()) // если это книга
                    {
                        Book book = new Book();
                        book.id = id;
                        book.autor = reader2.GetString(0);
                        book.pages = reader2.GetInt32(1);
                        bookCollection.Add(book);
                        item.info = "автор - " + book.autor + ", число страниц - " + book.pages;
                    }
                    else
                    {
                        sql = $"SELECT type, inc FROM Disk WHERE id={id}";
                        command = new SQLiteCommand(sql, m_dbConnection);
                        reader2 = command.ExecuteReader();
                        reader2.Read();
                        Disk disk = new Disk();
                        disk.id = id;
                        disk.type = reader2.GetString(0);
                        disk.inc = reader2.GetString(1);
                        diskCollection.Add(disk);
                        item.info = "тип - " + disk.type + ", содержимое - " + disk.inc;
                    }
                    productCollection.Add(item); ///Добавление товара в лист
                }
                dataGrid.ItemsSource = productCollection; ///Добавление в БД приложения
                toggleUI(true); ///Включение кнопок
            }
        }

        ///Включение соответствующего окна добавления товара
        private void addBookBtn_Click(object sender, RoutedEventArgs e)
        {
            addBookWindow = new AddBookWindow();
            addBookWindow.mw = this;    ///Связь окон с этим окном
            addBookWindow.Show();           
        }

        private void addDiskBtn_Click(object sender, RoutedEventArgs e)
        {
            addDiskWindow = new AddDiskWindow();
            addDiskWindow.mw = this;
            addDiskWindow.Show();
        }

        private void removeItemBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Удаляет из БД выбранное в DataGrid 
            Item item = dataGrid.SelectedItem as Item;
            string sql = $"DELETE FROM Book WHERE id={item.id};" +
                $"DELETE FROM Disk WHERE id={item.id};";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            sql = $"DELETE FROM Item WHERE id={item.id}";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ///Актуализирование БД в приложении 
            productCollection.Remove(item);
            dataGrid.ItemsSource = productCollection;
            dataGrid.Items.Refresh();
        }

        ///Фильтрация по виду товара
        private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem treeViewItem = treeView.SelectedItem as TreeViewItem;
            switch (treeViewItem.Header)
            {
                case "Товары":
                    {
                        dataGrid.ItemsSource = productCollection;
                        dataGrid.Items.Refresh();
                        break;
                    }
                case "Книги":
                    {
                        List<Item> books = new List<Item>();
                        foreach (Item i in productCollection) 
                        {
                            var found = bookCollection.Find(x => x.id == i.id); 
                            if (found != null)
                                books.Add(i);
                        }
                        dataGrid.ItemsSource = books;
                        dataGrid.Items.Refresh();
                        break;
                    }
                case "Диски":
                    {
                        List<Item> disks = new List<Item>();
                        foreach (Item t in productCollection)
                        {
                            var finded = diskCollection.Find(x => x.id == t.id); 
                            if (finded != null)
                                disks.Add(t);
                        }
                        dataGrid.ItemsSource = disks;
                        dataGrid.Items.Refresh();
                        break;
                    }
            }
        }

        public void addBook(string name, double price, string code, string autor, int pages)
        {
            price = Math.Round(price, 2);   ///Округление цены до двух цифр после запятой

            ///Добавление в БД
            string sql = "INSERT INTO Item (name,price,code) VALUES ('" + name + "','" + price + "','" + code + "');" +
                "SELECT last_insert_rowid();";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            long id = (long)command.ExecuteScalar();

            sql = "INSERT INTO Book (id,autor,pages) VALUES ('" + id + "','" + autor + "','" + pages + "')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ///Добавление в листы приложения
            Item item = new Item();
            item.id = (int)id;
            item.name = name;
            item.price = price;
            item.code = code;

            Book book = new Book();
            book.id = (int)id;
            book.autor = autor;
            book.pages = pages;
            item.info = "автор - " + book.autor + ", число страниц - " + book.pages;
            productCollection.Add(item);
            bookCollection.Add(book);
            dataGrid.ItemsSource = productCollection;
            dataGrid.Items.Refresh();
        }

        public void addDisk(string name, double price, string code, string type, string inc)
        {
            price = Math.Round(price, 2);   ///Округление цены до двух цифр после запятой

            ///Добавление в БД
            string sql = "INSERT INTO Item (name,price,code) VALUES ('" + name + "','" + price + "','" + code + "');" +
                "SELECT last_insert_rowid();";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            long id = (long)command.ExecuteScalar();

            sql = "INSERT INTO Disk (id,type,inc) VALUES ('" + id + "','" + type + "','" + inc + "')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ///Добавление в листы приложения
            Item item = new Item();
            item.id = (int)id;
            item.name = name;
            item.price = price;
            item.code = code;

            Disk disk = new Disk();
            disk.id = (int)id;
            disk.type = type;
            disk.inc = inc;
            item.info = "автор - " + disk.type + ", содержимое - " + disk.inc;
            productCollection.Add(item);
            diskCollection.Add(disk);
            dataGrid.ItemsSource = productCollection;
            dataGrid.Items.Refresh();
        }

        void toggleUI(bool toggle)  ///Переключение доступности кнопок
        {
            treeView.IsEnabled = toggle;
            addBookBtn.IsEnabled = toggle;
            addDiskBtn.IsEnabled = toggle;
            removeItemBtn.IsEnabled = toggle;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.Shutdown();     /// Полное закрытие приложения с любыми соединениями с БД
        }
    }
}

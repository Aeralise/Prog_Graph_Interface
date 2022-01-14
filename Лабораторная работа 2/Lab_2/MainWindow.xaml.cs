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

namespace Lab_2
{
    public partial class MainWindow : Window
    {
        SQLiteConnection m_dbConnection;
        List<Student> students = new List<Student>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openDBBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Подготовка к открытию БД
            students.Clear();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Файл базы данных (*.db)|*.db";
            dlg.ShowDialog();
            if (dlg.FileName != "")
            {
                string db_name = dlg.FileName;
                m_dbConnection = new SQLiteConnection("Data Source=" + db_name + ";Version=3;");
                ///Соединение с БД
                m_dbConnection.Open();
                ///Считывание БД 
                string sql = "SELECT * FROM STUDENT";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ///Считывание студентов с БД
                    int id = reader.GetInt32(0);
                    sql = $"SELECT MATH, PHYSICS FROM GRADES WHERE STUDENT_ID={id}";
                    command = new SQLiteCommand(sql, m_dbConnection);
                    SQLiteDataReader reader2 = command.ExecuteReader();
                    reader2.Read();
                    ///Создание студента
                    Student st = new Student();
                    st.Id = id;
                    st.Name = reader.GetString(1);
                    st.Math = reader2.GetInt32(0);
                    st.Physics = reader2.GetInt32(1);
                    st.PhoneNumber = reader.GetString(2);
                    students.Add(st);
                }
                dataGrid.ItemsSource = students;    ///Внесение полученных данных в таблицу в приложении
                ///Включение элементов интерфейса
                studentNameTB.IsEnabled = true;
                phoneTB.IsEnabled = true;
                mathSlider.IsEnabled = true;
                physicSlider.IsEnabled = true;
                AddBtn.IsEnabled = true;
                EditBtn.IsEnabled = true;
                DelBtn.IsEnabled = true;
            }
        }

        ///Добавление нового
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Добавление в БД
            string sql = "INSERT INTO STUDENT (NAME,PHONE) VALUES ('" + studentNameTB.Text + "','" + phoneTB.Text + "');" +
                "SELECT last_insert_rowid();";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            long id = (long)command.ExecuteScalar();

            sql = "INSERT INTO GRADES (STUDENT_ID,MATH,PHYSICS) VALUES ('" + id + "','" + (int)mathSlider.Value + "','" + (int)physicSlider.Value + "')";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            ///Добавление в лист и таблицу в приложении
            Student st = new Student();
            st.Id = (int)id;
            st.Name = studentNameTB.Text;
            st.Math = (int)mathSlider.Value;
            st.Physics = (int)physicSlider.Value;
            st.PhoneNumber = phoneTB.Text;
            students.Add(st);
            dataGrid.ItemsSource = students;
            dataGrid.Items.Refresh();
        }

        ///Обновление данных в выбранной ячейке
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            Student st = dataGrid.SelectedItem as Student;
            st.Name = studentNameTB.Text;
            st.Math = (int)mathSlider.Value;
            st.Physics = (int)physicSlider.Value;
            st.PhoneNumber = phoneTB.Text;
            string sql = $"UPDATE GRADES SET MATH = {st.Math}, PHYSICS = {st.Physics} WHERE STUDENT_ID = {st.Id};" +
                $"UPDATE STUDENT SET NAME = '{st.Name}', PHONE = '{st.PhoneNumber}' WHERE ID = {st.Id};";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            ///Выполнение запроса
            command.ExecuteNonQuery();
            dataGrid.ItemsSource = students;
            dataGrid.Items.Refresh();
        }

        ///Удаление выбранного
        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Удаление выделения из DataGrid и БД (сначала оценки)
            Student st = dataGrid.SelectedItem as Student;
            string sql = $"DELETE FROM GRADES WHERE STUDENT_ID={st.Id}";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ///Тоже самое с самим студентом
            sql = $"DELETE FROM STUDENT WHERE ID={st.Id}";
            command = new SQLiteCommand(sql, m_dbConnection);           
            command.ExecuteNonQuery();

            ///Обновление DataGrid
            students.Remove(st);
            dataGrid.ItemsSource = students;
            dataGrid.Items.Refresh();
        }

        ///Отображение выбранной оценки в виде цифры
        private void mathSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mathSValue != null)
            mathSValue.Content = mathSlider.Value.ToString();
        }

        private void physicSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (physSValue != null)
                physSValue.Content = physicSlider.Value.ToString();
        }

    }


}




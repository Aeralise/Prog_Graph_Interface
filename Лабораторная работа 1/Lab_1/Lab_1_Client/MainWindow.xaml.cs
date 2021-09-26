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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Lab_1_Client
{
    public partial class MainWindow : Window
    {       
        int port = 8888;    /// номер порта для обмена сообщениями       
        string address = "127.0.0.1";   /// IP-адрес сервера       
        TcpClient client = null;    /// объявление TCP-клиента        
        NetworkStream stream = null;    /// объявление канала соединения с сервером       
        string name = "";   /// имя пользователя 

        public MainWindow()
        {
            InitializeComponent();
        }

        ///Кнопка "Подключиться"
        private void connectBtn_Click(object sender, RoutedEventArgs e)
        {
            /// получение имени пользователя            
            name = nameTB.Text;
            if (name != "")
            {
                try
                {                   
                    client = new TcpClient(address, port);  /// создание клиента                   
                    stream = client.GetStream();    /// получение канала для обмена сообщениями

                    /// создание нового потока для ожидания сообщения от сервера
                    Thread listenThread = new Thread(() => listen());
                    listenThread.Start();

                    SystemMessageSend("присоединился");

                    nameTB.IsEnabled = false;
                    recieveTB.IsEnabled = true;
                    sendBtn.IsEnabled = true;
                    sendAllCheck.IsEnabled = true;
                    disconnectBtn.IsEnabled = true;
                    connectBtn.IsEnabled = false;
                    messageTB.IsEnabled = true;
                }
                catch (Exception ex)
                {
                    messageLogBox.Items.Add(ex.Message);
                }
            }
            else
                messageLogBox.Items.Add("Ваше имя не введено");
        }

        ///Кнопка "Отключиться"
        private void disconnectBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemMessageSend("покинул чат");

            CloseStream();

            nameTB.IsEnabled = true;
            recieveTB.IsEnabled = false;
            sendBtn.IsEnabled = false;
            sendAllCheck.IsEnabled = false;
            disconnectBtn.IsEnabled = false;
            connectBtn.IsEnabled = true;
            messageTB.IsEnabled = false;
        }

        ///Кнопка "Отправить"
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            if (messageTB.Text != "" && ((recieveTB.Text != "" && sendAllCheck.IsChecked == false) || sendAllCheck.IsChecked == true))  /// если поле сообщения не пустое, либо 
            {
                string message = messageTB.Text;        /// сообщение
                string recieveName = recieveTB.Text;    /// имя получателя
                string messageText = "";
                string sendAll = "";
                if (sendAllCheck.IsChecked == true)
                    sendAll = "1";
                else
                    sendAll = "0";

                messageText = String.Format("{0}~{1}~{2}~{3}", name, message, recieveName, sendAll);    /// преобразование сообщение в массив байтов
                byte[] data = Encoding.Unicode.GetBytes(messageText);
                stream.Write(data, 0, data.Length);  /// отправка сообщения

                messageTB.Text = "";
            }                                   /// сообщения об ошибках
            else if (messageTB.Text == "")
                messageLogBox.Items.Add("Поле вашего сообщения пустое");
            else if (recieveTB.Text == "" && sendAllCheck.IsChecked == false)
                messageLogBox.Items.Add("Не указан собеседник");
        }

        ///Прослушивание на сообщения от сервера
        void listen()
        {
            try
            {
                while (true)
                {
                    
                    byte[] data = new byte[64]; /// буфер для получаемых данных
                    
                    StringBuilder builder = new StringBuilder();    /// объект для построения строк
                    int bytes = 0;
                    do /// до тех пор, пока есть данные в потоке
                    {                       
                        bytes = stream.Read(data, 0, data.Length);   /// получение 64 байт                       
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes)); /// формирование строки
                    }
                    while (stream.DataAvailable);

                    if (bytes == 0) 
                        break;                   
                    string message = builder.ToString();    /// получить строку                   
                    Dispatcher.BeginInvoke(new Action(() => messageLogBox.Items.Add(message))); /// вывод сообщения в лог клиента
                }
            }
            catch (Exception ex)
            {              
                Dispatcher.BeginInvoke(new Action(() => messageLogBox.Items.Add(ex.Message)));  /// вывести сообщение об ошибке
            }
            finally
            {                
                CloseStream();  /// закрыть канал связи и завершить работу клиента
            }
        }

        void SystemMessageSend(string message)      /// системные автоматические сообщения в чат
        {
            string messageText = String.Format("{0}~{1}~{2}~{3}", name, message, "", 1);    /// преобразование сообщение в массив байтов
            byte[] data = Encoding.Unicode.GetBytes(messageText);
            stream.Write(data, 0, data.Length);                                             /// отправка сообщения
        }

        void CloseStream()
        {
            if (client != null)
                client.Close();
            if (stream != null)
                stream.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseStream();
        }
    }
}

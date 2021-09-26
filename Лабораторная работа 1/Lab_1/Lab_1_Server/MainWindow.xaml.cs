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

namespace Lab_1_Server
{
    public partial class MainWindow : Window
    {
        int port = 8888;        
        static TcpListener listener;
        Dictionary<string, TcpClient> tcpClients = new Dictionary<string, TcpClient>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startServerBtn_Click(object sender, RoutedEventArgs e)
        {
            ///Запуск прослушивателя по адресу и порту
            listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            listener.Start();

            ///Запуск потока для прослушивания подключений
            Thread listenThread = new Thread(() => listen());
            listenThread.Start();

            serverLogBox.Items.Add("Сервер запущен");

            startServerBtn.IsEnabled = false;
            stopServerBtn.IsEnabled = true;
        }
                
        ///Ожидание и прием на подключение
        void listen()
        {
            try
            {
                /// цикл подключения клиентов
                while (true)
                {
                    /// принятие запроса на подключение
                    TcpClient client = listener.AcceptTcpClient();

                    Dispatcher.BeginInvoke(new Action(() => serverLogBox.Items.Add("Новый клиент подключен")));

                    /// создание нового потока для обслуживания нового клиента
                    Thread clientThread = new Thread(() => Process(client));
                    clientThread.Start();

                }
            }
            catch (Exception e)
            {
                Dispatcher.BeginInvoke(new Action(() => serverLogBox.Items.Add(e.Message)));
            }
        }

        ///Обработка сообщений от клиента
        public void Process(TcpClient tcpClient)
        {
            TcpClient client = tcpClient;
            NetworkStream stream = null;

            try
            {
                stream = client.GetStream();    ///Канал связи с клиентом
                byte[] data = new byte[64];

                while (true)
                {
                    /// ========================== получение сообщения ============================
                    StringBuilder builder = new StringBuilder(); /// объект для формирования строк
                    int bytes = 0;
                    do /// до тех пор, пока в потоке есть данные
                    {
                        /// из потока считываются 64 байта и записываются в data, начиная с 0
                        bytes = stream.Read(data, 0, data.Length);
                        /// из считанных данных формируется строка
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);
                    if (bytes == 0) break;
                    /// преобразование сообщения
                    string message = builder.ToString();

                    string[] messageParts = message.Split('~');
                    string userName = messageParts[0];
                    string messageText = " " + messageParts[1];
                    string clientName = messageParts[2];
                    string sendAll = messageParts[3];       ///Содержит значение, отправить ли сообщение всем

                    if (!tcpClients.ContainsKey(userName))
                        tcpClients.Add(userName, client);
                    /// вывод сообщения в лог сервера
                    Dispatcher.BeginInvoke(new Action(() => serverLogBox.Items.Add(message)));
                    /// ========================== отправка сообщения =============================
                    /// преобразование сообщения в набор байтов
                    data = Encoding.Unicode.GetBytes(userName + ":" + messageText);

                    if (sendAll == "1")
                    {
                        foreach (string clientDName in tcpClients.Keys)
                        {
                            NetworkStream networkStream = tcpClients[clientDName].GetStream();
                            networkStream.Write(data, 0, data.Length);
                        }
                    }
                    else if (tcpClients.ContainsKey(clientName))
                    {
                        NetworkStream networkStream = tcpClients[clientName].GetStream();
                        networkStream.Write(data, 0, data.Length);
                    }

                    /// отправка сообщения обратно клиенту
                    if (sendAll == "0")
                        stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex) /// если возникла ошибка, вывести сообщение об ошибке
            {
                Dispatcher.BeginInvoke(new Action(() => serverLogBox.Items.Add(ex.Message)));
            }
            finally ///после выхода из бесконечного цикла
            {
                /// освобождение ресурсов при завершении сеанса
                if (stream != null)
                    stream.Close();
                if (client != null)
                    client.Close();
            }
        }

        private void stopServerBtn_Click(object sender, RoutedEventArgs e)
        {
            stopServer();
            startServerBtn.IsEnabled = true;
            stopServerBtn.IsEnabled = false;
        }

        void stopServer()
        {
            ///Разрыв соединения со всеми клиентами
            foreach (TcpClient client in tcpClients.Values)
            {
                if (client != null)
                    client.Close();
            }
            if (listener != null)
                listener.Stop();
            serverLogBox.Items.Add("Сервер остановлен");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            stopServer();
        }
    }
}

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
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using System.Media;

namespace Lab_3
{
    public partial class MainWindow : Window
    {
        DispatcherTimer ticker;
        bool tickToTimeSlider = true; ///Двигать ли слайдер времени тиками

        public MainWindow()
        {
            InitializeComponent();
            ticker = new DispatcherTimer();
            ticker.Tick += ticker_Tick;
        }

        private void openMI_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            ///Настройка источника и автовоспроизведение                   
            mediaElement.Source = new Uri(dlg.FileName, UriKind.Relative);
            mediaElement.Play();
            ticker.Start();
        }

        private void mediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            try
            {
                timeSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
                fileTimeLbl.Content = String.Format("{0:00}:{1:00}:{2:00}", mediaElement.NaturalDuration.TimeSpan.Hours, mediaElement.NaturalDuration.TimeSpan.Minutes, mediaElement.NaturalDuration.TimeSpan.Seconds);
                mediaElement.Volume = volumeSlider.Value;
            }
            catch   ///Если выбрали НЕ видео (картинку)
            {
                SystemSounds.Hand.Play();
            }
        }

        ///Если был выбран неверный тип файла
        private void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            SystemSounds.Hand.Play();
            MessageBox.Show("Файл не является файлом мультимедиа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            mediaElement.Source = null;
        }

        private void playBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
            ticker.Start();
        }

        private void pauseBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
            ticker.Stop();
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            ticker.Stop();
            timeSlider.Value = 0;
        }

        ///Когда пользователь использует слайдер для промотки
        private void timeSlider_GotMouseCapture(object sender, MouseEventArgs e)
        {
            tickToTimeSlider = false;
        }

        ///Перемотка к соответствующему новому положению слайдера
        private void timeSlider_ValueChanged(object sender, DragCompletedEventArgs e)
        {
            int sliderValue = (int)timeSlider.Value;
            TimeSpan timeSpan = new TimeSpan(0, 0, 0, 0, sliderValue);
            mediaElement.Position = timeSpan;
            tickToTimeSlider = true;
        }
        
        ///Громкость звука
        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = volumeSlider.Value;
        }               

        ///Синхронизация положения слайдера с текущим положением во времени файла
        private void ticker_Tick(object sender, EventArgs e)
        {
            currentTimeLbl.Content = String.Format("{0:00}:{1:00}:{2:00}", mediaElement.Position.Hours, mediaElement.Position.Minutes, mediaElement.Position.Seconds);
            if (tickToTimeSlider)
                timeSlider.Value = mediaElement.Position.TotalMilliseconds;
        }
    }
}

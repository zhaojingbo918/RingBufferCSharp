using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace RingCacheManagerTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private RingBufferManager m_Buffer = new RingBufferManager(10);
        MainViewModel m_ViewModel = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = m_ViewModel;
        }
        int index = 0;

        Thread t;
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (t != null)
                return;

            t = new Thread((o) =>
            {

                while (true)
                {
                    if (!m_IsPause)
                    {
                        BurrerData data = new BurrerData() { Data = index++ };
                        m_Buffer.WriteBuffer(data);
                        m_ViewModel.StartIndex = m_Buffer.GetStartIndex();
                        m_ViewModel.EndIndex = m_Buffer.DataEnd;
                        m_ViewModel.DataCount = m_Buffer.GetDataCount();
                    }


                    Thread.Sleep(100);
                }

            });
            t.Start();
        }

        private bool m_IsPause;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            m_IsPause = !m_IsPause;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int count = m_Buffer.GetDataCount();
            int startIndex = m_Buffer.GetStartIndex();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                var data = m_Buffer.Buffer[(startIndex + i) % m_Buffer.Buffer.Length];
                sb.Append(data.Data);
                sb.AppendLine();
            }
            this.lb1.Content = sb.ToString();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            m_Buffer.Clear();
        }
    }
}

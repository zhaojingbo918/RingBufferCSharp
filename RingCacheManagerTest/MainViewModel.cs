using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingCacheManagerTest
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private int m_StartIndex;

        public int StartIndex
        {
            get { return m_StartIndex; }
            set
            {
                m_StartIndex = value;
                this.OnPropertyChanged(nameof(StartIndex));
            }
        }


        private int m_EndIndex;

        public int EndIndex
        {
            get { return m_EndIndex; }
            set
            {
                m_EndIndex = value;
                this.OnPropertyChanged(nameof(EndIndex));
            }
        }

        private int m_DataCount;

        public int DataCount
        {
            get { return m_DataCount; }
            set
            {
                m_DataCount = value;
                this.OnPropertyChanged(nameof(DataCount));
            }
        }


    }
}

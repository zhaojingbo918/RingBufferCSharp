using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RingCacheManagerTest
{
    public class RingBufferManager
    {
        public BurrerData[] Buffer { get; set; } // 存放内存的数组
        public int DataCount { get; set; } // 写入数据大小 
        public int DataEnd { get; set; }   // 数据结束索引
        public RingBufferManager(int bufferSize)
        {
            DataCount = 0; 
            DataEnd = 0;
            Buffer = new BurrerData[bufferSize];
        }
         
        public int GetDataCount() // 获得当前写入的字节数
        {
            return Math.Min(DataCount, this.Buffer.Length);
        }

        public void Clear()
        {
            DataCount = 0; 
            m_HaseRestart = false;
            DataEnd = 0;
          
            Array.Clear(this.Buffer, 0, this.Buffer.Length);
        }

        private bool m_HaseRestart;

        public void WriteBuffer(BurrerData buffer)
        {
            if (DataEnd < Buffer.Length)            // 数据没到结尾
            {
                Buffer[DataEnd] = buffer;
                DataEnd += 1;
                DataCount += 1;
            }
            else           //  数据结束索引超出结尾 循环到开始
            {
                m_HaseRestart = true;
                System.Diagnostics.Debug.WriteLine("缓存重新开始....");

                DataEnd = 0;
                Buffer[DataEnd] = buffer;
                DataEnd += 1;
                DataCount += 1;
            }
           
        }

        public int GetStartIndex()
        {
            if(m_HaseRestart)
            {
                return DataEnd;
            }
            return 0;
        }

    }


    public class BurrerData
    {
        public DateTime Time { get; set; }

        public int Data;
    }
}

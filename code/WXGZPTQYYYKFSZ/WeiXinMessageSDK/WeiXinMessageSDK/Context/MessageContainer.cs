using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Senparc.Weixin.MP.Context
{
    public class MessageContainer<T> : List<T>
    {
        /// <summary>
        /// 最大记录条数（保留尾部），如果小于等于0则不限制
        /// </summary>
        public int MaxRecordCount { get; set; }

        private MessageContainer()
        {
        }

        public MessageContainer(int maxRecordCount)
        {
            MaxRecordCount = maxRecordCount;
        }

        new public void Add(T item)
        {
            base.Add(item);
            RemoveExpressItems();
        }

        new public void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            RemoveExpressItems();
        }

        new public void Insert(int index, T item)
        {
            base.Insert(index, item);
            RemoveExpressItems();
        }

        new public void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            RemoveExpressItems();
        }

        /// <summary>
        /// 如果消息记录超过数量限制，移除最早的消息
        /// </summary>
        private void RemoveExpressItems()
        {
            if (MaxRecordCount > 0 && base.Count > MaxRecordCount)
            {
                base.RemoveRange(0, base.Count - MaxRecordCount);
            }
        }
    }
}

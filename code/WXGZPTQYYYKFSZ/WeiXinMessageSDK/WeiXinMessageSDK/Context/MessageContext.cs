using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Senparc.Weixin.MP.Entities;

namespace Senparc.Weixin.MP.Context
{
    public interface IMessageContext
    {
        /// <summary>
        /// 用户名（OpenID）
        /// </summary>
        string UserName { get; set; }
        /// <summary>
        /// 最后一次活动时间（用户主动发送Resquest请求的时间）
        /// </summary>
        DateTime LastActiveTime { get; set; }
        /// <summary>
        /// 用户发送消息记录
        /// </summary>
        MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
        /// <summary>
        /// 用户接收消息记录
        /// </summary>
        MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
        /// <summary>
        /// 最大储存容量（分别针对RequestMessages和ResponseMessages）
        /// </summary>
        int MaxRecordCount { get; set; }
        /// <summary>
        /// 临时储存数据，如用户状态等，出于保持.net 3.5版本，这里暂不使用dynamic
        /// </summary>
        object StorageData { get; set; }
        /// <summary>
        /// 由具体的业务场景根据需求，编写用户上下文被从集合移除时需要额外执行的方法注册到该事件上
        /// </summary>
        event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved;
        /// <summary>
        /// 供集合移除用户会话上下文时调用的回调方法
        /// </summary>
        void OnRemoved();
    }

    /// <summary>
    /// 微信用户上下文信息（单个用户）
    /// </summary>
    public class MessageContext : IMessageContext
    {
        private int _maxRecordCount;
        public string UserName { get; set; }
        public DateTime LastActiveTime { get; set; }
        public MessageContainer<IRequestMessageBase> RequestMessages { get; set; }
        public MessageContainer<IResponseMessageBase> ResponseMessages { get; set; }
        public int MaxRecordCount
        {
            get
            {
                return _maxRecordCount;
            }
            set
            {
                RequestMessages.MaxRecordCount = value;
                ResponseMessages.MaxRecordCount = value;

                _maxRecordCount = value;
            }
        }
        public object StorageData { get; set; }
        public event EventHandler<WeixinContextRemovedEventArgs> MessageContextRemoved = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxRecordCount">maxRecordCount如果小于等于0，则不限制</param>
        public MessageContext()
        {
            /*
             * 注意：即使使用其他类实现IMessageContext，
             * 也务必在这里进行下面的初始化，尤其是设置当前时间，
             * 这个时间关系到及时从缓存中移除过期的消息，节约内存使用
             */
            RequestMessages = new MessageContainer<IRequestMessageBase>(MaxRecordCount);
            ResponseMessages = new MessageContainer<IResponseMessageBase>(MaxRecordCount);
            LastActiveTime = DateTime.Now;
        }
        public virtual void OnRemoved()
        {
            var onRemovedArg = new WeixinContextRemovedEventArgs(this);
            OnMessageContextRemoved(onRemovedArg);
        }
        /// <summary>
        /// 执行上下文被移除的事件
        /// 注意：此事件不是实时触发的，而是等过期后任意一个人发过来的下一条消息执行之前触发。
        /// </summary>
        /// <param name="e"></param>
        private void OnMessageContextRemoved(WeixinContextRemovedEventArgs e)
        {
            EventHandler<WeixinContextRemovedEventArgs> temp = MessageContextRemoved;

            if (temp != null)
            {
                temp(this, e);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.Entities
{
    /// <summary>
    /// 公众号回复消息数据规范接口
    /// </summary>
    public interface IResponseMessageBase : IMessageBase
    {
        /// <summary>
        /// 公众号回复消息类型
        /// </summary>
        ResponseMsgType MsgType { get; }
    }

    /// <summary>
    /// 公众号回复消息基类
    /// </summary>
    public class ResponseMessageBase : MessageBase, IResponseMessageBase
    {
        /// <summary>
        /// 公众号回复消息类型
        /// </summary>
        public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Text; }
        }
    }
}

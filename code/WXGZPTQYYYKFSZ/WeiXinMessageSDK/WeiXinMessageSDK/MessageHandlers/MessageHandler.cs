using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace Senparc.Weixin.MP.MessageHandlers
{
    public interface IMessageHandler
    {
        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        string WeixinOpenId { get; }
        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        bool CancelExcute { get; set; }
        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        XDocument RequestDocument { get; set; }
        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        XDocument ResponseDocument { get; }
        /// <summary>
        /// 请求实体
        /// </summary>
        IRequestMessageBase RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 只有当执行Execute()方法后才可能有值
        /// </summary>
        IResponseMessageBase ResponseMessage { get; set; }
        /// <summary>
        /// 执行微信请求的消息处理
        /// </summary>
        void Execute();
        /// <summary>
        /// 在执行消息处理前需要执行的操作
        /// </summary>
        void OnExecuting();
        /// <summary>
        /// 在执行完消息处理后需要执行的操作
        /// </summary>
        void OnExecuted();
    }

    /// <summary>
    /// 微信请求的集中处理方法
    /// 此方法中所有过程，都基于Senparc.Weixin.MP的基础功能，只为简化代码而设。
    /// </summary>
    public abstract class MessageHandler<TC> : IMessageHandler where TC : class, IMessageContext, new()
    {
        /// <summary>
        /// 上下文
        /// </summary>
        protected static WeixinContext<TC> GlobalWeixinContext = new WeixinContext<TC>();
        /// <summary>
        /// 全局消息上下文
        /// </summary>
        protected WeixinContext<TC> WeixinContext
        {
            get { return GlobalWeixinContext; }
        }
        /// <summary>
        /// 当前用户消息上下文
        /// </summary>
        public TC CurrentMessageContext
        {
            get { return WeixinContext.GetMessageContext(RequestMessage); }
        }
        /// <summary>
        /// 发送者用户名（OpenId）
        /// </summary>
        public string WeixinOpenId
        {
            get
            {
                if (RequestMessage != null)
                {
                    return RequestMessage.FromUserName;
                }
                return null;
            }
        }
        /// <summary>
        /// 取消执行Execute()方法。一般在OnExecuting()中用于临时阻止执行Execute()。
        /// 默认为False。
        /// 如果在执行OnExecuting()执行前设为True，则所有OnExecuting()、Execute()、OnExecuted()代码都不会被执行。
        /// 如果在执行OnExecuting()执行过程中设为True，则后续Execute()及OnExecuted()代码不会被执行。
        /// 建议在设为True的时候，给ResponseMessage赋值，以返回友好信息。
        /// </summary>
        public bool CancelExcute { get; set; }
        /// <summary>
        /// 在构造函数中转换得到原始XML数据
        /// </summary>
        public XDocument RequestDocument { get; set; }
        /// <summary>
        /// 根据ResponseMessageBase获得转换后的ResponseDocument
        /// 注意：这里每次请求都会根据当前的ResponseMessageBase生成一次，如需重用此数据，建议使用缓存或局部变量
        /// </summary>
        public XDocument ResponseDocument
        {
            get
            {
                if (ResponseMessage == null)
                {
                    return null;
                }
                return EntityHelper.ConvertEntityToXml(ResponseMessage as ResponseMessageBase);
            }
        }
        /// <summary>
        /// 请求实体
        /// </summary>
        public IRequestMessageBase RequestMessage { get; set; }
        /// <summary>
        /// 响应实体
        /// 正常情况下只有当执行Execute()方法后才可能有值。
        /// 也可以结合Cancel，提前给ResponseMessage赋值。
        /// </summary>
        public IResponseMessageBase ResponseMessage { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        public MessageHandler(Stream inputStream, int maxRecordCount = 0)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            using (XmlReader xr = XmlReader.Create(inputStream))
            {
                RequestDocument = XDocument.Load(xr);
                Init(RequestDocument);
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="requestDocument"></param>
        /// <param name="maxRecordCount"></param>
        public MessageHandler(XDocument requestDocument, int maxRecordCount = 0)
        {
            WeixinContext.MaxRecordCount = maxRecordCount;
            Init(requestDocument);
        }
        /// <summary>
        /// 执行微信请求的消息处理
        /// </summary>
        public void Execute()
        {
            //判断是否要终止消息处理过程
            if (CancelExcute)
            {
                return;
            }
            //消息预处理
            OnExecuting();
            //判断是否要终止消息处理过程
            if (CancelExcute)
            {
                return;
            }
            try
            {
                if (RequestMessage == null)
                {
                    return;
                }
                //根据具体的消息类型调用不同类型的消息处理方法来处理消息
                switch (RequestMessage.MsgType)
                {
                    case RequestMsgType.Text:
                        ResponseMessage = OnTextRequest(RequestMessage as RequestMessageText);
                        break;
                    case RequestMsgType.Location:
                        ResponseMessage = OnLocationRequest(RequestMessage as RequestMessageLocation);
                        break;
                    case RequestMsgType.Image:
                        ResponseMessage = OnImageRequest(RequestMessage as RequestMessageImage);
                        break;
                    case RequestMsgType.Voice:
                        ResponseMessage = OnVoiceRequest(RequestMessage as RequestMessageVoice);
                        break;
                    case RequestMsgType.Video:
                        ResponseMessage = OnVideoRequest(RequestMessage as RequestMessageVideo);
                        break;
                    case RequestMsgType.Event:
                        ResponseMessage = OnEventRequest(RequestMessage as RequestMessageEventBase);
                        break;
                    default:
                        throw new UnknownRequestMsgTypeException("未知的MsgType请求类型", null);
                }
                //记录上下文
                if (WeixinContextGlobal.UseWeixinContext && ResponseMessage != null)
                {
                    WeixinContext.InsertMessage(ResponseMessage);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //返回响应结果前的特殊处理
                OnExecuted();
            }
        }
        /// <summary>
        /// 在执行消息处理前需要执行的操作，视具体业务需求重写此方法
        /// </summary>
        public virtual void OnExecuting()
        {
        }
        /// <summary>
        /// 在执行完消息处理后需要执行的操作，视具体业务需求重写此方法
        /// </summary>
        public virtual void OnExecuted()
        {
        }
        /// <summary>
        /// 将用户发送消息的XML数据包转换为数据实体
        /// </summary>
        /// <param name="requestDocument"></param>
        private void Init(XDocument requestDocument)
        {
            RequestDocument = requestDocument;
            //将XML数据包转换为数据实体
            RequestMessage = RequestMessageFactory.GetRequestEntity(RequestDocument);
            //记录上下文
            if (WeixinContextGlobal.UseWeixinContext)
            {
                WeixinContext.InsertMessage(RequestMessage);
            }
        }
        /// <summary>
        /// 根据当前的RequestMessage创建指定类型的ResponseMessage
        /// </summary>
        /// <typeparam name="TR">基于ResponseMessageBase的响应消息类型</typeparam>
        /// <returns></returns>
        protected TR CreateResponseMessage<TR>() where TR : ResponseMessageBase
        {
            if (RequestMessage == null)
            {
                return null;
            }
            return RequestMessage.CreateResponseMessage<TR>();
        }
        /// <summary>
        /// 默认返回消息（当任何OnXX消息没有被重写，都将自动返回此默认消息）
        /// </summary>
        protected virtual IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "您发送的消息类型暂未被识别。";
            return responseMessage;
        }
        #region 每种类型的消息处理方法，视具体业务需求重写每个方法
        /// <summary>
        /// 文字类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 位置类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 图片类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 语音类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 视频类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnVideoRequest(RequestMessageVideo requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// 链接消息类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// Event事件类型请求
        /// </summary>
        protected virtual IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var strongRequestMessage = RequestMessage as IRequestMessageEventBase;
            IResponseMessageBase responseMessage = null;
            switch (strongRequestMessage.Event)
            {
                case Event.LOCATION://自动发送的用户当前位置
                    responseMessage = OnEvent_LocationRequest(RequestMessage as RequestMessageEvent_Location);
                    break;
                case Event.subscribe://订阅
                    responseMessage = OnEvent_SubscribeRequest(RequestMessage as RequestMessageEvent_Subscribe);
                    break;
                case Event.unsubscribe://退订
                    responseMessage = OnEvent_UnsubscribeRequest(RequestMessage as RequestMessageEvent_Unsubscribe);
                    break;
                case Event.CLICK://菜单点击
                    responseMessage = OnEvent_ClickRequest(RequestMessage as RequestMessageEvent_Click);
                    break;
                case Event.scan://二维码
                    responseMessage = OnEvent_ScanRequest(RequestMessage as RequestMessageEvent_Scan);
                    break;
                default:
                    throw new UnknownRequestMsgTypeException("未知的Event下属请求信息", null);
            }
            return responseMessage;
        }
        #endregion
        #region 每种事件消息处理方法，视具体业务需求重写每个方法
        /// <summary>
        /// Event事件类型请求之LOCATION
        /// </summary>
        protected virtual IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// Event事件类型请求之subscribe
        /// </summary>
        protected virtual IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// Event事件类型请求之unsubscribe
        /// </summary>
        protected virtual IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// Event事件类型请求之CLICK
        /// </summary>
        protected virtual IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        /// <summary>
        /// Event事件类型请求之scan
        /// </summary>
        protected virtual IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            return DefaultResponseMessage(requestMessage);
        }
        #endregion
    }
}

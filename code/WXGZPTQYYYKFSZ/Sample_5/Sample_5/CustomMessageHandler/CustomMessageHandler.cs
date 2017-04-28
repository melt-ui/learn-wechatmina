using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Sample_5.BLL;
using Sample_5.ViewEntity;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace Sample_5
{
/// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="inputStream"></param>
        /// <param name="maxRecordCount"></param>
        public CustomMessageHandler(Stream inputStream, int maxRecordCount = 0)
            : base(inputStream, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
        }
        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }
        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }
        /// <summary>
        /// 处理扫描请求
        /// 用户扫描带场景值二维码时，如果用户已经关注公众号，则微信会将带场景值扫描事件推送给开发者。
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            int sceneId = 0;
            int.TryParse(requestMessage.EventKey, out sceneId);
            if (sceneId > 0)
            {
                new ChannelScanBll().SaveScan(requestMessage.FromUserName, sceneId, ScanType.Scan);
            }
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "扫描已记录";
            return responseMessage;
        }
        /// <summary>
        /// 处理关注请求
        /// 用户扫描带场景值二维码时，如果用户还未关注公众号，则用户可以关注公众号，关注后微信会将带场景值关注事件推送给开发者。
        /// </summary>
        /// <returns></returns>
        protected override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            if (!string.IsNullOrWhiteSpace(requestMessage.EventKey))
            {
                string sceneIdstr = requestMessage.EventKey.Substring(8);
                int sceneId = 0;
                int.TryParse(sceneIdstr, out sceneId);
                if (sceneId > 0)
                {
                    new ChannelScanBll().SaveScan(requestMessage.FromUserName, sceneId, ScanType.Subscribe);
                }
            }
            var responseMessage = CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "扫描已记录";
            return responseMessage;
        }
    }
}
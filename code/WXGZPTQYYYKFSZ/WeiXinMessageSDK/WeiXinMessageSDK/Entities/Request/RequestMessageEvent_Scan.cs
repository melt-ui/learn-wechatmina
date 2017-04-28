using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class RequestMessageEvent_Scan : RequestMessageEventBase, IRequestMessageEventBase
    {
        public override Event Event
        {
            get { return Event.scan; }
        }
        /// <summary>
        /// 二维码的参数
        /// </summary>
        public string Ticket { get; set; }
    }
}

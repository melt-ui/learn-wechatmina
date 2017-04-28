using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageMusic : ResponseMessageBase, IResponseMessageBase
    {        
        public ResponseMessageMusic()
        {
            Music = new Music();
        }
        public override ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Music; }
        }
        public Music Music { get; set; }
    }
}

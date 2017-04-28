using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP.Entities
{
    public class ResponseMessageImage : ResponseMessageBase, IResponseMessageBase
    {        
        public ResponseMessageImage()
        {
            Image = new Image();
        }
        new public virtual ResponseMsgType MsgType
        {
            get { return ResponseMsgType.Image; }
        }
        public Image Image { get; set; }

    }
}

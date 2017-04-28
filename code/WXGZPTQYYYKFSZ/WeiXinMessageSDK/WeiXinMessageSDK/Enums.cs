using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senparc.Weixin.MP
{
    /// <summary>
    /// 用户发送消息类型
    /// </summary>
    public enum RequestMsgType
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        Text, 
        /// <summary>
        /// 地理位置消息
        /// </summary>
        Location, 
        /// <summary>
        /// 图片消息
        /// </summary>
        Image, 
        /// <summary>
        /// 语音消息
        /// </summary>
        Voice, 
        /// <summary>
        /// 视频消息
        /// </summary>
        Video, 
        /// <summary>
        /// 链接消息
        /// </summary>
        Link, 
        /// <summary>
        /// 事件消息
        /// </summary>
        Event, 
    }

    /// <summary>
    /// 当RequestMsgType类型为Event时，Event属性的类型
    /// </summary>
    public enum Event
    {
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        LOCATION,
        /// <summary>
        /// 关注事件
        /// </summary>
        subscribe,
        /// <summary>
        /// 关注事件
        /// </summary>
        unsubscribe,
        /// <summary>
        /// 自定义菜单点击事件
        /// </summary>
        CLICK,
        /// <summary>
        /// 扫描带参数二维码事件
        /// </summary>
        scan
    }


    /// <summary>
    /// 公众号回复消息类型
    /// </summary>
    public enum ResponseMsgType
    {
        /// <summary>
        /// 回复文本消息
        /// </summary>
        Text,
        /// <summary>
        /// 回复图文消息
        /// </summary>
        News,
        /// <summary>
        /// 回复音乐消息
        /// </summary>
        Music,
        /// <summary>
        /// 回复图片消息
        /// </summary>
        Image,
        /// <summary>
        /// 回复语音消息
        /// </summary>
        Voice,
        /// <summary>
        /// 回复视频消息
        /// </summary>
        Video
    }

    /// <summary>
    /// 菜单按钮类型
    /// </summary>
    public enum ButtonType
    {
        /// <summary>
        /// 点击
        /// </summary>
        click,
        /// <summary>
        /// Url
        /// </summary>
        view
    }
}

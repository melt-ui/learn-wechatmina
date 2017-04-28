//记录发送给朋友、分享到朋友圈等动作的默认配置，在实际记录分享动作的页面中赋以真实的配置值
var dataForWeixin = {
    appId: "xxxxxxxxx",
    MsgImg: "转发时的图片",
    TLImg: "图片",
    url: "自定义链接",
    title: "自定义标题",
    desc: "自定义描述",
    fakeid: "",
    //发送给朋友成功后的默认回调函数
    //在此回调函数中，将分享记录发送到服务器保存
    friendcallback: function () {
        alert("分享成功！");
    },
    //分享到朋友圈成功后的默认回调函数
    //在此回调函数中，将分享记录发送到服务器保存
    friendCirclecallback: function () {
        alert("分享成功！");
    }
};
(function () {
    //微信内置浏览器加载完毕后注册记录发送给朋友、分享到朋友圈等动作的事件触发函数
    var onBridgeReady = function () {
        //发送给朋友
        WeixinJSBridge.on('menu:share:appmessage', function (argv) {
            WeixinJSBridge.invoke('sendAppMessage', {
                "appid": dataForWeixin.appId,
                "img_url": dataForWeixin.MsgImg,
                "img_width": "120",
                "img_height": "120",
                "link": dataForWeixin.url,
                "desc": dataForWeixin.desc,
                "title": dataForWeixin.title
            }, function (res) {
                alert(res.err_msg);
                if (res.err_msg == "send_app_msg:ok" || res.err_msg == "send_app_msg:confirm") {
                    dataForWeixin.friendcallback();
                }
            });
        });
        //发送到朋友圈
        WeixinJSBridge.on('menu:share:timeline', function (argv) {
            //安卓系统无法触发回调，单独处理
            if (IsAndroid()) {
                dataForWeixin.friendCirclecallback();
                WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": dataForWeixin.TLImg,
                    "img_width": "120",
                    "img_height": "120",
                    "link": dataForWeixin.url,
                    "desc": dataForWeixin.desc,
                    "title": dataForWeixin.title
                }, function (res) {
                    if (res.err_msg == 'share_timeline:ok' || res.err_msg == 'share_timeline:confirm') {
                        //  dataForWeixin.friendCirclecallback();
                    }
                });
            } else {
                WeixinJSBridge.invoke('shareTimeline', {
                    "img_url": dataForWeixin.TLImg,
                    "img_width": "120",
                    "img_height": "120",
                    "link": dataForWeixin.url,
                    "desc": dataForWeixin.desc,
                    "title": dataForWeixin.title
                }, function (res) {
                    if (res.err_msg == 'share_timeline:ok' || res.err_msg == 'share_timeline:confirm') {
                        dataForWeixin.friendCirclecallback();
                    }
                });
            }

        });
        //分享到微博
        WeixinJSBridge.on('menu:share:weibo', function (argv) {
            WeixinJSBridge.invoke('shareWeibo', {
                "content": dataForWeixin.title,
                "url": dataForWeixin.url
            }, function (res) {
                if (res.err_msg == "share_weibo:ok") {
                    dataForWeixin.callback();
                }
            });
        });
        //分享到facebook
        WeixinJSBridge.on('menu:share:facebook', function (argv) {
            (dataForWeixin.callback)();
            WeixinJSBridge.invoke('shareFB', {
                "img_url": dataForWeixin.TLImg,
                "img_width": "120",
                "img_height": "120",
                "link": dataForWeixin.url,
                "desc": dataForWeixin.desc,
                "title": dataForWeixin.title
            }, function (res) { });
        });
        //判断手机操作系统是否安卓系统
        function IsAndroid() {
            var userAgentInfo = navigator.userAgent;
            if (userAgentInfo.indexOf("Android") > 0) { return true; }
            return false;
        }
    };
    //判断网页是否从微信内置浏览器打开
    //如果网页是在微信内置浏览器中打开，注册记录发送给朋友、分享到朋友圈等动作的事件触发函数
    if (typeof WeixinJSBridge == "undefined") {
        if (document.addEventListener) {
            document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);
        } else if (document.attachEvent) {
            document.attachEvent('WeixinJSBridgeReady', onBridgeReady);
            document.attachEvent('onWeixinJSBridgeReady', onBridgeReady);
        }
    }else{
        onBridgeReady();
    }
})();


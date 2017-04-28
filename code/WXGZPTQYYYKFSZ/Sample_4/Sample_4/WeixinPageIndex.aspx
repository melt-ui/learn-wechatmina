<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinPageIndex.aspx.cs" Inherits="Sample_4.WeixinPageIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>
<%-- 微信JS接口 --%>
<script src="Scripts/StatisticsWeChatAPI.js"></script>
</head>
<body>
    <%-- 所有的跳转页面，加上访问者与分享者的OpenId --%>
    <a href="WeixinPageSubPage.aspx?u=<%= ViewState["navOpenId"] as string %>&s=<%= ViewState["shareOpenId"] as string %>">WeixinPageSubPage</a>
    <form id="form1" runat="server">
    <div>    
    </div>
    </form>
</body>
</html>
<script>
    (function () {
        var url = location.href;
        dataForWeixin.appId = "";
        dataForWeixin.MsgImg = "";
        dataForWeixin.TLImg = "";
        dataForWeixin.url = url;
        dataForWeixin.title = 'Title';
        dataForWeixin.desc = 'Description';
        dataForWeixin.navOpenId = '<%= ViewState["navOpenId"] as string %>';
        dataForWeixin.shareOpenId = '<%= ViewState["shareOpenId"] as string %>';
        //转发到朋友圈的回调函数，向后台传递转发记录
        dataForWeixin.friendCirclecallback = function () {
            //AJAX请求
            $.ajax({
                type: "get",
                url: 'Share.aspx?type=timeline&url=' + encodeURIComponent(dataForWeixin.url) + "&u=" + dataForWeixin.navOpenId + "&s=" + dataForWeixin.shareOpenId,
                beforeSend: function () {
                },
                success: function () {
                },
                complete: function () {
                },
                error: function () {
                }
            });

        };
        //转发给朋友的回调函数，向后台传递转发记录
        dataForWeixin.friendcallback = function (res) {
            //AJAX请求
            $.ajax({
                type: "get",
                url: 'Share.aspx?type=friend&url=' + encodeURIComponent(dataForWeixin.url) + "&navOpenId=" + dataForWeixin.navOpenId + "&shareOpenId=" + dataForWeixin.shareOpenId,
                beforeSend: function () {
                },
                success: function () {
                },
                complete: function () {
                },
                error: function () {
                }
            });
        };
    })();
</script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Form.aspx.cs" Inherits="Sample_3.Form" %>

<!DOCTYPE html>
<html>
<head>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <!-- #手机浏览器兼容性设置 -->
    <meta content="application/xhtml+xml;charset=UTF-8" http-equiv="Content-Type">
    <meta content="no-cache,must-revalidate" http-equiv="Cache-Control">
    <meta content="no-cache" http-equiv="pragma">
    <meta content="0" http-equiv="expires">
    <meta content="telephone=no, address=no" name="format-detection">
    <meta content="width=device-width, initial-scale=1.0" name="viewport">
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <!-- #手机浏览器兼容性设置 -->
    <title>预约</title>
    <!-- #Jquery -->
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <!-- #Jquery Template -->
    <script src="Scripts/jquery.tmpl.min.js"></script> 
    <!-- #Jquery Mobile -->
    <script src="Scripts/jquery.mobile-1.3.2.min.js"></script>
    <link href="Css/jquery.mobile-1.3.2.min.css" rel="stylesheet" />
    </head>
    <body>
        <%-- 预约表单名称 --%>
        <h3><%=(ViewState["Reservation"] as Sample_3.ReservationEntity).Name%></h3>   
        <%-- 跟据预约表单设计生成用户需要填写的表单 --%>
        <form id="Form1" class="form-horizontal" type="post" runat="server">
            <input type="hidden" name="reservationID" id="reservationID" value ="<%=(ViewState["Reservation"] as Sample_3.ReservationEntity).ID%>" />
            <input type="hidden" name="openID" id="openID" value ="<%=Request.QueryString["openId"]%>" />
        <% foreach (Sample_3.ReservationContentEntity content in (ViewState["ReservationContents"] as List<Sample_3.ReservationContentEntity>))
           { %>
        <li data-role='fieldcontain'>
            <label for='content<%= content.ID%>'><%= content.Name%>：</label>
            <input type='text' placeholder='<%= content.Content%>' name='content<%= content.ID%>' id='content<%= content.ID%>' /></li>
        <% } %>
        <%-- 表单提交按钮 --%>
        <input type="submit" data-theme="b" value="提交" id="bsubmit" rel="external"/>
        </form>
    </body>
</html>


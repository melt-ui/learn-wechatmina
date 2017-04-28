<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeixinUserInfoList.aspx.cs" Inherits="Sample_5.WeixinUserInfoList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>微信用户列表</title>
</head>
<!-- #Bootstrap -->
<link href="Css/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.min.js"></script>
<!-- #Jquery -->
<script src="Scripts/jquery-1.9.1.min.js"></script>
<body>
    <div class="container">
        <form id="Form1" class="form-horizontal" type="post" runat="server">
            <h2 class="form-signin-heading">微信用户列表</h2>
            <table id="listTable" class="table table-bordered table-hover dataTable" style="width: auto">
                <thead>
                    <tr>
                        <th style="border-color: #ddd; color: Black">OpenId
                        </th>                        
                        <th style="border-color: #ddd; color: Black">头像
                        </th>                        
                        <th style="border-color: #ddd; color: Black">昵称
                        </th>
                        <th style="border-color: #ddd; color: Black">性别
                        </th>
                        <th style="border-color: #ddd; color: Black">国家
                        </th>
                        <th style="border-color: #ddd; color: Black">省
                        </th>
                        <th style="border-color: #ddd; color: Black">市
                        </th>                        
                        <th style="border-color: #ddd; color: Black">注册时间
                        </th>
                    </tr>
                </thead>
                <tbody>
                     <% foreach (Sample_5.ViewEntity.WeixinUserInfoEntity weixinUserInfo in (ViewState["WeixinUserInfoList"] as List<Sample_5.ViewEntity.WeixinUserInfoEntity>))
                        { %>
                            <tr>
                                <td><%= weixinUserInfo.OpenId%>
                                </td>              
                                <td><img width="50px" src="<%= weixinUserInfo.HeadImgUrl%>" />
                                </td>
                                <td><%= weixinUserInfo.NickName%>
                                </td>                                                  
                                <td><%= weixinUserInfo.Sex.ToString()%>
                                </td>                                                    
                                <td><%= weixinUserInfo.Country%>
                                </td>                                                  
                                <td><%= weixinUserInfo.Province%>
                                </td>                                                  
                                <td><%= weixinUserInfo.City%>
                                </td>                                                  
                                <td><%= weixinUserInfo.SubscribeTime.ToShortDateString()%>
                                </td>         
                            </tr>
                     <% } %>
                </tbody>
            </table>
        </form>
    </div>
</body>
</html>

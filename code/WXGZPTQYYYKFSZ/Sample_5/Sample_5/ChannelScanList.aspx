<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelScanList.aspx.cs" Inherits="Sample_5.ChannelScanList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>扫描记录列表</title>
</head>
<!-- #Bootstrap -->
<link href="Css/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.min.js"></script>
<!-- #Jquery -->
<script src="Scripts/jquery-1.9.1.min.js"></script>
<body>
    <div class="container">
        <form id="Form1" class="form-horizontal" type="post" runat="server">
            <h2 class="form-signin-heading">扫描记录列表</h2>
            <table id="listTable" class="table table-bordered table-hover dataTable" style="width: auto">
                <thead>
                    <tr>
                        <th style="border-color: #ddd; color: Black">ID
                        </th>
                        <th style="border-color: #ddd; color: Black">扫描类型
                        </th>
                        <th style="border-color: #ddd; color: Black">扫描时间
                        </th>
                        <th style="border-color: #ddd; color: Black">所属渠道ID
                        </th>
                        <th style="border-color: #ddd; color: Black">微信用户OpenId
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
                        <th style="border-color: #ddd; color: Black">关注时间
                        </th>
                    </tr>
                </thead>
                <tbody>
                     <% foreach (Sample_5.ViewEntity.ChannelScanDisplayEntity channelScanDisplay in (ViewState["ChannelScanDisplayList"] as List<Sample_5.ViewEntity.ChannelScanDisplayEntity>))
                        { %>
                            <tr>
                                <td><%= channelScanDisplay.ScanEntity.ID%>
                                </td>
                                <td><%= channelScanDisplay.ScanEntity.ScanType.ToString()%>
                                </td>
                                <td><%= channelScanDisplay.ScanEntity.ScanTime.ToString()%>
                                </td>
                                <td><%= channelScanDisplay.ScanEntity.ChannelId.ToString()%>
                                </td>
                                <td><%= channelScanDisplay.ScanEntity.OpenId%>
                                </td>              
                                <td><img width="50px" src="<%= channelScanDisplay.UserInfoEntity.HeadImgUrl%>" />
                                </td>
                                <td><%= channelScanDisplay.UserInfoEntity.NickName%>
                                </td>                                                  
                                <td><%= channelScanDisplay.UserInfoEntity.Sex.ToString()%>
                                </td>                                                    
                                <td><%= channelScanDisplay.UserInfoEntity.Country%>
                                </td>                                                  
                                <td><%= channelScanDisplay.UserInfoEntity.Province%>
                                </td>                                                  
                                <td><%= channelScanDisplay.UserInfoEntity.City%>
                                </td>                                                  
                                <td><%= channelScanDisplay.UserInfoEntity.SubscribeTime.ToShortDateString()%>
                                </td>         
                            </tr>
                     <% } %>
                </tbody>
            </table>
        </form>
    </div>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelList.aspx.cs" Inherits="Sample_5.ChannelList" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>渠道列表</title>
</head>
<!-- #Bootstrap -->
<link href="Css/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.min.js"></script>
<!-- #Jquery -->
<script src="Scripts/jquery-1.9.1.min.js"></script>
<body>
    <div class="container">
        <form id="Form1" class="form-horizontal" type="post" runat="server">
            <h2 class="form-signin-heading">渠道列表</h2> <a class="btnGrayS vm doaddit" href="ChannelEdit.aspx">添加</a> 
            <table id="listTable" class="table table-bordered table-hover dataTable" style="width: auto">
                <thead>
                    <tr>
                        <th style="border-color: #ddd; color: Black">ID
                        </th>
                        <th style="border-color: #ddd; color: Black">场景值ID
                        </th>
                        <th style="border-color: #ddd; color: Black">渠道名称
                        </th>
                        <th style="border-color: #ddd; color: Black">所属渠道类型ID
                        </th>
                        <th style="border-color: #ddd; color: Black">操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                     <% foreach (Sample_5.ViewEntity.ChannelEntity channel in (ViewState["ChannelList"] as List<Sample_5.ViewEntity.ChannelEntity>))
                        { %>
                            <tr>
                                <td><%= channel.ID%>
                                </td>
                                <td><%= channel.SceneId%>
                                </td>
                                <td><%= channel.Name%>
                                </td>
                                <td><%= channel.ChannelTypeId%>
                                </td>
                                <td>
                                    <p>
                                        <a class="btnGrayS vm doaddit" href="ChannelScanList.aspx?id=<%= channel.ID%>">查看扫描记录</a>
                                        <a class="btnGrayS vm doaddit" href="QRcode.aspx?id=<%= channel.ID%>">下载二维码</a>
                                        <a class="btnGrayS vm doaddit" href="ChannelEdit.aspx?id=<%= channel.ID%>">编辑</a> 
                                        <a href="javascript:void(0)" data="<%= channel.ID%>" class="dodelit deleteChannel">删除</a>
                                    </p>
                                </td>
                            </tr>
                     <% } %>
                </tbody>
            </table>
        </form>
    </div>
    <script>
        $(function(){
            $(".deleteChannel").click(function(){
                if (confirm("确定删除吗？")) {
                    var id = $(this).attr("data");
                    $.ajax({
                        url: "ChannelDelete.aspx?id=" + id,
                        type: "get",
                        success: function (repText) {
                            if (repText && repText == "True") {
                                window.location.reload();
                            } else {
                                alert(repText.ErrorMsg);
                            }
                        },
                        complete: function (xhr, ts) {
                            xhr = null;
                        }
                    });
                }
            });
        });
    </script>
</body>
</html>

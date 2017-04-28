<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelEdit.aspx.cs" Inherits="Sample_5.ChannelEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改渠道</title>
</head>
<body>
    <form id="Form1" class="form-horizontal" type="post" runat="server">
        <%if (ViewState["channel"] != null) 
        {%>
            <%-- 修改表单 --%>
            <input type="hidden" name="ID" value ="<%=(ViewState["channel"] as Sample_5.ViewEntity.ChannelEntity).ID%>" />
            <div class="control-group">
                <label class="control-label" for="Name"><strong>渠道名称</strong></label>
                <div class="controls">
                    <input type="text" name="Name" value="<%=(ViewState["channel"] as Sample_5.ViewEntity.ChannelEntity).Name%>"/>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="ChannelTypeId"><strong>所属渠道类型</strong></label>
                <div class="controls">         
                    <%-- 构造渠道类型下拉框 --%>
                    <select name="ChannelTypeId">
                    <% foreach (Sample_5.ViewEntity.ChannelTypeEntity channelType in (ViewState["ChannelTypeList"] as List<Sample_5.ViewEntity.ChannelTypeEntity>))
                    { %>
                        <%if (channelType.ID == (ViewState["channel"] as Sample_5.ViewEntity.ChannelEntity).ChannelTypeId)
                          {%>         
                        <%-- 设置渠道类型下拉框初始选中项目 --%>
                            <option value="<%=channelType.ID %>" selected="selected"><%=channelType.Name %></option>
                        <%}else{%>
                            <option value="<%=channelType.ID %>"><%=channelType.Name %></option>
                        <%}%>
                    <% } %>
                    </select>                
                </div>
            </div>
        <%}else{%>
            <%-- 新增表单 --%>
            <div class="control-group">
                <label class="control-label" for="Name"><strong>渠道名称</strong></label>
                <div class="controls">
                    <input type="text" name="Name"/>
                </div>
            </div>
            <div class="control-group">
                <label class="control-label" for="ChannelTypeId"><strong>所属渠道类型</strong></label>
                <div class="controls">         
                    <%-- 构造渠道类型下拉框 --%>
                    <select name="ChannelTypeId">
                    <% foreach (Sample_5.ViewEntity.ChannelTypeEntity channelType in (ViewState["ChannelTypeList"] as List<Sample_5.ViewEntity.ChannelTypeEntity>))
                    { %>
                            <option value="<%=channelType.ID %>"><%=channelType.Name %></option>
                    <% } %>
                    </select>                
                </div>
            </div>
        <%}%>
        <%-- 提交按钮 --%>
        <button class="btn btn-large btn-primary" type="submit">保存</button>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChannelTypeEdit.aspx.cs" Inherits="Sample_5.ChannelTypeEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改渠道类型</title>
</head>
<body>
    <form id="Form1" class="form-horizontal" type="post" runat="server">
        <%if (ViewState["channelType"] != null) 
        {%>
            <%-- 修改表单 --%>
            <input type="hidden" name="ID" value ="<%=(ViewState["channelType"] as Sample_5.ViewEntity.ChannelTypeEntity).ID%>" />
            <div class="control-group">
                <label class="control-label" for="Name"><strong>渠道类型名称</strong></label>
                <div class="controls">
                    <input type="text" name="Name" value="<%=(ViewState["channelType"] as Sample_5.ViewEntity.ChannelTypeEntity).Name%>"/>
                </div>
            </div>
        <%}else{%>
            <%-- 新增表单 --%>
            <div class="control-group">
                <label class="control-label" for="Name"><strong>渠道类型名称</strong></label>
                <div class="controls">
                    <input type="text" name="Name"/>
                </div>
            </div>
        <%}%>
        <%-- 提交按钮 --%>
        <button class="btn btn-large btn-primary" type="submit">保存</button>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="Sample_3.Admin" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>预约表单设计</title>
</head>
<!-- #Bootstrap -->
<link href="Css/bootstrap.min.css" rel="stylesheet" />
<script src="Scripts/bootstrap.min.js"></script>
<!-- #Jquery -->
<script src="Scripts/jquery-1.9.1.min.js"></script>
<!-- #Jquery Template -->
<script src="Scripts/jquery.tmpl.min.js"></script>
<body>
    <div class="container">
        <form class="form-horizontal" type="post" runat="server">
            <input type="hidden" id="lineCount" name="lineCount" value="">
            <h2 class="form-signin-heading">预约表单设计</h2>
            <%-- 表单名称输入框 --%>
            <div class="control-group">
                <label class="control-label" for="formName"><strong>表单名称</strong></label>
                <div class="controls">
                    <input type="text" name="formName" value=""/>
                </div>
            </div>
            <%-- 表单字段设计部分 --%>
            <table id="listTable" class="table table-bordered table-hover dataTable" style="width: auto">
                <thead>
                    <tr>
                        <th style="border-color: #ddd; color: Black">字段名
                        </th>
                        <th style="border-color: #ddd; color: Black">字段名称
                        </th>
                        <th style="border-color: #ddd; color: Black">初始内容
                        </th>
                        <th style="border-color: #ddd; color: Black">字段类型
                        </th>
                        <th style="border-color: #ddd; color: Black">操作
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
            <%-- 提交按钮 --%>
            <button class="btn btn-large btn-primary" type="submit">保存</button>
        </form>
    </div>
</body>
<%-- 一行表单字段模板 --%>
<script type="text/x-jquery-tmpl" id="replyOrderlist">
    <tr>
        <td>字段：
        </td>
        <td>
            <input type="text" name="Name${Index}" onkeyup="value=value.substring(0,20)" value="">
        </td>
        <td>
            <input name="Content${Index}" type="text" onkeyup="value=value.substring(0,500)" value="">
        </td>
        <td>文本框
        </td>
        <td>
            <p>
                <a class="btnGrayS vm doaddit" href="javascript:void(0)">添加</a> <a href='javascript:void(0)' class='dodelit'>删除</a>
            </p>
        </td>
    </tr>
</script>
<script type="text/javascript">
    var count = 0;
    //生成一行表单字段
    function createLine() {
        var line = {
            Index: count
        };
        count++;
        $("#lineCount").val(count);
        return $("#replyOrderlist").tmpl(line);
    }
    //页面加载完毕初始化第一行
    $(function () {
        createLine().appendTo("#listTable");
    });
    //注册删除按钮事件
    $(document).on("click", ".dodelit", function () {
        $(this).parent().parent().parent().remove();
    });
    //注册添加按钮事件
    $(document).on("click", ".doaddit", function () {
        $(this).parent().parent().parent().after(createLine());
    });
</script>
</html>

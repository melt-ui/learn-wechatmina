<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="Sample_4.Statistics1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>统计</title>
    <%-- Bootstrap --%>
    <link href="Css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-1.9.1.min.js" type="text/javascript"></script>
    <%-- HighCharts用于图表显示 --%>
    <link href="Css/highcharts/charts.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/HighCharts/highcharts.js" type="text/javascript"></script>
    <script src="Scripts/HighCharts/highcharts-more.js" type="text/javascript"></script>
    <script src="Scripts/HighCharts/publiclinecharts.js" type="text/javascript"></script>
</head>
<body>
    <div class="row-fluid">
        <div class="span12">
            <h3>访问记录</h3>
            <%-- 访问记录统计图 --%>
            <div class="box">
                <div class="box-content">
                    <div class="row" style="margin-top: 30px; margin-right: 15px;">
                        <div class="area">
                            <div id="page-nav-chart">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <%-- 访问记录列表 --%>
            <div class="maincontentinner1" style="margin-left: 20px">
                    <div id="Div12" class="dataTables_wrapper">
                        <table id="page-nav-table" class="table table-bordered responsive dataTable">                           
                            <%-- 访问记录列表列名 --%>
                            <thead>
                                <tr>
                                    <th>
                                        页面地址
                                    </th>
                                    <th>
                                        访问来源
                                    </th>
                                    <th>
                                        访问者openid
                                    </th>
                                    <th>
                                        分享自openid
                                    </th>
                                    <th>
                                        访问时间
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="page-nav-table-body">
                                <%-- 一行一行生成访问记录列表 --%>
                                <% foreach (Sample_4.PageNavEntity entity in (ViewState["NavList"] as List<Sample_4.PageNavEntity>))
                                   { %>
                                    <tr class="gradeX odd">
                                        <td>
                                            <%= entity.Url%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.From.ToString()%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.NavOpenId%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.ShareOpenId%>
                                        </td>
                                        <td class=" ">
                                            <%= entity.VisitTime.ToString()%>
                                        </td>
                                    </tr>
                                <% } %>
                            </tbody>
                        </table>
                    </div>
            </div>
        </div>
    </div>
    <script>
        //图表参数
        var pageNavChartOpts={
            getStatisticsUrl:'Data.aspx?type=navChart', //读取数据的访问地址
            titletext:"",
            ytext:"",
            startyear: 0,
            startmonth:0,
            startday:0,
            lineinterval: 3600 * 1000, //竖线以1小时为间隔显示
            pointInterval: 3600 * 1000,//点以1小时为间隔显示
            countArray:[],
            formid:"page-nav-chart", //图表容器ID
            seriesname:"访问次数",
            unit:"次"
        };
        jQuery(function () {
            //使用HighCharts绘制图表
            highcharts.extFunction.PreDrawMethod = function (repJson) {
                pageNavChartOpts.startyear = repJson.StartYear;
                pageNavChartOpts.startmonth = repJson.StartMonth;
                pageNavChartOpts.startday = repJson.StartDay;
                highcharts.displayMode = repJson.DisplayMode;
                pageNavChartOpts.lineinterval = repJson.LineInterval;
                pageNavChartOpts.pointInterval = repJson.PointInterval;
            };
            highcharts.init(pageNavChartOpts);
        });
    </script>
</body>
</html>

var highcharts = {
    opts: {
        getStatisticsUrl: "", //获取图标数据的action地址
        titletext: "", //标题
        ytext: "", //y轴显示文本
        startyear: 0, //开始年份
        startmonth: 0, //开始月份
        startday: 0, //开始天数
        lineinterval: 0, //以多少天为间隔显示一条竖线(毫秒)
        pointInterval: 0, //显示图表数据点的间隔(毫秒)
        countArray: [], //图表的数据
        formid: "", //需要填充的div的id
        seriesname: "", //显示数据列的名称
        unit: ""
    },
    displayMode: 0,
    init: function (opts) {
        highcharts.getStatistics(opts);
    },
    //获取图的数据
    getStatistics: function (opts) {
        jQuery.ajax({
            url: opts.getStatisticsUrl,
            type: "get",
            dataType: "json",
            data: { plat: opts.plat },
            success: function (repJson) {
                if (repJson) {
                    highcharts.extFunction.PreDrawMethod(repJson);
                    opts.countArray.push(highcharts.getSeries(opts, opts.seriesname, repJson.Statistics));
                    highcharts.drawChart(opts);
                }
            },
            complete: function (xhr, ts) {
                xhr = null;
            }
        });
    },
    //画图
    drawChart: function (opts) {
        var chart = new Highcharts.Chart({
            chart: {
                renderTo: opts.formid,
                type: 'line',
                marginRight: 30,
                marginBottom: 25
            },
            title: {
                text: opts.titletext,
                align: 'left'
            },
            xAxis: {
                type: 'datetime',
                labels: {
                    formatter: function () {
                        return highcharts.getFormatDate2(this.value);
                    },
                    style: {
                        color: '#2f7ed8'
                    }
                },
                gridLineWidth: 1,
                tickInterval: opts.lineinterval// one week
            },
            yAxis: {
                title: {
                    text: opts.ytext
                },
                plotLines: [{
                    value: 0,
                    width: 1,
                    color: '#808080'
                }],
                min: 0
            },
            tooltip: {
                formatter: function () {
                        return highcharts.getFormatDate2(this.x) + ': <span style="color:red">' + this.y + '<span>' + opts.unit;
                }
            },
            credits: {
                enabled: false//不显示highCharts版权信息
            },
            legend: {
                enabled: true,
                align: 'right',
                verticalAlign: 'top',
                floating: true,
                y: 0,
                borderWidth: 0,
                margin: 0,
                symbolPadding: 5,
                symbolWidth: 12,
                itemStyle: {
                    color: '#636363'
                }
            },
            series: opts.countArray
        });
    },
    getSeries: function (opts, seriesname, statistics) {
        var data = {
            name: seriesname,
            data: statistics,
            pointStart: Date.UTC(opts.startyear, opts.startmonth - 1, opts.startday),
            pointInterval: opts.pointInterval
        };
        return data;
    },
    ///格式化时间
    getFormatDate2: function (v) {
        var d = new Date(v);
        d.setHours(d.getHours() - 8);
        return d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate() + " " + d.getHours() + "点";
    },
    extFunction: {
        //画图前预处理方法
        PreDrawMethod: function (repJson) {
        }
    }
}

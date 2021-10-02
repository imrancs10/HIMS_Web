/// <reference path="../Global/Utility.js" />
'use strict';

$(document).ready(function () {
    $('.navbar-nav li.active').removeClass('active');
    $('[href="/Admin/Dashboard"]').parent().addClass('active');
    getChartDetail();

    $('#spanPreviousChart').click(function () {
        var days = 7;

        var dateFormat = $('#spanLastDayOfWeek').text();
        var dateParts = dateFormat.split("/");
        var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

        var date = new Date(dateObject);
        var last = new Date(date.getTime() - (days * 24 * 60 * 60 * 1000));
        var day = last.getDate();
        var month = last.getMonth() + 1;

        var lastDayofWeek = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' +
            date.getFullYear();

        getChartDetail(lastDayofWeek);
    });

    $('#spanNextChart').click(function () {
        var days = 7;

        var dateFormat = $('#spanLastDayOfWeek').text();
        var dateParts = dateFormat.split("/");
        var dateObject = new Date(+dateParts[2], dateParts[1] - 1, +dateParts[0]);

        var date = new Date(dateObject);
        var last = new Date(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var day = last.getDate();
        var month = last.getMonth() + 1;

        var lastDayofWeek = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' +
            date.getFullYear();

        getChartDetail(lastDayofWeek);
    });

});
function getChartDetail(reportStartDate) {
    var model = {
        reportStartDate: (reportStartDate == undefined ? null : reportStartDate)
    }
    $.ajax({
        type: "POST",
        data: JSON.stringify(model),
        url: "/Admin/BarChart",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (reportStartDate != null && reportStartDate != undefined) {
                setFirstDate(reportStartDate);
                setLastDate(reportStartDate);
            }
            else {
                setFirstDate();
                setLastDate();
            }
            successFunc(response);
        },
    });
}

function setFirstDate(sentDate) {
    if (sentDate != null && sentDate != undefined) {
        var d = new Date(sentDate);
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var currentDate = (('' + day).length < 2 ? '0' : '') + day + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            d.getFullYear();
        $('#spanLastDayOfWeek').text(currentDate)
    }
    else {
        var days = 6; // Days you want to subtract
        var date = new Date();
        var last = new Date(date.getTime() - (days * 24 * 60 * 60 * 1000));
        var day = last.getDate();
        var month = last.getMonth() + 1;

        var lastDayofWeek = (('' + day).length < 2 ? '0' : '') + day + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            date.getFullYear();
        $('#spanLastDayOfWeek').text(lastDayofWeek)
    }
}

function setLastDate(sentDate) {
    if (sentDate != null && sentDate != undefined) {
        var days = 6; // Days you want to subtract
        var date = new Date(sentDate);
        var last = new Date(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var day = last.getDate();
        var month = last.getMonth() + 1;

        var lastDayofWeek = (('' + day).length < 2 ? '0' : '') + day + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            date.getFullYear();
        $('#spanCurrentDayOfWeek').text(lastDayofWeek);
    }
    else {
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var currentDate = (('' + day).length < 2 ? '0' : '') + day + '/' +
            (('' + month).length < 2 ? '0' : '') + month + '/' +
            d.getFullYear();
        $('#spanCurrentDayOfWeek').text(currentDate);
    }
}

function successFunc(jsondata) {
    var MonthYr = [];
    jsondata.forEach(function (e) {
        MonthYr.push(e.MonthYr);
    })
    var chart = c3.generate({
        bindto: '#Barchart',
        data: {
            json: jsondata,
            keys: {
                value: ['Admit', 'Discharge', 'Refer'],
            },
            columns: ['Admit', 'Discharge', 'Refer'],
            type: 'bar',
            groups: [
                ['Admit', 'Discharge', 'Refer']
            ],
            types: {
                Male: 'area',
                Female: 'area-spline'
            },
        },
        axis: {
            x: {
                type: 'category',
                categories: MonthYr,
                tick: {
                    rotate: 50
                },
            }
        },
        color: {
            pattern: ['#1f77b4', '#aec7e8', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
        },
    });
}
/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {

    utility.restrictFutureDate('startDate');
    utility.restrictFutureDate('endDate');

    $('#searchPatient').click(function () {
        var IDPNumber = $('#ipdNumber').val();
        var PatientName = $('#patientName').val();
        var StartDate = $('#startDate').val();
        var EndDate = $('#endDate').val();
        var IPDStatus = $('#IPDStatus').val();
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{IDPNumber: "' + IDPNumber + '",PatientName: "' + PatientName + '",StartDate: "' + StartDate + '",EndDate: "' + EndDate + '" ,IPDStatus: "' + IPDStatus + '" }',
            url: '/Admin/GetIPDPatientDetail',
            success: function (data) {
                var rowHtml = "";
                $('#tbodyData tr').remove();
                var index = 1;
                $.each(data, function (key, entry) {
                    rowHtml += '<tr>' +
                        '<td class="text-center" > ' + index + '</td> ' +
                        '<td class="text-center">' + entry.IpdNo + '</td>' +
                        '<td class="text-center">' + entry.PatientName + '</td>' +
                        '<td class="text-center">' + entry.FatherOrHusbandName + '</td>' +
                        '<td class="text-center">' + entry.MobileNumber + '</td>' +
                        '<td class="text-center">' + entry.Age_Year + '</td>' +
                        '<td class="text-center">' + entry.Age_Month + '</td>' +
                        '<td class="text-center">' + entry.AdmittedDateTime + '</td>' +
                        '<td class="text-center">' + entry.TreatmentName + '</td>' +
                        '<td class="text-center">' + entry.DepartmentName + '</td>' +
                        '<td class="text-center">' + entry.AreaName + '</td>' +
                        '<td class="text-center">' + entry.IPDStatus + '</td>' +
                        '<td class="text-center">' + entry.IPDStatusDate + '</td>' +
                        '<td class="text-center">' + entry.MalariaStatus + '</td>' +
                        '<td class="text-center">' + entry.MalariaParasite_TestDate + '</td>' +
                        '<td class="text-center">' + entry.RapidKitNS1Status + '</td>' +
                        '<td class="text-center">' + entry.RapidKitNS1_TestDate + '</td>' +
                        '<td class="text-center">' + entry.RapidKitIGMStatus + '</td>' +
                        '<td class="text-center">' + entry.RapidKitIGM_TestDate + '</td>' +
                        '<td class="text-center">' + entry.ELISANS1Status + '</td>' +
                        '<td class="text-center">' + entry.ELISANS1_TestDate + '</td>' +
                        '<td class="text-center">' + entry.ELISAIGMStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISAIGM_TestDate + '</td>' +
                        '<td class="text-center">' + entry.ELISAScrubTyphusStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISAScrubTyphus_TestDate + '</td>' +
                        '<td class="text-center">' + entry.ELISALeptospiraStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISALaptospira_TestDate + '</td>' +

                        '<td class="text-center">' + entry.HbCount + '</td>' +
                        '<td class="text-center">' + entry.PlateletCount + '</td>' +
                        '<td class="text-center">' + entry.LFT_Details + '</td>' +
                        '<td class="text-center">' + entry.KFT_Details + '</td>' +
                        '<td class="text-center">' + entry.RandomDonerPlatelet_Count + '</td>' +
                        '<td class="text-center">' + entry.RandomDonerPlatelet_TestDate + '</td>' +
                        '<td class="text-center">' + entry.WholeBloodCell_Count + '</td>' +
                        '<td class="text-center">' + entry.WholeBloodCell_TestDate + '</td>' +
                        '<td class="text-center">' + entry.PackedRBC_Count + '</td>' +
                        '<td class="text-center">' + entry.PackedRBC_TestDate + '</td>' +
                        '<td class="text-center">' + entry.Xray_Details + '</td>' +
                        '<td class="text-center">' + entry.USG_Details + '</td>' +
                        '</tr>';
                    index++;
                });
                if (data.length == 0) {
                    rowHtml += '<tr>' +
                        '<td colspan="20">No Record Found.</td>' +
                        '</tr>';
                }
                $('#tbodyData').append(rowHtml);
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });

    $('#exportSearchPatient').click(function () {
        var IDPNumber = $('#ipdNumber').val();
        var PatientName = $('#patientName').val();
        var StartDate = $('#startDate').val();
        var EndDate = $('#endDate').val();
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{IPDNo: "' + IDPNumber + '",PatientName: "' + PatientName + '",StartDate: "' + StartDate + '",EndDate: "' + EndDate + '" }',
            url: '/Admin/IPDSearchExportToExcel',
            success: function (data) {
   
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
})

function formatDate(dateNow) {
    var milli = dateNow.replace(/\/Date\((-?\d+)\)\//, '$1');
    var now = new Date(parseInt(milli));
    var day = ("0" + now.getDate()).slice(-2);
    var month = ("0" + (now.getMonth() + 1)).slice(-2);
    var today = now.getFullYear() + "-" + (month) + "-" + (day);
    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
    return now.toISOString().slice(0, 16);
}
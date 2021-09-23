/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    $('#searchPatient').click(function () {
        var searchText = $('#searchText').val();
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{searchText: "' + searchText + '" }',
            url: '/Admin/GetPatientDetail',
            success: function (data) {
                var rowHtml = "";
                $('#tbodyData tr').remove();
                $.each(data, function (key, entry) {
                    rowHtml += '<tr>' +
                        '<td class="text-center"><input type="radio" id="selectPatient" name="selectPatient" data-Id="' + entry.PatientId + '" name="" /></td>' +
                        '<td class="text-center">' + entry.IpdNo + '</td>' +
                        '<td class="text-center">' + entry.PatientName + '</td>' +
                        '<td class="text-center">' + entry.FatherOrHusbandName + '</td>' +
                        '<td class="text-center">' + entry.MobileNumber + '</td>' +
                        '<td class="text-center">' + entry.Age + '</td>' +
                        '<td class="text-center">' + entry.AdmittedDateTime + '</td>' +
                        '<td class="text-center">' + entry.TreatmentName + '</td>' +
                        '<td class="text-center">' + entry.DepartmentName + '</td>' +
                        '<td class="text-center">' + entry.AreaName + '</td>' +
                        '<td class="text-center">' + entry.IPDStatus + '</td>' +
                        '</tr>';
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

    $('#saveLabReport').click(function () {
        var model = {
            PatientId: $('#patientId').val(),
            Hb: $('#Hb').val(),
            Platelet: $('#Platelet').val(),
            MalariaStatus: $('#Malaria').val(),
            malariadate: $('#malariadate').val(),
            RapidKitNS1Status: $('#RapidKitNS1Status').val(),
            RapidKitNS1Date: $('#RapidKitNS1Date').val(),
            RapidKitIGMStatus: $('#RapidKitIGMStatus').val(),
            RapidKitIGMDate: $('#RapidKitIGMDate').val(),
            ELISANS1Status: $('#ELISANS1Status').val(),
            ELISANS1Date: $('#ELISANS1Date').val(),
            ELISAIGMStatus: $('#ELISAIGMStatus').val(),
            ELISAIGMDate: $('#ELISAIGMDate').val(),
            ELISAScrubTyphusStatus: $('#ELISAScrubTyphusStatus').val(),
            ELISAScrubTyphusDate: $('#ELISAScrubTyphusDate').val(),
            ELISALeptospiraStatus: $('#ELISALeptospiraStatus').val(),
            ELISALeptospiraDate: $('#ELISALeptospiraDate').val(),
            LFT: $('#LFT').val(),
            kft: $('#kft').val(),
            BTRandomDonerPlatelet: $('#BTRandomDonerPlatelet').val(),
            BTRandomDonerPlateletDate: $('#BTRandomDonerPlateletDate').val(),
            BTSingleDonerPlatelet: $('#BTSingleDonerPlatelet').val(),
            BTSingleDonerPlateletDate: $('#BTSingleDonerPlateletDate').val(),
            WholeBloodCell: $('#WholeBloodCell').val(),
            WholeBloodCellDate: $('#WholeBloodCellDate').val(),
            PackedRBC: $('#PackedRBC').val(),
            PackedRBCDate: $('#PackedRBCDate').val(),
        }

        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(model),
            url: '/Admin/SaveIPDLabReport',
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

$(document).on('click', '#selectPatient', function () {
    debugger
    var patientId = $(this).attr('data-Id');
    if (patientId > 0) {
        $('#divReport').removeClass('hidden');
        $('[name*=patientId]').val(patientId);
    }
})



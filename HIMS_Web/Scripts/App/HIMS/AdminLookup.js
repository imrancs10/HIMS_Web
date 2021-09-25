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
                        '<td class="text-center">' + entry.MalariaStatus + '</td>' +
                        '<td class="text-center">' + entry.RapidKitNS1Status + '</td>' +
                        '<td class="text-center">' + entry.RapidKitIGMStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISANS1Status + '</td>' +
                        '<td class="text-center">' + entry.ELISAIGMStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISAScrubTyphusStatus + '</td>' +
                        '<td class="text-center">' + entry.ELISALeptospiraStatus + '</td>' +
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
                if (data == "Save") {
                    utility.alert.setAlert("IPD Lab Report", "IPD Lab Report Saved");
                    $('#Hb').val("");
                    $('#Platelet').val("");
                    $('#Malaria').val("");
                    $('#malariadate').val("");
                    $('#RapidKitNS1Status').val("");
                    $('#RapidKitNS1Date').val("");
                    $('#RapidKitIGMStatus').val("");
                    $('#RapidKitIGMDate').val("");
                    $('#ELISANS1Status').val("");
                    $('#ELISANS1Date').val("");
                    $('#ELISAIGMStatus').val("");
                    $('#ELISAIGMDate').val("");
                    $('#ELISAScrubTyphusStatus').val("");
                    $('#ELISAScrubTyphusDate').val("");
                    $('#ELISALeptospiraStatus').val("");
                    $('#ELISALeptospiraDate').val("");
                    $('#LFT').val("");
                    $('#kft').val("");
                    $('#BTRandomDonerPlatelet').val("");
                    $('#BTRandomDonerPlateletDate').val("");
                    $('#BTSingleDonerPlatelet').val("");
                    $('#BTSingleDonerPlateletDate').val("");
                    $('#WholeBloodCell').val("");
                    $('#WholeBloodCellDate').val("");
                    $('#PackedRBC').val("");
                    $('#PackedRBCDate').val("");
                    $('#searchPatient').click();
                }
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });

    $('#saveDiagnosisReport').click(function () {
        var model = {
            PatientId: $('#patientId').val(),
            Xray_Details: $('#xray').val(),
            USG_Details: $('#USG').val(),
        }
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(model),
            url: '/Admin/SaveDiagnosisReport',
            success: function (data) {
                if (data == "Save") {
                    utility.alert.setAlert("Diagnosis Report", "Diagnosis Report Saved");
                    $('#xray').val("");
                    $('#USG').val("");
                    $('#searchPatient').click();
                }
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });

    $('#saveIPDStatus').click(function () {
        var model = {
            PatientId: $('#patientId').val(),
            IPDStatus: $('#IPDStatus').val(),
            AdmittedDateTime: $('#AdmittedDateTime').val(),
            Reason: $('#Reason').val(),
            casesummary: $('#casesummary').val()
        }
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: JSON.stringify(model),
            url: '/Admin/SaveIPDStatus',
            success: function (data) {
                if (data == "Save") {
                    utility.alert.setAlert("IPD Status", "IPD Status Updated");
                    $('#IPDStatus').val("");
                    $('#AdmittedDateTime').val("");
                    $('#Reason').val("");
                    $('#casesummary').val("");
                    $('#searchPatient').click();
                }
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });

    $("#IPDStatus").change(function () {
        if ($(this).val() == "Admit") {
            $("#Reason").parent().parent().hide();
            $("#casesummary").parent().parent().hide();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "Discharge") {
            $("#Reason").parent().parent().hide();
            $("#casesummary").parent().parent().hide();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "Refer") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().show();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "LAMA") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().show();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "DOPR") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().show();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "Death") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().show();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "Abscond") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().show();
            $("#AdmittedDateTime").parent().parent().show();
        }
        else if ($(this).val() == "Other") {
            $("#Reason").parent().parent().show();
            $("#casesummary").parent().parent().hide();
            $("#AdmittedDateTime").parent().parent().show();
        }
    });
})



$(document).on('click', '#selectPatient', function () {
    var patientId = $(this).attr('data-Id');
    if (patientId > 0) {
        $('#divReport').removeClass('hidden');
        $('[name*=patientId]').val(patientId);
    }
})



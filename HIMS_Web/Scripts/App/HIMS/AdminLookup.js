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
                    if ($('#patientId').val() != "" && entry.PatientId == $('#patientId').val()) {
                        rowHtml += '<tr>' +
                            '<td class="text-center"><input type="radio" checked="checked" id="selectPatient" name="selectPatient" data-status="' + entry.IPDStatus + '" data-Id="' + entry.PatientId + '" name="" /></td>' +
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
                            '<td class="text-center">' + entry.IPDStatusDate + '</td>' +
                            '<td class="text-center">' + entry.MalariaStatus + '</td>' +
                            '<td class="text-center">' + entry.RapidKitNS1Status + '</td>' +
                            '<td class="text-center">' + entry.RapidKitIGMStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISANS1Status + '</td>' +
                            '<td class="text-center">' + entry.ELISAIGMStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISAScrubTyphusStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISALeptospiraStatus + '</td>' +
                            '</tr>';
                    }
                    else {
                        rowHtml += '<tr>' +
                            '<td class="text-center"><input type="radio" id="selectPatient" name="selectPatient" data-status="' + entry.IPDStatus + '" data-Id="' + entry.PatientId + '" name="" /></td>' +
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
                            '<td class="text-center">' + entry.IPDStatusDate + '</td>' +
                            '<td class="text-center">' + entry.MalariaStatus + '</td>' +
                            '<td class="text-center">' + entry.RapidKitNS1Status + '</td>' +
                            '<td class="text-center">' + entry.RapidKitIGMStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISANS1Status + '</td>' +
                            '<td class="text-center">' + entry.ELISAIGMStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISAScrubTyphusStatus + '</td>' +
                            '<td class="text-center">' + entry.ELISALeptospiraStatus + '</td>' +
                            '</tr>';
                    }

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

                    $('#searchPatient').click();
                    //$('#LabReportTab').removeClass('ui-tabs-active ui-state-active');
                    //$('#RadioDiagnosisTab').addClass('ui-tabs-active ui-state-active');
                    //$('#tabs-2').css('display', 'none');
                    //$('#tabs-3').css('display', 'block');
                    //$('#divDischarge').addClass('hidden');
                    //$('#divReport').addClass('hidden');
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

                    $('#searchPatient').click();
                    //$('#RadioDiagnosisTab').removeClass('ui-tabs-active ui-state-active');
                    //$('#StatusTab').addClass('ui-tabs-active ui-state-active');
                    //$('#tabs-3').css('display', 'none');
                    //$('#tabs-4').css('display', 'block');
                    //$('#divDischarge').addClass('hidden');
                    //$('#divReport').addClass('hidden');
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

                    $('#searchPatient').click();
                    //$('#divDischarge').addClass('hidden');
                    //$('#divReport').addClass('hidden');
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
    var status = $(this).attr('data-status');
    if (status == 'Discharge' || status == 'Death' || status == 'Refer' || status == 'Abscond') {
        $('#divDischarge').removeClass('hidden');
        $('#divReport').addClass('hidden');
    }
    else if (patientId > 0) {
        $.ajax({
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            type: 'POST',
            data: '{patientId: ' + patientId + '}',
            url: '/Admin/GetPatientDetailByPatientId',
            success: function (data) {
                resetLabReport();
                resetDiagnosisReport();
                resetIpdStatus();
                if (data.LabReport != null) {
                    $('#Hb').val(data.LabReport.HbCount);
                    $('#Platelet').val(data.LabReport.PlateletCount);
                    $('#Malaria').val(data.LabReport.MalariaParasite_Status);
                    if (data.LabReport.MalariaParasite_TestDate != null) {
                        $('#malariadate').val(formatDate(data.LabReport.MalariaParasite_TestDate));
                    }
                    $('#RapidKitNS1Status').val(data.LabReport.RapidKitNS1_Status);
                    //$('#RapidKitNS1Date').val(data.LabReport.HbCount);
                    if (data.LabReport.RapidKitNS1_TestDate != null) {
                        $('#RapidKitNS1Date').val(formatDate(data.LabReport.RapidKitNS1_TestDate));
                    }
                    $('#RapidKitIGMStatus').val(data.LabReport.RapidKitIGM_Status);
                    //$('#RapidKitIGMDate').val(data.LabReport.HbCount);
                    if (data.LabReport.RapidKitIGM_TestDate != null) {
                        $('#RapidKitIGMDate').val(formatDate(data.LabReport.RapidKitIGM_TestDate));
                    }
                    $('#ELISANS1Status').val(data.LabReport.ELISANS1_Status);
                    //$('#ELISANS1Date').val(data.LabReport.HbCount);
                    if (data.LabReport.ELISANS1_TestDate != null) {
                        $('#ELISANS1Date').val(formatDate(data.LabReport.ELISANS1_TestDate));
                    }
                    $('#ELISAIGMStatus').val(data.LabReport.ELISAIGM_Status);
                    //$('#ELISAIGMDate').val(data.LabReport.HbCount);
                    if (data.LabReport.ELISAIGM_TestDate != null) {
                        $('#ELISAIGMDate').val(formatDate(data.LabReport.ELISAIGM_TestDate));
                    }
                    $('#ELISAScrubTyphusStatus').val(data.LabReport.ELISAScrubTyphus_Status);
                    //$('#ELISAScrubTyphusDate').val(data.LabReport.HbCount);
                    if (data.LabReport.ELISAScrubTyphus_TestDate != null) {
                        $('#ELISAScrubTyphusDate').val(formatDate(data.LabReport.ELISAScrubTyphus_TestDate));
                    }
                    $('#ELISALeptospiraStatus').val(data.LabReport.ELISALaptospira_Status);
                    //$('#ELISALeptospiraDate').val(data.LabReport.HbCount);
                    if (data.LabReport.ELISALaptospira_TestDate != null) {
                        $('#ELISALeptospiraDate').val(formatDate(data.LabReport.ELISALaptospira_TestDate));
                    }
                    $('#LFT').val(data.LabReport.LFT_Details);
                    $('#kft').val(data.LabReport.KFT_Details);
                    $('#BTRandomDonerPlatelet').val(data.LabReport.RandomDonerPlatelet_Count);
                    //$('#BTRandomDonerPlateletDate').val(data.LabReport.HbCount);
                    if (data.LabReport.RandomDonerPlatelet_TestDate != null) {
                        $('#BTRandomDonerPlateletDate').val(formatDate(data.LabReport.RandomDonerPlatelet_TestDate));
                    }
                    $('#BTSingleDonerPlatelet').val(data.LabReport.SingleDonorPlatelet_Count);
                    //$('#BTSingleDonerPlateletDate').val(data.LabReport.HbCount);
                    if (data.LabReport.SingleDonorPlatelet_TestDate != null) {
                        $('#BTSingleDonerPlateletDate').val(formatDate(data.LabReport.SingleDonorPlatelet_TestDate));
                    }
                    $('#WholeBloodCell').val(data.LabReport.WholeBloodCell_Count);
                    //$('#WholeBloodCellDate').val(data.LabReport.HbCount);
                    if (data.LabReport.WholeBloodCell_TestDate != null) {
                        $('#WholeBloodCellDate').val(formatDate(data.LabReport.WholeBloodCell_TestDate));
                    }
                    $('#PackedRBC').val(data.LabReport.PackedRBC_Count);
                    //$('#PackedRBCDate').val(data.LabReport.HbCount);
                    if (data.LabReport.PackedRBC_TestDate != null) {
                        $('#PackedRBCDate').val(formatDate(data.LabReport.PackedRBC_TestDate));
                    }
                }

                if (data.DiagnosisReport != null) {
                    $('#xray').val(data.DiagnosisReport.Xray_Details);
                    $('#USG').val(data.DiagnosisReport.USG_Details);
                }

                if (data.PatientStatus != null) {
                    $('#IPDStatus').val(data.PatientStatus.IPDStatus);

                    if (data.PatientStatus.IPDStatus == "Admit") {
                        if (data.PatientStatus.AdmittedDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.AdmittedDateTime));
                        $("#Reason").parent().parent().hide();
                        $("#casesummary").parent().parent().hide();
                    }
                    else if (data.PatientStatus.IPDStatus == "Discharge") {
                        if (data.PatientStatus.DischargeDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.DischargeDateTime));
                        $("#Reason").parent().parent().hide();
                        $("#casesummary").parent().parent().hide();
                    }
                    else if (data.PatientStatus.IPDStatus == "Refer") {
                        if (data.PatientStatus.ReferDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.ReferDateTime));
                        $('#Reason').val(data.PatientStatus.ReferReasons);
                        $('#casesummary').val(data.PatientStatus.ReferCaseSummary);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().show();
                    }
                    else if (data.PatientStatus.IPDStatus == "LAMA") {
                        if (data.PatientStatus.LAMADateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.LAMADateTime));
                        $('#Reason').val(data.PatientStatus.LAMAReasons);
                        $('#casesummary').val(data.PatientStatus.LAMACaseSummary);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().show();
                    }
                    else if (data.PatientStatus.IPDStatus == "DOPR") {
                        if (data.PatientStatus.DOPRDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.DOPRDateTime));
                        $('#Reason').val(data.PatientStatus.DOPRReasons);
                        $('#casesummary').val(data.PatientStatus.DOPRCaseSummary);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().show();
                    }
                    else if (data.PatientStatus.IPDStatus == "Death") {
                        if (data.PatientStatus.DeathDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.DeathDateTime));
                        $('#Reason').val(data.PatientStatus.DeathReasons);
                        $('#casesummary').val(data.PatientStatus.DeathCaseSummary);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().show();
                    }
                    else if (data.PatientStatus.IPDStatus == "Abscond") {
                        if (data.PatientStatus.AbscondDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.AbscondDateTime));
                        $('#Reason').val(data.PatientStatus.AbscondReasons);
                        $('#casesummary').val(data.PatientStatus.AbscondCaseSummary);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().show();
                    }
                    else if (data.PatientStatus.IPDStatus == "Other") {
                        if (data.PatientStatus.OtherDateTime != null)
                            $('#AdmittedDateTime').val(formatDate(data.PatientStatus.OtherDateTime));
                        $('#Reason').val(data.PatientStatus.OtherReasons);
                        $("#Reason").parent().parent().show();
                        $("#casesummary").parent().parent().hide();
                    }
                }

                $('#divDischarge').addClass('hidden');
                $('#divReport').removeClass('hidden');
                $('[name*=patientId]').val(patientId);
            },
            failure: function (response) {
                console.log(response);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    }
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

function resetLabReport() {
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

}
function resetDiagnosisReport() {
    $('#xray').val("");
    $('#USG').val("");
}
function resetIpdStatus() {
    $('#IPDStatus').val("");
    $('#AdmittedDateTime').val("");
    $('#Reason').val("");
    $('#casesummary').val("");
}

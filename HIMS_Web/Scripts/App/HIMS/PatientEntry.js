/// <reference path="../Global/Utility.js" />
'use strict';

$(document).ready(function () {
    fillTreatment();
    //fillstate();
    //fillArea(null);

    utility.restrictFutureDate('AdmittedDateTime');

    $("#Area").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: '/Common/GetAreaAutoComplete/',
                data: "{ 'prefix': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data, function (item) {
                        return item;
                    }))
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#hfAreaId").val(i.item.val);
        },
        minLength: 0
    }).focus(function () {
        $(this).autocomplete("search");
    });

});

function onSaveIPDEntry() {
    if ($('[name*=IPDNo]').val() == ""
        || $('[name*=AdmittedDateTime]').val() == ""
        || $('[name*=PetientName]').val() == ""
        || $('[name*=Age_Year]').val() == ""
        || $('[name*=Age_Month]').val() == ""
        || $('[name*=Gender]').val() == ""
        || $('[name*=FathersHusbandName]').val() == ""
        || $('[name*=Treatment]').val() == ""
        || $('[name*=Address]').val() == ""
        || $('[name*=Area]').val() == ""
        || $('[name*=department]').val() == ""
        || $('[name*=IDorAadharNumber]').val() == ""
        || $('[name*=IDNumber]').val() == ""
    ) {
        utility.alert.setAlert("IPD Patient Info", "Required parameter is/are missing");
        return false;
    }
    //else if ($('#Treatment').val() == "Other" && $("#OtherTreatment").val() == "") {
    //    utility.alert.setAlert("IPD Patient Info", "Treatment name is mandatory in case of Other");
    //    return false;
    //}
    else if ($('#Area').val() == "Other" && $("#OtherArea").val() == "") {
        utility.alert.setAlert("IPD Patient Info", "Area name is mandatory in case of Other");
        return false;
    }
    else if ($('#Mobile').val().trim() != "" && $('#Mobile').val().length != 10) {
        utility.alert.setAlert("IPD Patient Info", "Mobile Number is not correct");
        return false;
    }
    else {
        if (confirm("Are you sure want to save this IPD Entry?")) {
            document.getElementById('formSave').submit();
        }
    }
}

function fillstate() {
    let dropdown = $('#state');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/Getstate',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.StateId).text(entry.StateName));
            });
            dropdown.val('34');
            dropdown.change();
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

$('#state').on('change', function (e) {
    var valueSelected = this.value;
    fillCity(valueSelected)
});

function fillCity(stateId) {
    let dropdown = $('#city');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetCityByStateId',
        data: '{stateId: "' + stateId + '" }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.CityId).text(entry.CityName));
            })
            dropdown.val('1318');
            dropdown.change();
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

$('#city').on('change', function (e) {
    var valueSelected = this.value;
    fillArea(valueSelected)
});

function fillArea(cityId = null) {
    let dropdown = $('#Area');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetAreaByCityId',
        data: '{cityId: "' + cityId + '" }',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.AreaId).text(entry.AreaName));
            });
            dropdown.append('<option value="Other">Other</option>');
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

$('#Area').on('change', function (e) {
    var valueSelected = this.value;
    $('#OtherAreaDiv').val('');
    if (valueSelected == "Other") {
        $('#OtherAreaDiv').removeClass('hidden');
    }
    else {
        $('#OtherAreaDiv').addClass('hidden');
    }
});

function fillDepartment() {
    let dropdown = $('#department');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetDepartments',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.DepartmentId).text(entry.DeparmentName));
            })
            dropdown.val('7');
            var patientId = getUrlParameter('patientId');
            if (patientId != null && patientId != undefined && patientId > 0) {
                GetPatientDetail(patientId);
            }
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function GetPatientDetail(patientId) {
    $.ajax({
        dataType: 'json',
        type: 'POST',
        data: '{patientId: "' + patientId + '" }',
        url: '/Admin/GetPatientDetailById',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data != null && data != undefined) {
                $('#PatientId').val(data.PatientId);
                $('#IPDNo').val(data.IpdNo);
                $('#IPDNo').attr('disabled', 'disabled');
                if (data.AdmittedDate != null && data.AdmittedDate != undefined) {
                    var milli = data.AdmittedDate.replace(/\/Date\((-?\d+)\)\//, '$1');
                    var now = new Date(parseInt(milli));
                    var day = ("0" + now.getDate()).slice(-2);
                    var month = ("0" + (now.getMonth() + 1)).slice(-2);
                    var today = now.getFullYear() + "-" + (month) + "-" + (day);
                    now.setMinutes(now.getMinutes() - now.getTimezoneOffset());
                    document.getElementById('AdmittedDateTime').value = now.toISOString().slice(0, 16);
                }
                $('#PetientName').val(data.PatientName);
                $('#FathersHusbandName').val(data.FatherOrHusbandName);
                $('#Age_Year').val(data.Age_Year);
                $('#Age_Month').val(data.Age_Month);
                $('#Gender').val(data.Gender);
                $('#Address').val(data.Address);
                $('#hfAreaId').val(data.AreaId);
                $('#Area').val(data.AreaName);
                $('#Mobile').val(data.MobileNumber);
                if (data.TreatmentIds != null) {
                    var ids = data.TreatmentIds.split(',');
                    $('#Treatment').val(ids);
                }
                $('#department').val(data.DepartmentId);
                $('#IDorAadharNumber').val(data.IDorAadharNumber);
                $('#IDNumber').val(data.IDNumber);
            }
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function fillTreatment() {
    let dropdown = $('#Treatment');
    dropdown.empty();
    dropdown.append('<option value="">Select</option>');
    dropdown.prop('selectedIndex', 0);
    $.ajax({
        dataType: 'json',
        type: 'POST',
        url: '/Common/GetTreatment',
        async: true,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $.each(data, function (key, entry) {
                dropdown.append($('<option></option>').attr('value', entry.TreatmentId).text(entry.TreatmentName));
            })
            fillDepartment();
        },
        failure: function (response) {
            alert(response);
        },
        error: function (response) {
            alert(response.responseText);
        }
    });
}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
}

//$('#Treatment').on('change', function (e) {
//    var valueSelected = this.value;
//    $('#OtherTreatment').val('');
//    if (valueSelected == "Other") {
//        $('#OtherTreatmentDiv').removeClass('hidden');
//    }
//    else {
//        $('#OtherTreatmentDiv').addClass('hidden');
//    }
//});

$('#Age_Year').on('keypress', function (event) {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});
$('#Age_Year').on('keypress', function (event) {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});
$('#Mobile').on('keypress', function (event) {
    var regex = new RegExp("^[0-9]+$");
    var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
    if (!regex.test(key)) {
        event.preventDefault();
        return false;
    }
});

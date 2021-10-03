/// <reference path="../Global/Utility.js" />
'use strict';
$(document).ready(function () {
    $('#btnAddNewUser').click(function () {
        $('[name*=userId]').val("");
        $('[name*=username]').val("");
        $('[name*=name]').val("");
        $('[name*=email]').val("");
        $('[name*=Role]').val("");
        $('[name*=mobileNumber]').val("");
        $('[name*=active]').prop('checked', true);
        $('#lblModalTitle').text('Add New User');

        $('#myModal').css('display', 'block');
        $('#myModal').addClass('in');
        $('body').addClass('modal-open')
        $('body').append('<div class="modal-backdrop fade in"></div>');
    });

    $('#btnCloseModal').click(function () {
        $('#myModal').css('display', 'none');
        $('#myModal').removeClass('in');
        $('body').removeClass('modal-open')
        $('.modal-backdrop').remove();
    });
})


function editUser(id) {
    $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        data: '{userId: ' + id + ' }',
        url: '/Admin/GetUserDetail',
        success: function (data) {
            if (data != null) {
                $('[name*=userId]').val(data.Id);
                $('[name*=username]').val(data.UserName);
                $('[name*=name]').val(data.Name);
                $('[name*=email]').val(data.Email);
                $('[name*=mobileNumber]').val(data.MobileNo);
                $('[name*=Designation]').val(data.DesignationId);
                $('[name*=Department]').val(data.DepartmentId);
                $('[name*=Role]').val(data.UserRole);
                $('[name*=active]').prop('checked', data.IsActive);
                $('#lblModalTitle').text('Update User');
            }
        },
        failure: function (response) {
            console.log(response);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
    $('#myModal').css('display', 'block');
    $('#myModal').addClass('in');
    $('body').addClass('modal-open')
    $('body').append('<div class="modal-backdrop fade in"></div>');
}